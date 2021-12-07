using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using GeoServiceDataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Repositories {
    public class CityRepository : ICityRepository {
        protected CountryContext context;

        public CityRepository(CountryContext context) {
            this.context = context;
        }

        public City AddCity(City city) {
            DTCity dt = DataConverter.ConvertCityToCityData(city);
            context.Cities.Add(dt);
            context.SaveChanges();
            return DataConverter.ConvertCityDataToCity(dt);
        }

        public void Delete(int city) {
            DTCity rstdt = context.Cities.Find(city);
            context.Remove(rstdt);
            context.SaveChanges();
        }

        public City GetCityById(int id) {
            DTCity rs = GetDataCityForRetrievingId(id);
            if(rs == null) {
                return null;
            }
            else {
                return DataConverter.ConvertCityDataToCity(rs);
            }
        }

        //Lambda expressie voor het vergemakkelijken van de methode met param(id)
        private DTCity GetDataCityForRetrievingId(int id) {
            return context.Cities
                .Where(x => x.Id
                == id)
                .Include(x =>
                x.Country)
                .ThenInclude(x => x.Continent)
                .FirstOrDefault();
        }

        private void UpdateCityHelper(DTCity cOne, DTCity cTwo) {
            cOne.CountryId = cTwo.CountryId;
            cOne.Name = cTwo.Name;
            cOne.Population = cTwo.Population;
            cOne.Capital = cTwo.Capital;
        }

        public City Update(City cityId) {
            DTCity newCity = DataConverter.ConvertCityToCityData(cityId);
            DTCity originalCity = GetDataCityForRetrievingId(cityId.Id);
            UpdateCityHelper(newCity, originalCity);
            context.Cities.Update(originalCity);
            context.SaveChanges();
            return DataConverter.ConvertCityDataToCity(originalCity);
        }
    }
}
