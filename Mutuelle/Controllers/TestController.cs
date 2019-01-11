using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace rest.Controllers
{
    public class TestController : ApiController
    {

        public static void SearchImage(object ThreadDTOObj)
        {
            ThreadDTO ThreadDTO = (ThreadDTO)ThreadDTOObj;

            var source = ThreadDTO.source;
            var mutuelleImage = ThreadDTO.mutuelleImage;
            var imageResultat = ThreadDTO.imageResultat;
            var resultat = ThreadDTO.resultat;

            Image<Bgr, byte> template = new Image<Bgr, byte>(mutuelleImage); // Image A

            using (Image<Gray, float> result = source.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed))
            {
                double[] minValues, maxValues;
                Point[] minLocations, maxLocations;
                result.MinMax(out minValues, out maxValues, out minLocations, out maxLocations);

                // You can try different values of the threshold. I guess somewhere between 0.75 and 0.95 would be good.
                if (maxValues[0] > 0.5)
                {
                    Rectangle match = new Rectangle(maxLocations[0], template.Size);
                    imageResultat.Draw(match, new Bgr(Color.Red), 3);

                    resultat.Add(mutuelleImage);
                }
            }

            Thread.Sleep(0);
        }

        public static string EmguImageToBase64(Emgu.CV.Image<Bgr, byte> image)
        {
            Bitmap bitmap = new Bitmap(image.Bitmap);
            System.IO.MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            return SigBase64;
        }

        public static Emgu.CV.Image<Bgr, Byte> Base64ToEmguImage(string base64)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                
                Bitmap bmpImage = new Bitmap(image);
                Emgu.CV.Image<Bgr, Byte> imageOut = new Emgu.CV.Image<Bgr, Byte>(bmpImage);

                return imageOut;
            }
        }

        /// <summary>
        /// Get this instance.
        /// </summary>
        /// <returns>The get.</returns>
        public JsonResult<string> Post([FromBody] dynamic value)
        {
            List<String> resultat = new List<String>();

            //Récupérée à partir des données POST
            string sourceBase64 = (string)value.image;
            //Récupérée du disque - Tests
            var carteFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "CartesSources", "carte_test.png");

            var mutuellesImagesDirectory = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Assets", "Mutuelles");
            var mutuellesImages = System.IO.Directory.GetFiles(mutuellesImagesDirectory);

            //Image<Bgr, byte> source = new Image<Bgr, byte>(carteFilename);
            Image<Bgr, byte> source = Base64ToEmguImage(sourceBase64);
            Image<Bgr, byte> imageResultat = source.Copy();

            List<Thread> listeThreads = new List<Thread>();
            foreach (var mutuelleImage in mutuellesImages)
            {
                ThreadDTO ThreadDTO = new ThreadDTO();
                ThreadDTO.source = source;
                ThreadDTO.mutuelleImage = mutuelleImage;
                ThreadDTO.imageResultat = imageResultat;
                ThreadDTO.resultat = resultat;

                Thread t = new Thread(new ParameterizedThreadStart(SearchImage));
                t.Start(ThreadDTO);
                listeThreads.Add(t);
            }

            foreach(var t in listeThreads)
            {
                t.Join();
            }

            ResultatDTO resultatDTO = new ResultatDTO();
            resultatDTO.imageResultat = EmguImageToBase64(imageResultat);
            resultatDTO.mutuelles = resultat;

            string json = new JavaScriptSerializer().Serialize(resultatDTO);
            return Json(json);
           // return Json(resultatDTO);
        }
    }

    public class ThreadDTO
    {
        public Image<Bgr, byte> source { get; set; }
        public string mutuelleImage { get; set; }
        public Image<Bgr, byte> imageResultat { get; set; }
        public List<string> resultat { get; set; }
    }

    public class ResultatDTO
    {
        public string imageResultat { get; set; }
        public List<string> mutuelles { get; set; }
    }
}
