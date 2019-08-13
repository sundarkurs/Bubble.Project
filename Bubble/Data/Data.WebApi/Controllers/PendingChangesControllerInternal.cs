using Data.ClientModels.Comm;
using Data.ClientModels.CustomerService;
using Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PendingChangesControllerInternal
    {
        private readonly IPendingChangesService _pendingChangesService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pendingChangesService"></param>
        public PendingChangesControllerInternal(IPendingChangesService pendingChangesService)
        {
            _pendingChangesService = pendingChangesService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Response<PendingChange> GetPendingChanges()
        {
            var serviceResult = _pendingChangesService.GetPendingChanges();

            if (serviceResult == null)
            {
                return new Response<PendingChange>(false, "No data found.");
            }

            return new Response<PendingChange>(serviceResult);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        public Response<PaxPendingChange> GetPaxPendingChanges(int paxId)
        {
            var serviceResult = _pendingChangesService.GetPaxPendingChanges(paxId);

            if (serviceResult == null)
            {
                return new Response<PaxPendingChange>(false, "No data found.");
            }

            return new Response<PaxPendingChange>(serviceResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        public Response<bool> Approve(int paxId)
        {
            return new Response<bool>(_pendingChangesService.Approve(paxId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        public Response<bool> Reject(int paxId)
        {
            return new Response<bool>(_pendingChangesService.Reject(paxId));
        }
    }
}