using SkyScanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkyScanner.Data.Interim;
using SkyScanner.Settings;

namespace FlightFinder.Services.Services
{
    public interface IFlightFinderService
    {
        IList<FlightDetails> MapFlightDetails(List<Itinerary> itineraries);
    }
}
