using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Managers {
    public class CountryManager {
        private readonly InterfaceCollection _icollection;

        public CountryManager(InterfaceCollection _icollection) {
            this._icollection = _icollection;
        }

        public Country add(Country country) {
            if (_icollection.Countries.exists(country)) throw new CountryManagerException("CountryManager: add - country doesn't exist");
            try {
                _icollection.Continents.update(country.Continent);
                _icollection.Complete();
                return _icollection.Countries.getAll().Last();
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: add - failed", ex);
            }
        }

        public Country Get(int id) {
            try {
                return _icollection.Countries.getById(id);
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: Get - failed", ex);
            }
        }

        public IEnumerable<Country> getAll() {
            try {
                return _icollection.Countries.getAll();
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: getAll - failed", ex);
            }
        }

        public void delete(Country country) {
            try {
                if (country.Cities.Count != 0) throw new CountryManagerException("CountryManager: delete - there are no cities found");
                country.Continent.removeCountry(country);
                _icollection.Continents.update(country.Continent);
                _icollection.Countries.delete(country);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: delete - failed", ex);
            }
        }

        public void deleteAll() {
            try {
                _icollection.Countries.deleteAll();
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: deleteAll - failed", ex);
            }
        }

        public void update(Country country) {
            try {
                _icollection.Countries.update(country);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CountryManagerException("CountryManager: update - failed", ex);
            }
        }

        public bool exist(Country country) {
            return _icollection.Countries.exists(country);
        }







    }
}
