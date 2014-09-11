using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string GBFullName { get; set; }

        public string GBTownDist { get; set; }

        public string MobilePhone { get; set; }

        public int CityId { get; set; }

        public string Country { get; set; }

        public string Nationality { get; set; }

        public int Residence { get; set; }

        public int DocTypeId { get; set; }

        public string DocIdentify { get; set; }

        public string DocIssuePlace { get; set; }

        public DateTime DocIssueDate { get; set; }

        public DateTime DocExpiryDate { get; set; }

        public int MainSector { get; set; }

        public int Sector { get; set; }

        public int MainIndustryId { get; set; }

        public int IndustryId { get; set; }

        public int TargetId { get; set; }

        public int CustomerStatus { get; set; }

        public int AccountOfficer { get; set; }

        public int CifCode { get; set; }
    }
}