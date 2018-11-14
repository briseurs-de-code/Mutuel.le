using System;
using System.Web.Http;
namespace rest.Controllers
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
        public string Get()
        {
            return "hello, world";
        }
    }
}
