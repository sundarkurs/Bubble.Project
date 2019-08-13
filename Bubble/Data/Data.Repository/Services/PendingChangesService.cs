using Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ClientModels.CustomerService;

namespace Data.Repository.Services
{
    public class PendingChangesService : IPendingChangesService
    {
        bool IPendingChangesService.Approve(int paxId)
        {
            throw new NotImplementedException();
        }

        PaxPendingChange IPendingChangesService.GetPaxPendingChanges(int paxId)
        {
            throw new NotImplementedException();
        }

        List<PendingChange> IPendingChangesService.GetPendingChanges()
        {
            throw new NotImplementedException();
        }

        bool IPendingChangesService.Reject(int paxId)
        {
            throw new NotImplementedException();
        }
    }
}
