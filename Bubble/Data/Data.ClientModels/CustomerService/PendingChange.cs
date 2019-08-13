using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ClientModels.CustomerService
{
    public class PendingChange
    {
        public int PaxId { get; set; }

        public short Season { get; set; }

        public int BookingNumber { get; set; }

        public int ComponentNumber { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string BookingType { get; set; }

        public string ProdCode { get; set; }

        public string MarketId { get; set; }
    }
}
