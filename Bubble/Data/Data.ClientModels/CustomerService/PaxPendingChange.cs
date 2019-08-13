
namespace Data.ClientModels.CustomerService
{
    public class PaxPendingChange
    {
        public BookingPassenger MasterData { get; set; }
        public ModifiedPassenger StagedData { get; set; }
    }
}
