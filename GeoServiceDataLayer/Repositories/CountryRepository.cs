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
    public class CountryRepository : ICountryRepository {

        protected CountryContext context;

        public CountryRepository(CountryContext context) {
            this.context = context;
        }

        public Country AddCountry(Country country) {
            DTCountry dt = DataConverter.ConvertCountryToDataCountry(country);
            context.Countries.Add(dt);
            context.SaveChanges();
            return GetCountryById(dt.Id);
        }

        public void Delete(int countryId) {
            DTCountry dt = context.Countries.Find(countryId);
            context.Countries.Remove(dt);
            context.SaveChanges();
        }

        public Country GetCountryById(int id) {
            DTCountry dtc = GetCountryForDefienedId(id);
            if (dtc == null)
                return null;
            else
                return DataConverter.ConvertCountryDataToCountry(dtc);
        }

        //Hulpfunctie id
        private DTCountry GetCountryForDefienedId(int id) {
            return context.Countries.Where(x => x.Id == id)
                .Include(x => x.Continent)
                .Include(x => x.Cities)
                .Include(x => x.Rivers)
                .ThenInclude(x => x.River)
                .FirstOrDefault();
        }

        public Country Update(Country country)  {
            DTCountry DataCountry = DataConverter.ConvertCountryToDataCountry(country);
            DTCountry OriginalCountry = GetCountryForDefienedId(country.Id);
            OriginalCountry.Name = DataCountry.Name;
            OriginalCountry.Population = DataCountry.Population;
            OriginalCountry.Surface = DataCountry.Surface;
            OriginalCountry.ContinentId = DataCountry.ContinentId;
            context.Countries.Update(OriginalCountry);
            context.SaveChanges();
            return DataConverter.ConvertCountryDataToCountry(OriginalCountry);
        }
    }
    }
