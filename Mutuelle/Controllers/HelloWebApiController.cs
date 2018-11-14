using System;
using System.Web.Http;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Http.Results;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

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
            Mat pattern = new Mat(‪"C:\\Users\\svent\\Documents\\DotNet\\Mutuel.le\\Mutuelle\\Assets\\harmonie.png");
            Mat carteTest = new Mat("/Assets/carte_test.png");
            Mat result = null;

            Emgu.CV.CvInvoke.MatchTemplate(carteTest, pattern, result, Emgu.CV.CvEnum.TemplateMatchingType.Ccoeff);

            List<String> collection = new List<String>
            {
               //Path.GetFullPath("HelloWebApiController.cs"),
               //Path.GetFullPath("Assets/carte_test.png"),
               //Path.GetFullPath("Mutuelle/Assets/carte_test.png"),
               Path.GetPathRoot("/"),
            };

            return Json(collection);
        }
    }
}
