using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class BookingInformation
    {
        public int ResBookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int SiteID { get; set; }
        public int ResourceID { get; set; }
        public int StaffID { get; set; }
        public String Comments { get; set; }
        public String Status { get; set; }
        public int ResCategoryID { get; set; }
        public String RName { get; set; }
        public String SName { get; set; }
    }
}