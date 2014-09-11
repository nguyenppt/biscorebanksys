using System.Collections.Generic;
using System.Linq;
using BankProject.Entity;

namespace BankProject.Repository
{
    public class ListItemRepository : Repository<ListItem>
    {
        static IList<ListItem> fTAccountCodeList = new List<ListItem>();
        static IList<ListItem> closeAccountCodeList = new List<ListItem>();
        static IList<ListItem> DiscountedcloseList = new List<ListItem>();
        static IList<ListItem> blockAccountList = new List<ListItem>();
        static IList<ListItem> tranferAccountList = new List<ListItem>();
        static IList<ListItem> cashDepositeList = new List<ListItem>();
        static IList<ListItem> cashWithdrawPreviewList = new List<ListItem>();
        public ListItemRepository()
        {
            GenerateFTAccountCodeList();

            GenerateBlockAccountList();

            GenerateCloseAccountList();

            GenerateDiscountedcloseList();

            GenerateTransferWithdrawalList();
            GenerateCashDepositeList();
            GenerateCashWithdrawList();
        }

        private static void GenerateCashWithdrawList()
        {
            if (cashWithdrawPreviewList.Count != 0)
                return;

            cashWithdrawPreviewList.Add(new ListItem { Id = 1, Code = "TT/14161/07890" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 2, Code = "TT/14161/07891" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 3, Code = "TT/14161/07892" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 4, Code = "TT/14161/07893" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 5, Code = "TT/14161/07894" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 6, Code = "TT/14161/07895" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 7, Code = "TT/14161/07896" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 8, Code = "TT/14161/07897" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 9, Code = "TT/14161/07898" });
            cashWithdrawPreviewList.Add(new ListItem { Id = 11, Code = "TT/14161/07899" });
        }

        private static void GenerateCashDepositeList()
        {
            if (cashDepositeList.Count != 0)
                return;

            cashDepositeList.Add(new ListItem { Id = 1, Code = "TT/14161/07864" });
            cashDepositeList.Add(new ListItem { Id = 2, Code = "TT/14161/07865" });
            cashDepositeList.Add(new ListItem { Id = 3, Code = "TT/14161/07866" });
            cashDepositeList.Add(new ListItem { Id = 4, Code = "TT/14161/07867" });
            cashDepositeList.Add(new ListItem { Id = 5, Code = "TT/14161/07868" });
            cashDepositeList.Add(new ListItem { Id = 6, Code = "TT/14161/07869" });
            cashDepositeList.Add(new ListItem { Id = 7, Code = "TT/14161/07870" });
            cashDepositeList.Add(new ListItem { Id = 8, Code = "TT/14161/07871" });
            cashDepositeList.Add(new ListItem { Id = 9, Code = "TT/14161/07872" });
            cashDepositeList.Add(new ListItem { Id = 11, Code = "TT/14161/07873" });
        }

        private static void GenerateDiscountedcloseList()
        {
            if (DiscountedcloseList.Count != 0)            
                return;

            DiscountedcloseList.Add(new ListItem { Id = 1, Code = "LD/09156/00380", CustomerName = "Phan Van Han", Currency = "VND", OpenActual = "131,642,494", OpenTotalCredit = "1,600,158", DebitAccount = "020002710049" });
            DiscountedcloseList.Add(new ListItem { Id = 2, Code = "LD/09156/00381", CustomerName = "Dinh Tien Hoang", Currency = "VND", OpenActual = "70,124,785", OpenTotalCredit = "900,257", DebitAccount = "020002561123" });
            DiscountedcloseList.Add(new ListItem { Id = 3, Code = "LD/09156/00382", CustomerName = "Pham Ngoc Thach", Currency = "VND", OpenActual = "80,157,494", OpenTotalCredit = "1,050,248", DebitAccount = "020003360187" });
            DiscountedcloseList.Add(new ListItem { Id = 4, Code = "LD/09156/00383", CustomerName = "Vo Thi Sau", Currency = "VND", OpenActual = "140,125,486", OpenTotalCredit = "1,800,158", DebitAccount = "020003455677" });
            DiscountedcloseList.Add(new ListItem { Id = 5, Code = "LD/09156/00384", CustomerName = "Truong Cong Dinh", Currency = "VND", OpenActual = "25,158,257", OpenTotalCredit = "400,214", DebitAccount = "020005896000" });
            DiscountedcloseList.Add(new ListItem { Id = 6, Code = "LD/09156/00385", CustomerName = "CTY TNHH SONG HONG", Currency = "VND", OpenActual = "160,248,475", OpenTotalCredit = "2,256,214", DebitAccount = "020005788003" });
            DiscountedcloseList.Add(new ListItem { Id = 7, Code = "LD/09156/00386", CustomerName = "CTY TNHH PHAT TRIEN PHAN MEM ABC", Currency = "VND", OpenActual = "131,642,494", OpenTotalCredit = "1,600,158", DebitAccount = "020003668989" });
            DiscountedcloseList.Add(new ListItem { Id = 8, Code = "LD/09156/00387", CustomerName = "Travelocity Corp.", Currency = "USD", OpenActual = "42,494.12", OpenTotalCredit = "125.24", DebitAccount = "020004001996" });
            DiscountedcloseList.Add(new ListItem { Id = 9, Code = "LD/09156/00388", CustomerName = "Wall Street Corp.", Currency = "USD", OpenActual = "50,157.00", OpenTotalCredit = "130.89", DebitAccount = "020002330044" });
            DiscountedcloseList.Add(new ListItem { Id = 11, Code = "LD/09156/00389", CustomerName = "Viet Victory Corp.", Currency = "USD", OpenActual = "9,642.00", OpenTotalCredit = "59.00", DebitAccount = "020009003655" });
        }

        private static void GenerateCloseAccountList()
        {
            if (closeAccountCodeList.Count != 0)
                return;

            closeAccountCodeList.Add(new ListItem { Id = 1, Code = "06.000076987.0", CustomerName = "Phan Van Han", Currency = "VND", OpenActual = "131,642,494", OnlineActual = "233,568,559", OpenTotalCredit = "100,158", OnlineTotalCredit = "139,368" });
            closeAccountCodeList.Add(new ListItem { Id = 2, Code = "06.000076987.1", CustomerName = "Dinh Tien Hoang", Currency = "VND", OpenActual = "70,124,785", OnlineActual = "140,256,000", OpenTotalCredit = "60,257", OnlineTotalCredit = "120,248" });
            closeAccountCodeList.Add(new ListItem { Id = 3, Code = "06.000076987.2", CustomerName = "Pham Ngoc Thach", Currency = "VND", OpenActual = "80,157,494", OnlineActual = "170,571,214", OpenTotalCredit = "70,248", OnlineTotalCredit = "148,215" });
            closeAccountCodeList.Add(new ListItem { Id = 4, Code = "06.000076987.3", CustomerName = "Vo Thi Sau", Currency = "VND", OpenActual = "140,125,486", OnlineActual = "260,214,015", OpenTotalCredit = "120,158", OnlineTotalCredit = "160,258" });
            closeAccountCodeList.Add(new ListItem { Id = 5, Code = "06.000076987.4", CustomerName = "Truong Cong Dinh", Currency = "VND", OpenActual = "25,158,257", OnlineActual = "80,147,254", OpenTotalCredit = "50,214", OnlineTotalCredit = "47,258" });
            closeAccountCodeList.Add(new ListItem { Id = 6, Code = "06.000076987.5", CustomerName = "CTY TNHH SONG HONG", Currency = "VND", OpenActual = "160,248,475", OnlineActual = "300,215,687", OpenTotalCredit = "140,214", OnlineTotalCredit = "187,215" });
            closeAccountCodeList.Add(new ListItem { Id = 7, Code = "06.000076987.6", CustomerName = "CTY TNHH PHAT TRIEN PHAN MEM ABC", Currency = "VND", OpenActual = "131,642,494", OnlineActual = "233,568,559", OpenTotalCredit = "100,158", OnlineTotalCredit = "139,368" });
            closeAccountCodeList.Add(new ListItem { Id = 8, Code = "06.000076987.7", CustomerName = "Travelocity Corp.", Currency = "USD", OpenActual = "42,494.12", OnlineActual = "50,158.00", OpenTotalCredit = "25.24", OnlineTotalCredit = "40.24" });
            closeAccountCodeList.Add(new ListItem { Id = 9, Code = "06.000076987.8", CustomerName = "Wall Street Corp.", Currency = "USD", OpenActual = "50,157.00", OnlineActual = "68,148.14", OpenTotalCredit = "30.89", OnlineTotalCredit = "50.00" });
            closeAccountCodeList.Add(new ListItem { Id = 11, Code = "06.000076987.9", CustomerName = "Viet Victory Corp.", Currency = "USD", OpenActual = "9,642.00", OnlineActual = "11,568.00", OpenTotalCredit = "11.00", OnlineTotalCredit = "15.00" });
        }

        private static void GenerateBlockAccountList()
        {
            if (blockAccountList.Count != 0)
                return;

            blockAccountList.Add(new ListItem { Id = 1, Code = "ACLK1416100070" });
            blockAccountList.Add(new ListItem { Id = 2, Code = "ACLK1416100071" });
            blockAccountList.Add(new ListItem { Id = 3, Code = "ACLK1416100072" });
            blockAccountList.Add(new ListItem { Id = 4, Code = "ACLK1416100073", Status = 2 });
            blockAccountList.Add(new ListItem { Id = 5, Code = "ACLK1416100074", Status = 2 });
            blockAccountList.Add(new ListItem { Id = 6, Code = "ACLK1416100075", Status = 2 });
            blockAccountList.Add(new ListItem { Id = 7, Code = "ACLK1416100076" });
            blockAccountList.Add(new ListItem { Id = 8, Code = "ACLK1416100077" });
            blockAccountList.Add(new ListItem { Id = 9, Code = "ACLK1416100078" });
            blockAccountList.Add(new ListItem { Id = 10, Code = "ACLK1416100079", Status = 2 });
            blockAccountList.Add(new ListItem { Id = 11, Code = "ACLK1416100076", Status =2 });
            blockAccountList.Add(new ListItem { Id = 12, Code = "ACLK1416100077", Status =2 });
            blockAccountList.Add(new ListItem { Id = 13, Code = "ACLK1416100078" });
        }

        private static void GenerateFTAccountCodeList()
        {
            if (fTAccountCodeList.Count != 0)
                return;

            fTAccountCodeList.Add(new ListItem { Id = 1, Code = "FT/14161/80155" });
            fTAccountCodeList.Add(new ListItem { Id = 2, Code = "FT/14161/80156" });
            fTAccountCodeList.Add(new ListItem { Id = 3, Code = "FT/14161/80157" });
            fTAccountCodeList.Add(new ListItem { Id = 4, Code = "FT/14161/80158" });
            fTAccountCodeList.Add(new ListItem { Id = 5, Code = "FT/14161/80159" });
            fTAccountCodeList.Add(new ListItem { Id = 6, Code = "FT/14161/80160" });
            fTAccountCodeList.Add(new ListItem { Id = 7, Code = "FT/14161/80161" });
            fTAccountCodeList.Add(new ListItem { Id = 8, Code = "FT/14161/80162" });
            fTAccountCodeList.Add(new ListItem { Id = 9, Code = "FT/14161/80163" });
            fTAccountCodeList.Add(new ListItem { Id = 10, Code = "FT/14161/80164" });            
        }

        private static void GenerateTransferWithdrawalList()
        {
            if (tranferAccountList.Count != 0)
                return;

            tranferAccountList.Add(new ListItem { Id = 1, Code = "TT/14161/07897" });
            tranferAccountList.Add(new ListItem { Id = 2, Code = "TT/14161/07898" });
            tranferAccountList.Add(new ListItem { Id = 3, Code = "TT/14161/07899" });
            tranferAccountList.Add(new ListItem { Id = 4, Code = "TT/14161/07890" });
            tranferAccountList.Add(new ListItem { Id = 5, Code = "TT/14161/07891" });
            tranferAccountList.Add(new ListItem { Id = 6, Code = "TT/14161/07892" });
            tranferAccountList.Add(new ListItem { Id = 7, Code = "TT/14161/07893" });
            tranferAccountList.Add(new ListItem { Id = 8, Code = "TT/14161/07894" });
            tranferAccountList.Add(new ListItem { Id = 9, Code = "TT/14161/07895" });
            tranferAccountList.Add(new ListItem { Id = 10, Code = "TT/14161/07896" });
        }

        public IQueryable<ListItem> GetFTAccountCodeList()
        {
            return fTAccountCodeList.AsQueryable();
        }

        public ListItem GetFTAccountCodeById(int id)
        {
            return (from li in fTAccountCodeList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public IQueryable<ListItem> GetCloseAccountCodeList()
        {
            return closeAccountCodeList.AsQueryable();
        }

        public ListItem GetCloseAccountCodeById(int id)
        {
            return (from li in closeAccountCodeList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public IQueryable<ListItem> GetDiscountedCloseList()
        {
            return DiscountedcloseList.AsQueryable();
        }

        public ListItem GetDiscountedCloseById(int id)
        {
            return (from li in DiscountedcloseList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public ListItem GetDiscountedCloseByCode(string code)
        {
            return (from li in DiscountedcloseList
                    where li.Code == code
                    select li).FirstOrDefault();
        }

        public IQueryable<ListItem> GetBlockedAccountList()
        {
            return blockAccountList.AsQueryable();
        }

        public ListItem GetBlockedAccountById(int id)
        {
            return (from li in blockAccountList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public void UpdateBlockAccount(ListItem item)
        {
            ListItem currentItem = this.GetBlockedAccountById(item.Id);
            currentItem.Status = item.Status;
        }

        public IQueryable<ListItem> GetPreviewBlockedAccountList()
        {
            return blockAccountList.Where(i => i.Status == 2).AsQueryable();
        }

        public IQueryable<ListItem> GetPreviewTransferWithdrawal()
        {
            return tranferAccountList.AsQueryable();
        }

        public ListItem GetTransferWithdrawalById(int id)
        {
            return (from li in tranferAccountList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public IQueryable<ListItem> GetPreviewCashDeposites()
        {
            return cashDepositeList.AsQueryable();
        }

        public ListItem GetCashDepositeListlById(int id)
        {
            return (from li in cashDepositeList
                    where li.Id == id
                    select li).FirstOrDefault();
        }

        public IQueryable<ListItem> GetPreviewCashWithdrawalList()
        {
            return cashWithdrawPreviewList.AsQueryable();
        }

        public ListItem GetCashWithdrawalById(int id)
        {
            return (from li in cashWithdrawPreviewList
                    where li.Id == id
                    select li).FirstOrDefault();
        }
    }
}