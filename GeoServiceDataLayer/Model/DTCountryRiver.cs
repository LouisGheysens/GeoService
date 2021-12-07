using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Model {
    public class DTCountryRiver {
        public int CountryId { get; set; }
        public DTCountry Country { get; set; }
        public int RiverId { get; set; }
        public DTRiver River { get; set; }
    }
}
