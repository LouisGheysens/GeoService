using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Input {
    public class CountryDTOInput {
        public int CountryId { get; set; }

        public int ContinentId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int Surface { get; set; }
    }
}
