using OhmCalculator.Models;
using System.Web.Mvc;

namespace OhmCalculator.Controllers
{
    public class OhmCalculatorController : Controller
    {
        private IOhmValueCalculator bandEntities;

        public OhmCalculatorController()
        {
            bandEntities = new OhmValueCalculator();            
        }

        /// <summary>
        /// Get the list of band colors
        /// </summary>
        /// <returns>Populates the view</returns>
        public ActionResult Index()
        {
            return View(bandEntities.GetBands());
        }

        /// <summary>
        /// Calulate the resistance value based on band selected band colors
        /// </summary>
        /// <param name="BandA"></param>
        /// <param name="BandB"></param>
        /// <param name="BandC"></param>
        /// <param name="BandD"></param>
        /// <returns>Returns a Json result to populate on the view</returns>
        public JsonResult Calculate(string BandA, string BandB, string BandC, string BandD)
        {
            //Calculate OhmValue here              
            var ohmValueRange = bandEntities.CalculateOhmValue(BandA, BandB, BandC, BandD);

            //return OhmValue as range
            return Json(new { maxValue = ohmValueRange["maxValue"].ToString(),
                              minValue = ohmValueRange["minValue"].ToString()
                            }, JsonRequestBehavior.AllowGet);
        }
    }
}