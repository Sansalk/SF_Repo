using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlightFinder.Models;
using FlightFinder.Services;
using FlightFinder.Services.Services;
using NodaTime;
using SkyScanner.Services;
using SkyScanner.Settings;

namespace FlightFinder.Controllers
{
    public class FlightFinderController : Controller
    {
        // GET: FlightFinder
        public ActionResult Index()
        {
            var flightDetailModel = new FlightDetailModel
            {
                Adults = 1,
                Children = 0,
                Infants = 0,
                Destination = "New York",
                Origin = "London",
                InboundDate = DateTime.Today.AddDays(2),
                OutboundDate = DateTime.Today

            };

            return View(flightDetailModel);
        }

        // Post : FlightFinder
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Index(FlightDetailModel flightDetails)
        {
            //Validate the input model
            if (!ModelState.IsValid)
            {
                return View(flightDetails);
            }

            IFlightFinderService flightFinderService = new FlightFinderService();

            var scanner = new Scanner(ConfigurationManager.AppSettings["apiKey"]);
            var from = (await scanner.QueryLocation(flightDetails.Origin)).First();
            var to = (await scanner.QueryLocation(flightDetails.Destination)).First();
            

            var outboundDate = new LocalDate(flightDetails.OutboundDate.Year, flightDetails.OutboundDate.Month, flightDetails.OutboundDate.Day);
            var inboundDate = new LocalDate(flightDetails.InboundDate.Year, flightDetails.InboundDate.Month, flightDetails.InboundDate.Day);

            //Query flights
            var itineraries = await scanner.QueryFlight(
                new FlightQuerySettings(
                    new FlightRequestSettings(from, to, outboundDate, inboundDate,flightDetails.Adults,flightDetails.Children,flightDetails.Infants),
                    new FlightResponseSettings(SortType.Price, SortOrder.Ascending))
                );

             itineraries = itineraries.Take(10).ToList();

            var flightList = flightFinderService.MapFlightDetails(itineraries);

            flightDetails.FlightDetails = flightList;

            return View(flightDetails);
        }

    }
}
