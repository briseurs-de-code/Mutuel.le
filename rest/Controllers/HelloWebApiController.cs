using System;
using System.Web.Http;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Http.Results;

namespace Mutuelle.Controllers
{
    /// <summary>
    /// Hello web API controller.
    /// </summary>
    public class HelloWebApiController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:rest.Controllers.HelloWebApiController"/> class.
        /// </summary>
        public HelloWebApiController()
        {
        }

        /// <summary>
        /// Get this instance.
        /// </summary>
        /// <returns>The get.</returns>
        public JsonResult<List<string>> Get()
        {
            List<String> collection = new List<String>();

            collection.Add("abc");

            return Json(collection);
        }
    }
}
