using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankProject.DBContext;
using System.Data.Entity;

namespace BankProject.DBRespository
{
    public class OpenAccountRepository : BaseRepository<BOPENACCOUNT>, IOpenAccountRepository
    {
       
    }
}