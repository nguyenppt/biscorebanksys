namespace BankProject.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BankProject.Entity;
    using BankProject.Entity.Administration;

    using DotNetNuke.Entities.Users;

    using AccountPeriod = BankProject.Entity.Administration.AccountPeriod;
    using EntityAccountPeriod = Entity.AccountPeriod;
    using Shift = BankProject.Entity.Administration.Shift;

    public class AccountPeriodRepository : RepositoryBase, IAccountPeriodRepository
    {
        public IEnumerable<AccountPeriod> GetByUserId(int userId)
        {
            using (var dbContext = this.CreateDbContext())
            {
                var list = new List<AccountPeriod>();

                var entities =
                    dbContext.AccountPeriods.Where(x => x.UserId == userId);

                foreach (var entity in entities)
                {
                    list.Add(this.ToAccountPeriod(entity));
                }

                return list;
            }
        }

        public void AddAccountPeriod(AccountPeriod accountPeriod)
        {
            if (accountPeriod == null)
            {
                throw new ArgumentNullException("accountPeriod");
            }

            using (var dbContext = this.CreateDbContext())
            {
                var entityAccountPeriod = this.ToEntity(accountPeriod);
                dbContext.AccountPeriods.InsertOnSubmit(entityAccountPeriod);

                foreach (var shift in accountPeriod.Shifts)
                {
                    var entityShift = dbContext.Shifts.FirstOrDefault(x => x.Id == shift.Id);
                    if (entityShift == null)
                    {
                        throw new InvalidOperationException("Shift does not exist");
                    }

                    dbContext.AccountShifts
                        .InsertOnSubmit(new AccountShift
                        {
                            AccountPeriod = entityAccountPeriod,
                            AccountPeriodId = accountPeriod.Id,
                            Shift = entityShift,
                            ShiftId = shift.Id
                        });
                }

                dbContext.SubmitChanges();
            }
        }

        public void UpdateAccountPeriod(AccountPeriod accountPeriod)
        {
            if (accountPeriod == null)
            {
                throw new ArgumentNullException("accountPeriod");
            }

            using (var dbContext = this.CreateDbContext())
            {
                var existEntity = dbContext.AccountPeriods.FirstOrDefault(x => x.Id == accountPeriod.Id);
                if (existEntity == null)
                {
                    throw new InvalidOperationException();
                }


                this.MapChange(dbContext, accountPeriod, existEntity);

                // Remove account shift
                foreach (var accountShift in existEntity.AccountShifts)
                {
                    dbContext.AccountShifts.DeleteOnSubmit(accountShift);
                }

                existEntity.AccountShifts.Clear();

                // Add new account shift
                foreach (var shift in accountPeriod.Shifts)
                {
                    var entityShift = dbContext.Shifts.FirstOrDefault(x => x.Id == shift.Id);
                    if (entityShift == null)
                    {
                        throw new InvalidOperationException("Shift does not exist");
                    }

                    dbContext.AccountShifts
                        .InsertOnSubmit(new AccountShift
                        {
                            AccountPeriod = existEntity,
                            AccountPeriodId = accountPeriod.Id,
                            ShiftId = shift.Id,
                            Shift = entityShift
                        });
                }

                dbContext.SubmitChanges();
            }
        }

        public IEnumerable<AccountPeriod> GetAll()
        {
            using (var dbContext = this.CreateDbContext())
            {
                var accountPeriods = new List<AccountPeriod>();

                var entities = dbContext.AccountPeriods;
                foreach (var entity in entities)
                {
                    accountPeriods.Add(this.ToAccountPeriod(entity));
                }

                return accountPeriods;
            }
        }

        public AccountPeriod GetAccountPeriod(int accountPeriodId)
        {
            using (var dbContext = this.CreateDbContext())
            {
                var entity = dbContext.AccountPeriods.FirstOrDefault(x => x.Id == accountPeriodId);

                if (entity != null)
                {
                    return this.ToAccountPeriod(entity);
                }

                return null;
            }
        }

        private void MapChange(BankProjectModelsDataContext dbContext, AccountPeriod accountPeriod, EntityAccountPeriod existEntity)
        {
            existEntity.Title = accountPeriod.Title;
            existEntity.AvailableSlot = accountPeriod.AvailableSlot;
            existEntity.BeginPeriod = accountPeriod.BeginPeriod;
            existEntity.EndPeriod = accountPeriod.EndPeriod;
            existEntity.UserId = accountPeriod.UserId;
            existEntity.IsEnabled = accountPeriod.IsEnabled;
            existEntity.IsBlocked = accountPeriod.IsBlocked;
            existEntity.WorkingDay = (int)accountPeriod.WorkingDay;

            var currentShiftIds = accountPeriod.Shifts.Select(x => x.Id).ToArray();
            var originalShifts = existEntity.AccountShifts.ToList();

            foreach (var accountShift in originalShifts)
            {
                if (!currentShiftIds.Contains(accountShift.ShiftId))
                {
                    existEntity.AccountShifts.Remove(accountShift);
                    dbContext.AccountShifts.DeleteOnSubmit(accountShift);
                }
            }

            foreach (var shiftId in currentShiftIds)
            {
                if (existEntity.AccountShifts.All(x => x.ShiftId != shiftId))
                {
                    var newAccountShift = new AccountShift { AccountPeriodId = accountPeriod.Id, ShiftId = shiftId };
                    existEntity.AccountShifts.Add(newAccountShift);
                    dbContext.AccountShifts.InsertOnSubmit(newAccountShift);
                }
            }
        }

        private EntityAccountPeriod ToEntity(AccountPeriod accountPeriod)
        {
            return new EntityAccountPeriod
            {
                Id = accountPeriod.Id,
                Title = accountPeriod.Title,
                AvailableSlot = accountPeriod.AvailableSlot,
                BeginPeriod = accountPeriod.BeginPeriod,
                EndPeriod = accountPeriod.EndPeriod,
                UserId = accountPeriod.UserId,
                IsEnabled = accountPeriod.IsEnabled,
                IsBlocked = accountPeriod.IsBlocked,
                WorkingDay = (int)accountPeriod.WorkingDay
            };
        }

        private AccountPeriod ToAccountPeriod(EntityAccountPeriod entity)
        {
            var userInfo = UserController.GetUserById(this.PortalId, entity.UserId);

            return new AccountPeriod
            {
                Id = entity.Id,
                Title = entity.Title,
                AvailableSlot = entity.AvailableSlot,
                BeginPeriod = entity.BeginPeriod,
                EndPeriod = entity.EndPeriod,
                UserId = entity.UserId,
                Username = userInfo == null ? "(Deleted)" : userInfo.Username,
                IsEnabled = entity.IsEnabled,
                IsBlocked = entity.IsBlocked,
                WorkingDay = (WorkingDay)entity.WorkingDay,
                Shifts = this.ConvertShift(entity.AccountShifts)
            };
        }

        private IList<Shift> ConvertShift(IEnumerable<AccountShift> accountShifts)
        {
            var shifts = new List<Shift>();
            foreach (var accountShift in accountShifts)
            {
                shifts.Add(
                    new Shift
                    {
                        Id = accountShift.ShiftId,
                        BeginShift = accountShift.Shift.BeginShift.TimeOfDay,
                        EndShift = accountShift.Shift.EndShift.TimeOfDay,
                        Title = accountShift.Shift.Title
                    });
            }

            return shifts;
        }

        public void RemoveAccountPeriod(int accountPeriodId)
        {
            using (var context = this.CreateDbContext())
            {
                var accountPeriod = context.AccountPeriods.FirstOrDefault(x => x.Id == accountPeriodId);
                if (accountPeriod != null)
                {
                    context.AccountShifts.DeleteAllOnSubmit(accountPeriod.AccountShifts);
                    context.AccountPeriods.DeleteOnSubmit(accountPeriod);

                    context.SubmitChanges();
                }
            }
        }
    }
}