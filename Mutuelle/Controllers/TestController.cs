using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Results;

namespace rest.Controllers
{
    public class TestController : ApiController
    {

        public static void SearchImage(object imageDTOObj)
        {
            ImageDTO imageDTO = (ImageDTO)imageDTOObj;

            var source = imageDTO.source;
            var mutuelleImage = imageDTO.mutuelleImage;
            var resultat = imageDTO.resultat;

            Image<Bgr, byte> template = new Image<Bgr, byte>(mutuelleImage); // Image A

            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.5)
                {
                    resultat.Add(mutuelleImage);
                }
            }

            Thread.Sleep(0);
        }

        /// <summary>
        /// Get this instance.
        /// </summary>
        /// <returns>The get.</returns>
        public JsonResult<List<string>> Get()
        {
            List<String> resultat = new List<String>();

            var carteFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "CartesSources", "carte_test.png");
            var mutuellesImagesDirectory = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "Mutuelles");
            
            var mutuellesImages = System.IO.Directory.GetFiles(mutuellesImagesDirectory);

            Image<Bgr, byte> source = new Image<Bgr, byte>(carteFilename); // Image B

            List<Thread> listeThreads = new List<Thread>();
            foreach (var mutuelleImage in mutuellesImages)
            {
                ImageDTO imageDTO = new ImageDTO();
                imageDTO.source = source;
                imageDTO.mutuelleImage = mutuelleImage;
                imageDTO.resultat = resultat;

                Thread t = new Thread(new ParameterizedThreadStart(SearchImage));
                t.Start(imageDTO);
                listeThreads.Add(t);
            }

            foreach(var t in listeThreads)
            {
                t.Join();
            }

            return Json(resultat);
        }
    }

    public class ImageDTO
    {
        public Image<Bgr, byte> source { get; set; }
        public string mutuelleImage { get; set; }
        public List<string> resultat { get; set; }
    }
}
