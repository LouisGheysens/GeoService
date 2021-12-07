using GeoServiceBusinessLayer.Models;
using GeoServiceDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer {
    internal static class DataConverter {

        internal static Continent ConvertContinentDataToContinent(DTContinent data) {
            Continent result = new Continent(data.Name);
            result.Id = data.Id;
            if (data.Countries != null) {
                foreach (DTCountry c in data.Countries) {
                    CreateCountryToAddToContinent(c, result);
                }
            }
            return result;
        }
        internal static Country ConvertCountryDataToCountry(DTCountry data) {

            Continent continent = ConvertContinentDataToContinent(data.Continent);
            Country country = continent.GetCountries().Where(x => x.Id == data.Id).FirstOrDefault();
            return country;
        }
        internal static City ConvertCityDataToCity(DTCity data) {
 

            Country country = ConvertCountryDataToCountry(data.Country);
            return country.GetCities().Where(x => x.Id == data.Id).FirstOrDefault();

        }
        internal static River ConvertRiverDataToRiver(DTRiver data) {
            List<Country> countries = new List<Country>();
            if (data.CountryLink != null) {
                foreach (DTCountryRiver countryRiver in data.CountryLink) {
                    countries.Add(ConvertCountryDataToCountry(countryRiver.Country));
                }
            }
            River result = new River(data.Name, data.Length, countries);
            result.Id = data.Id;
            return result;
        }

        internal static DTContinent ConvertContinentToContinentData(Continent continent) {
            DTContinent result = new DTContinent();
            result.Id = continent.Id;
            result.Name = continent.Name;
            var collection = continent.GetCountries();
            if (collection != null) {
                foreach (Country c in collection) {
                    result.Countries.Add(ConvertCountryToDataCountry(c));
                }
            }

            return result;
        }
        internal static DTCountry ConvertCountryToDataCountry(Country country) {
            DTCountry data = new DTCountry();
            data.ContinentId = country.Continent.Id;
            data.Id = country.Id;
            data.Name = country.Name;
            data.Population = country.Population;
            data.Surface = country.Surface;

            return data;
        }
        internal static DTCity ConvertCityToCityData(City city) {
            DTCity result = new DTCity();
            result.CountryId = city.Country.Id;
            result.Name = city.Name;
            result.Population = city.Population;
            result.Capital = city.Capital;
            return result;
        }

        internal static DTRiver ConvertRiverToRiverData(River river) {
            DTRiver data = new DTRiver();
            data.Id = river.Id;
            data.Length = river.Length;
            data.Name = river.Name;
            foreach (Country country in river.GetCountries()) {
                DTCountryRiver temp = new DTCountryRiver();
                temp.RiverId = data.Id;
                temp.CountryId = country.Id;
                data.CountryLink.Add(temp);
            }
            return data;
        }


        private static Country CreateCountryToAddToContinent(DTCountry country, Continent continent) {
            Country countryResult = new Country(country.Name, country.Population, country.Surface, continent);
            foreach (DTCity city in country.Cities) {
                CreateCityToAddToCountry(city, countryResult);
            }

            countryResult.Id = country.Id;
            return countryResult;
        }

        private static City CreateCityToAddToCountry(DTCity city, Country country) {
            City cityResult = new City(city.Name, city.Population, country, city.Capital);
            cityResult.Id = city.Id;
            return cityResult;
        }
    }
}
