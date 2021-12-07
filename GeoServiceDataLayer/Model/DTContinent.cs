using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Model {
    public class DTContinent {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DTCountry> Countries { get; set; } = new HashSet<DTCountry>();
    }
}
