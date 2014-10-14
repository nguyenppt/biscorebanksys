namespace BankProject.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EntitySessionHistory = BankProject.Entity.SessionHistory;
    using SessionHistory = BankProject.Entity.Administration.SessionHistory;

    public class SessionHistoryRepository : RepositoryBase
    {
        private static readonly object Sync = new object();

        public IEnumerable<SessionHistory> GetSessionHistories()
        {
            using (var dbContext = this.CreateDbContext())
            {
                var sessionHistories = new List<SessionHistory>();
                var entities = dbContext.SessionHistories;
                foreach (var entity in entities)
                {
                    sessionHistories.Add(this.ToSessionHistory(entity));
                }

                return sessionHistories;
            }
        }

        public IEnumerable<SessionHistory> GetSessionHistories(int? userId, DateTime? fromDate, DateTime? toDate)
        {
            using (var dbContext = this.CreateDbContext())
            {
                var sessionHistories = new List<SessionHistory>();
                IQueryable<EntitySessionHistory> query = dbContext.SessionHistories;

                query = this.QueryFilter(query, userId, fromDate, toDate);

                var entities = query.ToArray();
                foreach (var entity in entities)
                {
                    sessionHistories.Add(this.ToSessionHistory(entity));
                }

                return sessionHistories;
            }
        }

        public void SaveHistory(SessionHistory sessionHistory)
        {
            lock (Sync)
            {
                using (var dbContext = this.CreateDbContext())
                {
                    var beginOfDate = sessionHistory.CreatedTime.Date;
                    var endOfDate = sessionHistory.CreatedTime.Date.AddDays(1).AddSeconds(-1);
                    var existHistory =
                        dbContext.SessionHistories.FirstOrDefault(
                        x => x.AccountPeriodId == sessionHistory.AccountPeriodId && x.CreatedTime >= beginOfDate && x.CreatedTime <= endOfDate);

                    if (existHistory == null)
                    {
                        var entity = this.ToEntity(sessionHistory);
                        dbContext.SessionHistories.InsertOnSubmit(entity);
                    }
                    else
                    {
                        // Do not update the past date
                        existHistory.TotalUser = sessionHistory.TotalUser;
                        existHistory.MaxSession = sessionHistory.MaxSession;
                        existHistory.BeginShift = sessionHistory.CreatedTime.Date + sessionHistory.BeginShift;
                        existHistory.EndShift = sessionHistory.CreatedTime.Date + sessionHistory.EndShift;
                        existHistory.Title = sessionHistory.Title;
                        existHistory.ModifiedTime = sessionHistory.ModifiedTime;
                        existHistory.UserId = sessionHistory.UserId;
                        existHistory.Username = sessionHistory.Username;
                    }

                    dbContext.SubmitChanges();
                } 
            }
        }

        public void Purge(int? userId, DateTime? fromDate, DateTime? toDate)
        {
            using (var dbContext = this.CreateDbContext())
            {
                IQueryable<EntitySessionHistory> query = dbContext.SessionHistories;
                query = this.QueryFilter(query, userId, fromDate, toDate);
                dbContext.SessionHistories.DeleteAllOnSubmit(query);
                dbContext.SubmitChanges();
            }
        }

        private IQueryable<EntitySessionHistory> QueryFilter(IQueryable<EntitySessionHistory> query, int? userId, DateTime? fromDate, DateTime? toDate)
        {
            if (userId != null)
            {
                query = query.Where(x => x.UserId == userId.Value);
            }

            if (fromDate != null)
            {
                var fixedFromDate = fromDate.Value.Date;
                query = query.Where(x => x.CreatedTime >= fixedFromDate);
            }

            if (toDate != null)
            {
                var fixedToDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(x => x.CreatedTime <= fixedToDate);
            }

            return query;
        }

        private EntitySessionHistory ToEntity(SessionHistory sessionHistory)
        {
            return new EntitySessionHistory
                       {
                           Id = sessionHistory.Id,
                           AccountPeriodId = sessionHistory.AccountPeriodId,
                           BeginShift = sessionHistory.CreatedTime.Date + sessionHistory.BeginShift,
                           EndShift = sessionHistory.CreatedTime.Date + sessionHistory.EndShift,
                           CreatedTime = sessionHistory.CreatedTime,
                           ShiftId = sessionHistory.ShiftId,
                           MaxSession = sessionHistory.MaxSession,
                           ModifiedTime = sessionHistory.ModifiedTime,
                           Title = sessionHistory.Title,
                           TotalUser = sessionHistory.TotalUser,
                           UserId = sessionHistory.UserId,
                           Username = sessionHistory.Username
                       };
        }

        private SessionHistory ToSessionHistory(EntitySessionHistory entity)
        {
            return new SessionHistory
                       {
                           AccountPeriodId = entity.AccountPeriodId,
                           BeginShift = entity.BeginShift.TimeOfDay,
                           EndShift = entity.EndShift.TimeOfDay,
                           Id = entity.Id,
                           MaxSession = entity.MaxSession,
                           Title = entity.Title,
                           TotalUser = entity.TotalUser,
                           UserId = entity.UserId,
                           Username = entity.Username,
                           CreatedTime = entity.CreatedTime,
                           ModifiedTime = entity.ModifiedTime
                       };
        }

    }
}