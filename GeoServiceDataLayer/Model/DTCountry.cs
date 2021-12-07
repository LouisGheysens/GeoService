using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Model {
    public class DTCountry {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public int Surface { get; set; }
        public int ContinentId { get; set; }
        public DTContinent Continent { get; set; }
        public ICollection<DTCountryRiver> Rivers { get; set; }
        public ICollection<DTCity> Cities { get; set; }
    }
}
