using System;

namespace Data.ClientModels.CustomerService
{
    public class ModifiedPassenger
    {
        public int PaxId { get; set; }

        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string PassportNationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string PassportBirthPlace { get; set; }
    }
}
