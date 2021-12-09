using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Input {
    public class CityDTOInput {
        public int CityId { get; set; }

        public string Name { get; set; }

        public bool Capital { get; set; }

        public int Population { get; set; }

        public int CountryId { get; set; }

        public int ContinentId { get; set; }
    }
}
