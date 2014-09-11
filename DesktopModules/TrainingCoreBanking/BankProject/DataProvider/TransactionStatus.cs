namespace BankProject.DataProvider
{
    public static class TransactionStatus
    {
        public static readonly string UNA = "UNA";//Unauthorized : Chờ duyệt
        public static readonly string DEL = "DEL";//Delete : Xóa, không duyệt
        public static readonly string AUT = "AUT";//Authorized : Đã duyệt
        public static readonly string RUNA = "RUNA";//Unauthorized reverse : Chờ duyệt reverse
        public static readonly string REV = "REV";//Reversed : Đã reverse
    }
}