namespace BankProject.Entity.Administration
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class AccountSession
    {
        private readonly TimeSpan sessionLiveTime;

        private readonly ConcurrentDictionary<string, SessionStatus> sessionIds;

        private readonly int userId;

        private int availableSlot;

        public AccountSession(int userId, int availableSlot, TimeSpan sessionLiveTime)
        {
            this.availableSlot = availableSlot;
            this.sessionLiveTime = sessionLiveTime;
            this.userId = userId;
            this.sessionIds = new ConcurrentDictionary<string, SessionStatus>();
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
        }

        public int TotalUsing
        {
            get
            {
                return this.sessionIds.Count;
            }
        }

        public int AvailableSlot
        {
            get
            {
                return this.availableSlot;
            }

            set
            {
                this.availableSlot = value;
            }
        }

        public ReadOnlyCollection<string> SessionIds
        {
            get
            {
                return new ReadOnlyCollection<string>(this.sessionIds.Keys.ToList());
            }
        }

        public bool RegisterSession(string sessionId, string ipAddress)
        {
            if (this.availableSlot == 0)
            {
                return true;
            }

            var utcNow = DateTime.UtcNow;

            // Reset last time access
            SessionStatus originalSessionStatus;
            if (this.sessionIds.TryGetValue(sessionId, out originalSessionStatus))
            {
                originalSessionStatus.LastTimeAccess = utcNow;
                return true;
            }

            if (this.sessionIds.Count > this.availableSlot)
            {
                return false;
            }

            var newSessionStatus = new SessionStatus(utcNow, sessionId, ipAddress);
            if (this.sessionIds.TryAdd(sessionId, newSessionStatus))
            {
                if (this.sessionIds.Count > this.availableSlot)
                {
                    SessionStatus removeSessionStatus;
                    this.sessionIds.TryRemove(sessionId, out removeSessionStatus);
                    return false;
                }
            }
            
            return true;
        }

        public void ReleaseSession(string sessionId)
        {
            SessionStatus removeSessionStatus;
            this.sessionIds.TryRemove(sessionId, out removeSessionStatus);
        }

        public void AutoReleaseSession(DateTime utcNow)
        {
            foreach (var sessionId in this.sessionIds.Keys)
            {
                SessionStatus sessionStatus;
                if (this.sessionIds.TryGetValue(sessionId, out sessionStatus))
                {
                    var timeSpan = utcNow - sessionStatus.LastTimeAccess;
                    if (timeSpan > this.sessionLiveTime)
                    {
                        this.ReleaseSession(sessionId);
                    }
                }
            }
        }
    }
}