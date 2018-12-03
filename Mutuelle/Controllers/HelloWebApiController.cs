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
using Emgu.CV.Features2D;
using Emgu.CV.Util;
using System.Drawing;
using Emgu.CV.UI;

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
            var carteFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "carte_test.png");
            var patternFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "harmonie.png");

            Image<Bgr, byte> source = new Image<Bgr, byte>(carteFilename); // Image B
            Image<Bgr, byte> template = new Image<Bgr, byte>(patternFilename); // Image A
            Image<Bgr, byte> imageToShow = source.Copy();
            
            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.3)
                {
                    // This is a match. Do something with it, for example draw a rectangle around it.
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imageToShow.Draw(match, new Bgr(Color.Red), 3);
                }
            }

            imageToShow.ToBitmap().Save(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "resultat.png"));
            
            List<String> collection = new List<String>
            {
               //result.Cols.ToString(),
               //result.Rows.ToString(),
               //resultat.Size.ToString(),
            };    

            return Json(collection);
        }
    }
}
