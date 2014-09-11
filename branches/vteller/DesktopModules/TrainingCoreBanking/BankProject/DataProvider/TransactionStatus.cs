namespace BankProject.DataProvider
{
    public static class TransactionStatus
    {
        public static readonly string NAU = "NAU";//Unauthorized : Chờ duyệt
        public static readonly string AUT = "AUT";//Authorized : Đã duyệt
        public static readonly string REV = "REV";//Reversed : Đã reverse
    }
}