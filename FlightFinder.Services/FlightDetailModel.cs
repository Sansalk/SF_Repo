using System;
using System.Collections.Generic;
using NodaTime;
using SkyScanner.Data;
using System.ComponentModel.DataAnnotations;

namespace FlightFinder.Services
{
    public class FlightDetailModel
    {
        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OutboundDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime InboundDate { get; set; }

        public int Adults { get; set; }

        public int Children { get; set; }

        public int Infants { get; set; }
        public int CityFrom { get; set; }

        public IList<FlightDetails> FlightDetails { get; set; }
    }
    public enum CityFrom
    {
        Frankfurt,
        Durban,
        London,
        Dubai
    }

    public enum CityTo
    {
        Frankfurt,
        Durban,
        London,
        Dubai
    }
}