using Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.ClientModels.CustomerService;
using Data.Models.OracleDb;
using AutoMapper;

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
                return Mapper.Map<List<CTApplications_PendingChanges>, List<PendingChange>>(changes);
            }
        }

        bool IPendingChangesService.Reject(int paxId)
        {
            throw new NotImplementedException();
        }
    }
}
