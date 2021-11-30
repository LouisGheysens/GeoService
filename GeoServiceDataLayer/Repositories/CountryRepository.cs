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
    public class CountryRepository : ICountryRepository {

        protected DataContext context;

        public CountryRepository(DataContext context) {
            this.context = context;
        }


        public Country addCountry(Country country) {
            try {
                this.context.Countries.Add(country);
                return country;
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: addCountry - gefaald", ex);
            }
        }

        public void delete(Country country) {
            try {
                this.context.Countries.Remove(country);
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: delete - gefaald", ex);
            }
        }

        public void deleteAll() {
            try {
                this.context.Countries.RemoveRange(context.Countries);
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: deleteAll - gefaald", ex);
            }
        }

        public bool exists(Country country) {
            try {
                return this.context.Countries.Contains(country);
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: exists - gefaald", ex);
            }
        }

        public IEnumerable<Country> getAll() {
            try {
                return this.context.Countries
                    .Include(country => country.Continent).ThenInclude(continent => continent.Countries)
                    .Include(country => country.Cities).ThenInclude(city => city.Country)
                    .Include(country => country.Capitals).ThenInclude(city => city.Country)
                    .Include(country => country.Rivers).ThenInclude(river => river.Countries).ToList<Country>();
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: getAll - gefaald", ex);
            }
        }

        public Country getById(int id) {
            try {
                return this.context.Countries
                    .Include(country => country.Continent).ThenInclude(continent => continent.Countries)
                    .Include(country => country.Cities).ThenInclude(city => city.Country)
                    .Include(country => country.Capitals).ThenInclude(capital => capital.Country)
                    .Include(country => country.Rivers).ThenInclude(river => river.Countries).Where(c => c.Id == id).SingleOrDefault();
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: getById(int id) gefaald", ex);
            }
        }

        public void update(Country country) {
            try {
                this.context.Countries.Update(country);
            }catch(Exception ex) {
                throw new CountryRepositoryException("CountryRepository: update - gefaald", ex);
            }
        }
    }
}
