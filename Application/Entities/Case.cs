using System;

namespace Application.Entities
{
    public class Case : BaseEntity
    {
        public DateTime ObservationDate { get; set; }

        public string ProvinceState { get; set; }

        public string CountryRegion { get; set; }

        public double Confirmed { get; set; }

        public double Deaths { get; set; }

        public double Recovered { get; set; }
    }
}
