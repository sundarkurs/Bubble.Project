using Data.ClientModels.CustomerService;
using Data.Interfaces.Services;
using Data.Repository.Services;
using Data.WebApi.Base;
using Framework.Core.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Data.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PendingChangesController : BaseDataApiController
    {
        private readonly PendingChangesControllerInternal _pendingChangesControllerInternal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pendingChangesService"></param>
        public PendingChangesController()
        {
            _pendingChangesControllerInternal = new PendingChangesControllerInternal(new PendingChangesService());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetPendingChanges()
        {
            var pendingChanges = new List<PendingChange>();

            try
            {
                return Ok(_pendingChangesControllerInternal.GetPendingChanges());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, MethodBase.GetCurrentMethod().Name);
                return InternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{paxId:int}")]
        public IHttpActionResult GetPassengerChanges(int paxId)
        {
            if (paxId <= 0)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                return Ok(_pendingChangesControllerInternal.GetPaxPendingChanges(paxId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, MethodBase.GetCurrentMethod().Name);
                return InternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{paxId:int}/approve")]
        public IHttpActionResult Approve(int paxId)
        {
            if (paxId <= 0)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                return Ok(_pendingChangesControllerInternal.Approve(paxId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, MethodBase.GetCurrentMethod().Name);
                return InternalServerError(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paxId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{paxId:int}/reject")]
        public IHttpActionResult Reject(int paxId)
        {
            if (paxId <= 0)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                return Ok(_pendingChangesControllerInternal.Reject(paxId));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, MethodBase.GetCurrentMethod().Name);
                return InternalServerError(ex.Message);
            }
        }
    }
}