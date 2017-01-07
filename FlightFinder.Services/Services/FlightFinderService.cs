using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;
using SkyScanner.Data;
using SkyScanner.Services;
using SkyScanner.Settings;

namespace FlightFinder.Services.Services
{
    public class FlightFinderService : IFlightFinderService
    {
        public IList<FlightDetails> MapFlightDetails(List<Itinerary> itineraries)
        {
            var flightDetails = new List<FlightDetails> ();
            foreach (var itinerary in itineraries)
            {
                var flightDetail = new FlightDetails();
                var price = itinerary.PricingOptions.Min(option => option.Price);
               
                flightDetail.Price = price;

                flightDetail.OutboundDetails = string.Format("   outbound itinerary at {1} with {0} ({2}-{3})",
                    string.Join(", ",
                    itinerary.OutboundLeg.OperatingCarriers.Select(c => c.Name)),
                    itinerary.OutboundLeg.DepartureTime.TimeOfDay,
                    itinerary.OutboundLeg.Origin.Code,
                    itinerary.OutboundLeg.Destination.Code);

                flightDetail.InboundDetails = string.Format("   inbound itinerary at {1} with {0} ({2}-{3})",
                    string.Join(", ",
                    itinerary.InboundLeg.OperatingCarriers.Select(c => c.Name)),
                    itinerary.InboundLeg.DepartureTime.TimeOfDay,
                    itinerary.InboundLeg.Origin.Code,
                    itinerary.InboundLeg.Destination.Code);

                flightDetails.Add(flightDetail);
            }
            return flightDetails;
        }
    }
}
