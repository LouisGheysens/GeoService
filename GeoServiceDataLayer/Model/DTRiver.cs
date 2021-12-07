using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Model {
    public class DTRiver {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public ICollection<DTCountryRiver> CountryLink { get; set; } = new HashSet<DTCountryRiver>();
    }
}
