using GeoServiceBusinessLayer.Interfaces;
using GeoServiceDataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer {
    public class DataAcces: InterfaceCollection {

        private CountryContext Context;
        public DataAcces(string db = "Production") {
            Context = new CountryContext(db);
            Cities = new CityRepository(Context);
            Continents = new ContinentRepository(Context);
            Countries = new CountryRepository(Context);
            Rivers = new RiverRepository(Context);
        }
        public ICityRepository Cities { get; set; }
        public IContinentRepository Continents { get; set; }
        public ICountryRepository Countries { get; set; }
        public IRiverRepository Rivers { get; set; }
    }
}
