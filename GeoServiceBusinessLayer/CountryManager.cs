using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer {
    public class CountryManager : ICountryManager {

        public InterfaceCollection Data { get; set; }

        public CountryManager(InterfaceCollection dataAcces) {
            Data = dataAcces;
        }
        public City AddCity(string name, int population, Country country, bool capital = false) {
            throw new NotImplementedException();
        }

        public Continent AddContinent(string name) {
            if (Data.Continents.IsNameAvailable(name)) {
                Continent continent = new Continent(name);

                Continent result = Data.Continents.AddContinent(continent);
                return result;
            }
            else throw new ContinentException("CountryManager: A Continent's name must be unique.");
        }

        public Country AddCountry(string name, int population, int surfaceArea, Continent continent) {
            Country country = new Country(name, population, surfaceArea, continent);
            Country result = Data.Countries.AddCountry(country);
            return result;
        }

        public River AddRiver(string name, int length, List<Country> countries) {
            River river = new River(name, length, countries);

            River result = Data.Rivers.AddRiver(river);
            return result;
        }

        public void DeleteCity(int cityId) {
            Data.Cities.Delete(cityId);
        }

        public void DeleteContinent(int continentId) {
            Continent continent = GetContinentForId(continentId);
            if (continent.GetCountries().Count != 0)
                throw new ContinentException("CountryManager: All countries within a continent must be deleted before the continent " +
                    "can be deleted");
            Data.Continents.Delete(continentId);
        }

        public void DeleteCountry(int countryId) {
            Country country = GetCountryForId(countryId);
            if (country.GetCities().Count != 0)
                throw new CountryException("CountryManager: " +
                    "All cities within a country must be deleted before " +
                    "the country can be deleted");
            Data.Countries.Delete(countryId);
        }

        public void DeleteRiver(int riverId) {
            Data.Rivers.Delete(riverId);
        }

        public City GetCityForId(int id) {
            City city = Data.Cities.GetCityById(id);
            if (city != null)
                return city;
            else throw new CityException("CountryManager: " +
                "No city with the given Id exists.");
        }

        public Continent GetContinentForId(int id) {
            Continent continent = Data.Continents.GetContinentById(id);
            if (continent != null)
                return continent;
            else throw new ContinentException("CountryManager: " +
                "No continent with the given Id exists.");
        }

        public Country GetCountryForId(int id) {
            Country country = Data.Countries.GetCountryById(id);
            if (country != null)
                return country;
            else throw new CountryException("CountryManager: " +
                "No Country with the given Id exists.");
        }

        public River GetRiverForId(int id) {
            River river = Data.Rivers.GetRiverById(id);
            if (river != null)
                return river;
            else throw new RiverException("CountryManager: No river with " +
                "the given Id exists.");
        }

        public City UpdateCity(City city) {
            City updated = Data.Cities.Update(city);
            return updated;
        }

        public Continent UpdateContinent(Continent continent) {
            Continent updated = Data.Continents.Update(continent);
            return updated;
        }

        public Country UpdateCountry(Country country) {
            Country updated = Data.Countries.Update(country);
            return updated;
        }

        public River UpdateRiver(River river) {
            River updated = Data.Rivers.UpdateRiver(river);
            return updated;
        }
    }
}
