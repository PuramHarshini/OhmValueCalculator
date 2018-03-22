using OhmCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OhmCalculator.Controllers
{
    public class BandsController : ApiController
    {
        private IBandEntities bandEntities;

        public BandsController()
        {
            bandEntities = new BandEntities();
        }

        public IEnumerable<Band> GetBands()
        {
            return bandEntities.GetBands();
        }

        public IHttpActionResult GetBand(string color)
        {
            var band = bandEntities.GetBandByColor(color);
            if (band == null)
            {
                return NotFound();
            }
            return Ok(band);
        }

        public HttpResponseMessage Get(string BandA, string BandB, string BandC, string BandD)
        {
            var response= new HttpResponseMessage() { StatusCode =  HttpStatusCode.OK };
            response.Content = new StringContent("OK");
            return response;
        }
    }
}
