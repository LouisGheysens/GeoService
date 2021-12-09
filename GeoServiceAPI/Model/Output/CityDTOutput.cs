using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Output {
    public class CityDTOutput {
        public string CityId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public string Country { get; set; }

        public bool Capital { get; set; }
    }
}
