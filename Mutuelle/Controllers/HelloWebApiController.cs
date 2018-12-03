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
using System.Web.Hosting;

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
            //var filename = @"C:\Users\svent\Documents\DotNet\Mutuel.le\Mutuelle\Assets\harmonie.png";
            var carteFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "carte_test.png");
            var patternFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "harmonie.png");

            //if (!File.Exists(filename)) return null;
            //if (!File.Exists(carteFilename)) return null;

            Mat pattern = new Mat(patternFilename);
            Mat carteTest = new Mat(carteFilename);
            Mat result = new Mat();

            Emgu.CV.CvInvoke.MatchTemplate(carteTest, pattern, result, Emgu.CV.CvEnum.TemplateMatchingType.Ccoeff);

            List<String> collection = new List<String>
            {
               "test"
            };

            return Json(collection);
        }
    }
}
