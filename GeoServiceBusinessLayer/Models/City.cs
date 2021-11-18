using GeoServiceAPI.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public class City {
        #region Constructor
        public City(string name, int population) {
            this.Name = name;
            this.Population = population;
        }

        public City() { }
        #endregion

        #region Properties
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int CityId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public ICollection<CityMapping> CityMappings { get; set; }
        #endregion
    }
}
