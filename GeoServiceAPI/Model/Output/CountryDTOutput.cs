using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Output {
    public class CountryDTOutput {
        public string CountryId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int Surface { get; set; }

        public string Continent { get; set; }

        public string[] Capitals { get; set; }

        public string[] Cities { get; set; }
    }
}
