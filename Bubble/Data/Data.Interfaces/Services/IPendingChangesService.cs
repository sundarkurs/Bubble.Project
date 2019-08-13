#region Namespace

using Data.ClientModels.CustomerService;
using System.Collections.Generic;

#endregion

namespace Data.Interfaces.Services
{
    public interface IPendingChangesService
    {
        List<PendingChange> GetPendingChanges();

        PaxPendingChange GetPaxPendingChanges(int paxId);

        bool Approve(int paxId);

        bool Reject(int paxId);
    }
}
