using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.TriTT_Saving
{
    public class SavingAccount
    {
        public string CustomerID { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string GBShortName { get; set; }
        public string GBFullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string GBStreet { get; set; }
        public string GBDist { get; set; }
        public string MobilePhone { get; set; }
        public string MaTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string NationalityCode { get; set; }
        public string NationalityName { get; set; }
        public string ResidenceCode { get; set; }
        public string ResidenceName { get; set; }
        public string DocType { get; set; }
        public string DocID { get; set; }
        public string DocIssuePlace { get; set; }
        public DateTime ? DocIssueDate { get; set; }
        public DateTime ? DocExpiryDate { get; set; }
        public string SectorCode { get; set; }
        public string SectorName { get; set; }
        public string SubSectorCode { get; set; }
        public string SubSectorName { get; set; }
        public string IndustryCode { get; set; }
        public string IndustryName { get; set; }
        public string SubIndustryCode { get; set; }
        public string SubIndustryName { get; set; }
        public string TargetCode { get; set; }
        public string MaritalStatus { get; set; }
        public string AccountOfficer { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public DateTime ? ContactDate { get; set; }
        public string RelationCode { get; set; }
        public string OfficeNumber { get; set; }
        public string FaxNumber { get; set; }
        public string NoOfDependant { get; set; }
        public string NoOfChildUnder15 { get; set; }
        public string NoOfChildUnder25 { get; set; }
        public string NoOfchildOver25 { get; set; }
        public string HomeOwnerShip { get; set; }
        public string ResidenceType { get; set; }
        public string EmploymentStatus { get; set; }
        public string CompanyName { get; set; }
        public string Currency { get; set; }
        public string MonthlyIncome { get; set; }
        public string OfficeAddress { get; set; }
        public string CustomerLiability { get; set; }
        public string ApprovedUser {get; set;}
        public string EmailAddress { get; set; }
    }
}