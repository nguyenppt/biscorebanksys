using BankProject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Repository
{
    public class BlockedAccountRepository : IRepository<BlockedAccount>
    {
        private static IList<BlockedAccount> data = new List<BlockedAccount>();       
        public void InsertBlockAccount(BlockedAccount blockAccount)
        {
            BlockedAccount currentblockAccount = data.SingleOrDefault(ac => ac.Id == blockAccount.Id);
            if (currentblockAccount == null)
            {
                currentblockAccount = new BlockedAccount();
                currentblockAccount.Amount = blockAccount.Amount;
                currentblockAccount.FromDate = blockAccount.FromDate;
                currentblockAccount.Status = blockAccount.Status;
                currentblockAccount.ToDate = blockAccount.ToDate;
                data.Add(currentblockAccount);
            }
            else
            {
                currentblockAccount.Amount = blockAccount.Amount;
                currentblockAccount.FromDate = blockAccount.FromDate;
                currentblockAccount.Status = blockAccount.Status;
                currentblockAccount.ToDate = blockAccount.ToDate;
            }            
        }

        //public BlockedAccount GetBlockedAccount(int id)
        //{ 
        //    return        
        //}

        public IQueryable<BlockedAccount> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}