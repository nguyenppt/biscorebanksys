namespace BankProject.Entity.Administration
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Linq;
    using System.Threading;

    using BankProject.Repository;

    using DotNetNuke.Entities.Users;

    public class SessionManagment : IDisposable
    {
        private static readonly Lazy<SessionManagment> LazySessionManagement =
            new Lazy<SessionManagment>(
                () =>
                {
                    var timeToLiveString = ConfigurationManager.AppSettings["SessionManagement_TimeToLive"];
                    var autoReleaseIntervalString = ConfigurationManager.AppSettings["SessionManagement_AutoReleaseInterval"];

                    TimeSpan timeToLive, autoReleaseInterval;
                    if (!TimeSpan.TryParse(timeToLiveString, out timeToLive))
                    {
                        timeToLive = TimeSpan.FromMinutes(5);
                    }

                    if (!TimeSpan.TryParse(autoReleaseIntervalString, out autoReleaseInterval))
                    {
                        autoReleaseInterval = TimeSpan.FromMinutes(5);
                    }

                    var sessionManagement = new SessionManagment(timeToLive, autoReleaseInterval, new AccountPeriodRepository());
                    sessionManagement.Start();
                    return sessionManagement;
                });

        private readonly IAccountPeriodRepository accountPeriodRepository;

        private readonly ConcurrentDictionary<int, AccountSession> accountSessions;

        private readonly TimeSpan timeLiveSession;

        private readonly TimeSpan autoReleasePeriod;

        private Timer autoReleaseSessionTimer;

        private bool disposed;

        public SessionManagment(IAccountPeriodRepository accountPeriodRepository)
            : this(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5), accountPeriodRepository)
        {
        }

        public SessionManagment(TimeSpan timeLiveSession, TimeSpan autoReleasePeriod, IAccountPeriodRepository accountPeriodRepository)
        {
            this.timeLiveSession = timeLiveSession;
            this.autoReleasePeriod = autoReleasePeriod;
            this.accountPeriodRepository = accountPeriodRepository;
            this.accountSessions = new ConcurrentDictionary<int, AccountSession>();
        }

        public static SessionManagment Default
        {
            get
            {
                return LazySessionManagement.Value;
            }
        }

        public ReadOnlyCollection<AccountSession> AccountSessions
        {
            get
            {
                return new ReadOnlyCollection<AccountSession>(this.accountSessions.Values.ToList());
            }
        }

        public void Start()
        {
            this.ScheduleAutoReleaseSession();
        }

        public bool RegisterSession(int userId, string sessionId, string ipAddress)
        {
            var accountPeriods = this.accountPeriodRepository
                .GetByUserId(userId).Where(x => x.IsEnabled).ToArray();
            if (!accountPeriods.Any())
            {
                return true;
            }

            foreach (var accountPeriod in accountPeriods)
            {
                if (accountPeriod.IsBlocked)
                {
                    continue;
                }

                Shift approveShift;
                AccountSession accountSession;
                bool isTotalUserChanged;
                var isValid = this.RegisterSession(userId, sessionId, ipAddress, accountPeriod, out approveShift, out accountSession, out isTotalUserChanged);
                if (isValid)
                {
                    if (approveShift != null && accountSession != null && isTotalUserChanged)
                    {
                        // Save session history, only save if using is increase
                        var dateTimeNow = DateTime.Now;
                        var sessionHistoryRepository = new SessionHistoryRepository();
                        var sessionHistory = new SessionHistory
                        {
                            AccountPeriodId = accountPeriod.Id,
                            BeginShift = approveShift.BeginShift,
                            EndShift = approveShift.EndShift,
                            CreatedTime = dateTimeNow,
                            ModifiedTime = dateTimeNow,
                            MaxSession = accountPeriod.AvailableSlot,
                            ShiftId = approveShift.Id,
                            Title = accountPeriod.Title,
                            TotalUser = accountSession.TotalUsing,
                            UserId = userId,
                            Username = UserController.GetUserById(0, userId).Username
                        };

                        sessionHistoryRepository.SaveHistory(sessionHistory);
                    }

                    return true;
                }
            }

            return false;
        }

        public bool AllowToLogin(int userId)
        {
            var dateTimeNow = DateTime.Now;
            var accountPeriods = this.accountPeriodRepository.GetByUserId(userId).Where(x => x.IsEnabled).ToArray();
            if (!accountPeriods.Any())
            {
                return true;
            }

            foreach (var accountPeriod in accountPeriods)
            {
                if (accountPeriod.IsBlocked)
                {
                    continue;
                }

                Shift approveShift;
                if (this.AllowToLogin(dateTimeNow, accountPeriod, out approveShift))
                {
                    return true;
                }
            }

            return false;
        }

        public bool AllowToLogin(int userId, AccountPeriod accountPeriod, out Shift approveShift)
        {
            var utcNow = DateTime.Now;
            return this.AllowToLogin(utcNow, accountPeriod, out approveShift);
        }

        public bool AllowToLogin(int userId, AccountPeriod accountPeriod)
        {
            var utcNow = DateTime.Now;
            Shift approveShift;
            return this.AllowToLogin(utcNow, accountPeriod, out approveShift);
        }

        public bool AllowToLogin(DateTime now, AccountPeriod accountPeriod, out Shift approveShift)
        {
            var workingDay = this.ToWorkingDay(now.DayOfWeek);
            approveShift = null;

            // Check range of date
            if (now < accountPeriod.BeginPeriod || now > accountPeriod.EndPeriod.Date.AddDays(1).AddSeconds(-1))
            {
                return false;
            }

            // Check working day
            if ((accountPeriod.WorkingDay & workingDay) != workingDay)
            {
                return false;
            }

            // Check have shift
            bool haveShiftOnTime = false;
            foreach (var shift in accountPeriod.Shifts)
            {
                if (now.TimeOfDay >= shift.BeginShift && now.TimeOfDay <= shift.EndShift)
                {
                    haveShiftOnTime = true;
                    approveShift = shift;
                    break;
                }
            }

            if (!haveShiftOnTime)
            {
                return false;
            }

            return true;
        }

        public void ReleaseSession(int userId, string sessionId)
        {
            AccountSession accountSession;
            if (this.accountSessions.TryGetValue(userId, out accountSession))
            {
                accountSession.ReleaseSession(sessionId);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool RegisterSession(
            int userId, string sessionId, string ipAddress, AccountPeriod accountPeriod, out Shift approveShift, out AccountSession accountSession, out bool isTotalUsingChanged)
        {
            approveShift = null;
            accountSession = null;
            isTotalUsingChanged = false;

            if (accountPeriod == null || !accountPeriod.IsEnabled)
            {
                return true;
            }

            if (accountPeriod.IsBlocked)
            {
                return false;
            }

            if (!this.AllowToLogin(userId, accountPeriod, out approveShift))
            {
                return false;
            }

            accountSession = this.TryGetAccountSession(userId);
            var originalUsing = accountSession.TotalUsing;
            var result = accountSession.RegisterSession(sessionId, ipAddress);

            if (result)
            {
                if (accountSession.TotalUsing >= originalUsing)
                {
                    isTotalUsingChanged = true;
                }
            }

            return result;
        }

        private AccountSession TryGetAccountSession(int userId)
        {
            int availableSlot = this.GetAccountAvailableSlot(userId);
            var accountSession = new AccountSession(userId, availableSlot, this.timeLiveSession);
            if (!this.accountSessions.TryAdd(userId, accountSession))
            {
                if (!this.accountSessions.TryGetValue(userId, out accountSession))
                {
                    throw new InvalidOperationException(
                        "Some thing wrong with session management. Expect exist account session but it doesn't");
                }

                // Update available slot from database
                accountSession.AvailableSlot = availableSlot;
            }

            return accountSession;
        }

        private void OnAutoReleaseSession(object state)
        {
            if (this.disposed)
            {
                return;
            }

            var utcNow = DateTime.UtcNow;
            foreach (var accountSession in this.accountSessions)
            {
                accountSession.Value.AutoReleaseSession(utcNow);
            }

            this.ScheduleAutoReleaseSession();
        }

        private void ScheduleAutoReleaseSession()
        {
            if (this.autoReleaseSessionTimer != null)
            {
                this.autoReleaseSessionTimer.Dispose();
            }

            // Setup new auto release timer
            this.autoReleaseSessionTimer = new Timer(this.OnAutoReleaseSession, null, this.autoReleasePeriod, new TimeSpan(-1));
        }

        private int GetAccountAvailableSlot(int userId)
        {
            var accountPeriods = this.accountPeriodRepository.GetByUserId(userId);
            var now = DateTime.Now;
            foreach (var accountPeriod in accountPeriods)
            {
                Shift approveShift;
                if (this.AllowToLogin(now, accountPeriod, out approveShift))
                {
                    return accountPeriod.AvailableSlot;
                }
            }

            return 0;
        }

        private WorkingDay ToWorkingDay(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return WorkingDay.Monday;
                case DayOfWeek.Tuesday:
                    return WorkingDay.Tuesday;
                case DayOfWeek.Wednesday:
                    return WorkingDay.Wednesday;
                case DayOfWeek.Thursday:
                    return WorkingDay.Thursday;
                case DayOfWeek.Friday:
                    return WorkingDay.Friday;
                case DayOfWeek.Saturday:
                    return WorkingDay.Saturday;
                case DayOfWeek.Sunday:
                    return WorkingDay.Sunday;
            }

            throw new NotSupportedException(string.Format("Not support dayOfWeek {0}", dayOfWeek));
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.autoReleaseSessionTimer != null)
                {
                    this.autoReleaseSessionTimer.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}