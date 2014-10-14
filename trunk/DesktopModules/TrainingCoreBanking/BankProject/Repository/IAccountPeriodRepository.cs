namespace BankProject.Repository
{
    using System.Collections.Generic;

    using BankProject.Entity.Administration;

    public interface IAccountPeriodRepository
    {
        IEnumerable<AccountPeriod> GetByUserId(int userId);
    }
}