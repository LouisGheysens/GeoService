using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Managers {
    public class CityManager {

        private readonly InterfaceCollection _icollection;

        public CityManager(InterfaceCollection _icollection) {
            this._icollection = _icollection;
        }

        public City Add(City city) {
            if (_icollection.Cities.exists(city)) throw new CityManagerException("CityManager: add - city allready exists");
            try {
                _icollection.Countries.update(city.Country);
                _icollection.Complete();
                return _icollection.Cities.getAll().Last();
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: add - failed", ex);
            }
        }

        public City Get(int id) {
            try {
                return _icollection.Cities.getCityById(id);
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: Get - failed", ex);
            }
        }

        public IEnumerable<City> getAll() {
            try {
                return _icollection.Cities.getAll();
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: getAll - failed", ex);
            }
        }

        public void Delete(City city) {
            try {
                _icollection.Cities.delete(city);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: delete - failed", ex);
            }
        }

        public void deleteAll() {
            try {
                _icollection.Cities.deleteAll();
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: deleteAll - failed", ex);
            }
        }

        public void update(City city) {
            try {
                _icollection.Cities.update(city);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new CityManagerException("CityManager: update - failed", ex);
            }
        }

        public bool exists(City city) {
            return _icollection.Cities.exists(city);
        }

    }
}
