using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceDataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer {
    public class CollectionDriver : InterfaceCollection {
        protected DataContext datacontext;

        public CollectionDriver(DataContext context) {
            this.datacontext = context;
            this.Cities = new CityRepository(context);
            this.Continents = new ContinentRepository(context);
            this.Countries = new CountryRepository(context);
            this.Rivers = new RiverRepository(context); ;
        }

        public ICityRepository Cities { get; private set;}

        public IContinentRepository Continents { get; private set; }

        public ICountryRepository Countries { get; private set; }

        public IRiverRepository Rivers { get; private set; }


        public int Complete() {
            try {
                return datacontext.SaveChanges();
            }catch(Exception ex) {
                throw new CollectionDriverException("CollectionDriver: complete - gefaald", ex);
            }
        }

        public void Dispose() {
            datacontext.Dispose();
        }
    }
}
