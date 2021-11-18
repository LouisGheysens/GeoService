using GeoServiceAPI.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public class Continent {
        #region Constructor
        public Continent(string name) {
            this.Name = name;
        }

        public Continent() { }
        #endregion

        #region Properties
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int ContinentId { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public ICollection<CountryMapping> CountryMappings { get; set; }
        #endregion
    }
}
