using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Data.ClientModels;
using System.Web.Http.Results;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Data.ClientModels.Comm;

namespace Data.WebApi.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseDataApiController: ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        protected BaseDataApiController()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected new ResponseMessageResult BadRequest(string message)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new Response<object>(false, message)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ResponseMessageResult InternalServerError(string message)
        {
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new Response<object>(false, message)));
        }
    }
}