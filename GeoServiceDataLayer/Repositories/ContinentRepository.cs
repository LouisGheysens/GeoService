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
    public class ContinentRepository : IContinentRepository {

        protected CountryContext context;

        public ContinentRepository(CountryContext context) {
            this.context = context;
        }

        public Continent AddContinent(Continent continent) {
            DTContinent dt = DataConverter.ConvertContinentToContinentData(continent);
            context.Continents.Add(dt);
            context.SaveChanges();
            return DataConverter.ConvertContinentDataToContinent(dt);
        }

        public void Delete(int continentId) {
            DTContinent dt = GetContinentDataForRetrievedId(continentId);
            context.Remove(dt);
            context.SaveChanges();
        }

        //Hulp methode voor het ontvangen van de ID
        private DTContinent GetContinentDataForRetrievedId(int id) {
            return context.Continents.Where(x => x.Id == id)
                .Include(x => x.Countries)
                .ThenInclude(x => x.Cities)
                .FirstOrDefault();
        }

        public Continent GetContinentById(int id) {
            DTContinent dt = GetContinentDataForRetrievedId(id);
            if(dt == null) {
                return null;
            }
            else {
                return DataConverter.ConvertContinentDataToContinent(dt);
            }
        }

        public bool IsNameAvailable(string name) {
            return !context.Continents.Any(x => x.Name == name);
        }

        //Hulp methode voor update
        private void UpdateContinentHelper(DTContinent dtOne, DTContinent dtTwo) {
            dtOne.Countries = dtTwo.Countries;
            dtOne.Name = dtTwo.Name;
        }

        public Continent Update(Continent continent) {
            DTContinent dt = DataConverter.ConvertContinentToContinentData(continent);
            DTContinent dtSecond = context.Continents.Find(dt.Id);
            UpdateContinentHelper(dt, dtSecond);
            context.Continents.Update(dtSecond);
            context.SaveChanges();
            return DataConverter.ConvertContinentDataToContinent(dtSecond);
        }
    }
}
