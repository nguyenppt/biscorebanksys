namespace BankProject.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EntityShift = BankProject.Entity.Shift;
    using Shift = BankProject.Entity.Administration.Shift;

    public class ShiftRepository : RepositoryBase, IShiftRepository
    {
        public IEnumerable<Shift> GetShifts() 
        {
            using (var dbContext = this.CreateDbContext())
            {
                var shifts = new List<Shift>();

                foreach (var entityShift in dbContext.Shifts)
                {
                    shifts.Add(this.ToShift(entityShift));    
                }

                return shifts;
            }
        }

        public Shift GetShift(int shiftId)
        {
            using (var dbContext = this.CreateDbContext())
            {
                var entity = dbContext.Shifts.FirstOrDefault(x => x.Id == shiftId);

                if (entity != null)
                {
                    return this.ToShift(entity);
                }

                return null;
            }
        }

        public void AddShift(Shift shift)
        {
            if (shift == null)
            {
                throw new ArgumentNullException("shift");
            }

            using (var dbContext = this.CreateDbContext())
            {
                var entity = this.ToEntity(shift);
                dbContext.Shifts.InsertOnSubmit(entity);
                dbContext.SubmitChanges();
            }
        }

        public void UpdateShift(Shift shift)
        {
            if (shift == null)
            {
                throw new ArgumentNullException("shift");
            }

            using (var dbContext = this.CreateDbContext())
            {
                var existEntity = dbContext.Shifts.FirstOrDefault(x => x.Id == shift.Id);
                if (existEntity == null)
                {
                    throw new InvalidOperationException();
                }
                
                this.MapChange(shift, existEntity);
                dbContext.SubmitChanges();
            }
        }

        public void RemoveShift(int shiftId)
        {
            using (var dbContext = this.CreateDbContext())
            {
                var existEntity = dbContext.Shifts.FirstOrDefault(x => x.Id == shiftId);
                if (existEntity != null)
                {
                    dbContext.Shifts.DeleteOnSubmit(existEntity);
                    dbContext.SubmitChanges();
                }
            }
        }

        private void MapChange(Shift shift, EntityShift existEntity)
        {
            var beginDate = new DateTime(2000, 1, 1);
            existEntity.BeginShift = beginDate.Add(shift.BeginShift);
            existEntity.EndShift = beginDate.Add(shift.EndShift);
            existEntity.Title = shift.Title;
        }

        private Shift ToShift(EntityShift entityShift)
        {
            return new Shift
                       {
                           Id = entityShift.Id,
                           BeginShift = entityShift.BeginShift.TimeOfDay,
                           EndShift = entityShift.EndShift.TimeOfDay,
                           Title = entityShift.Title
                       };
        }

        private EntityShift ToEntity(Shift shift)
        {
            var beginDate = new DateTime(2000, 1, 1);

            return new EntityShift
            {
                Id = shift.Id,
                BeginShift = beginDate.Add(shift.BeginShift),
                EndShift = beginDate.Add(shift.EndShift),
                Title = shift.Title
            };
        }
    }
}