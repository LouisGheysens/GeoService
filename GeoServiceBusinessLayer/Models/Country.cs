using GeoServiceAPI.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public partial class Country {
        #region Constructor
        public Country(string name, int surface) {
            this.Name = name;
            this.Surface = surface;
        }

        public Country() { }
        #endregion

        #region Properties
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public int Surface { get; set; }

        /// <summary>
        /// Deze date wordt niet meegenomen naar de api door de eventuele mapping tijdens het proces = ignore
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<CityMapping> CityMappings { get; set; }
        [JsonIgnore]
        public virtual ICollection<CountryMapping> CountryMappings { get; set; }
        public virtual ICollection<RiverMapping> RiverMappings { get; set; }
        #endregion
    }
}
