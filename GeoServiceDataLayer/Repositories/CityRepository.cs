using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Repositories {
    public class CityRepository : ICityRepository {
        protected DataContext context;

        public CityRepository(DataContext context) {
            this.context = context;
        }

        public City addCity(City city) {
            try {
                this.context.Cities.Add(city);
                return city;
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: addCity - gefaald", ex);
            }
        }

        public void delete(City city) {
            try {
                this.context.Cities.Remove(city);
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: delete - gefaald", ex);
            }
        }

        public void deleteAll() {
            try {
                this.context.Cities.RemoveRange(context.Cities);
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: deleteAll - gefaald", ex);
            }
        }

        public bool exists(City city) {
            try {
                return this.context.Cities.Contains(city);
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: exists - gefaald", ex);
            }
        }

        public IEnumerable<City> getAll() {
            try {
                return this.context.Cities
                    .Include(City => City.Country).ThenInclude(country => country.Continent)
                    .Include(city => city.Country).ThenInclude(country => country.Cities)
                    .Include(city => city.Country).ThenInclude(country => country.Capitals)
                    .Include(city => city.Country).ThenInclude(country => country.Rivers).ToList<City>();
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: getAll - gefaald", ex);
            }
        }

        public City getCityById(int id) {
            try {
                return this.context.Cities
                    .Include(city => city.Country).ThenInclude(country => country.Continent)
                    .Include(city => city.Country).ThenInclude(country => country.Cities)
                    .Include(city => city.Country).ThenInclude(country => country.Capitals)
                    .Include(city => city.Country).ThenInclude(country => country.Rivers).Where(c => c.Id == id).SingleOrDefault();
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: getCityById(int id) - gefaald", ex);
            }
        }

        public void update(City city) {
            try {
                this.context.Cities.Update(city);
            }catch(Exception ex) {
                throw new CityRepositoryException("CityRepository: update - gefaald", ex);
            }
        }
    }
}
