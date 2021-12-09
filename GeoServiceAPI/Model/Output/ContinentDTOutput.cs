using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Output {
    public class ContinentDTOutput {
        public string ContinentId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public string[] Countries { get; set; }
    }
}
