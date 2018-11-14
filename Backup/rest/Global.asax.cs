using System.Web;
using System.Web.Http;
using System;

namespace rest
{
    /// <summary>
    /// Global.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>
        /// Sessions the start.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applications the begin request.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applications the end request.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applications the authenticate request.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applications the error.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Application_Error(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Sessions the end.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Session_End(Object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applications the end.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected void Application_End(Object sender, EventArgs e)
        {

        }
    }
}
