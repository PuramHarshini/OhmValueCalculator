using OhmCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OhmCalculator.Controllers
{
    public class OhmCalculatorController : Controller
    {
        private IBandEntities bandEntities;
        public OhmCalculatorController()
        {
            bandEntities = new BandEntities();
        }
        // GET: OhmCalculator
        public ActionResult Index()
        {
            return View(bandEntities.GetBands());
        }

        public JsonResult Calculate(string BandA, string BandB, string BandC, string BandD)
        {
            //Calculate OhmValue here
            var ohmValue = ((bandEntities.GetSignificantFigureByColor(BandA) * 10) + (bandEntities.GetSignificantFigureByColor(BandB))) * (bandEntities.GetMultiPlierByColor(BandC));
            var tolerance = bandEntities.GetToleranceByColor(BandD);
                       
            var maxValue = ohmValue + (ohmValue * tolerance)/100;
            var minValue = ohmValue - (ohmValue * tolerance)/100;
            return Json(new { maxValue = maxValue, minValue= minValue }, JsonRequestBehavior.AllowGet);

        }
    }
}