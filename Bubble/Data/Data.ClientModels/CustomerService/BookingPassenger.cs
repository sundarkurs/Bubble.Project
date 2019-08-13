using System;

namespace Data.ClientModels.CustomerService
{
    public partial class BookingPassenger
    {
        public int PaxId { get; set; }
        public string Title { get; set; }
        public string FamilyName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PassportNationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string PassportBirthPlace { get; set; }
    }
}
