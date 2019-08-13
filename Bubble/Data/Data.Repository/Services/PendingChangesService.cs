using Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.ClientModels.CustomerService;
using Data.Models.OracleDb;

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
            using (var context = new BluecowEntities())
            {
                var changes = context.CTApplications_PendingChanges.ToList();

                var response = new List<PendingChange>();
                response.Add(new PendingChange()
                {
                    FirstName = changes[0].FIRST_NAME,
                    FamilyName = changes[0].FAMILY_NAME
                });

                return response;
            }
        }

        bool IPendingChangesService.Reject(int paxId)
        {
            throw new NotImplementedException();
        }
    }
}
