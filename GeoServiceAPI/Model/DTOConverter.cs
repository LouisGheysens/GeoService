using GeoServiceAPI.Model.Output;
using GeoServiceBusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model {
    public class DTOConverter {


        public DTOConverter(IConfiguration config) {
            CreateHostString(config);
        }
        private static string HostString;
        

        //Thinking of a collection loop in different stages
        public static ContinentDTOutput ConvertContinentToDTOOut(Continent continent) {
            ContinentDTOutput result = new ContinentDTOutput();
            result.Name = continent.Name;
            result.Population = continent.GetPopulation();
            result.ContinentId = CreateContinentIdString(continent.Id);
            var countries = continent.GetCountries();
            string[] countryStrings = new string[countries.Count];
            for (int i = 0; i < countries.Count; i++) {
                countryStrings[i] = CreateCountryIdString(continent.Id, countries[i].Id);
            }

            result.Countries = countryStrings;
            return result;
        }
        public static CityDTOutput ConvertCityToDTOOut(City city) {
            CityDTOutput result = new CityDTOutput();
            result.Name = city.Name;
            result.CityId = CreateCityIdString(city.Country.Continent.Id, city.Country.Id, city.Id);
            result.Population = city.Population;
            result.Capital = city.Capital;

            result.Country = CreateCountryIdString(city.Country.Continent.Id, city.Country.Id);
            return result;
        }
        public static CountryDTOutput ConvertCountryToDTOOut(Country country) {
            CountryDTOutput result = new CountryDTOutput();
            result.Continent = CreateContinentIdString(country.Continent.Id);
            result.Name = country.Name;
            result.Population = country.Population;
            result.Surface = country.Surface;
            result.CountryId = CreateCountryIdString(country.Continent.Id, country.Id);

            var capitals = country.GetCapitals();
            string[] capitalStrings = new string[capitals.Count];
            for (int i = 0; i < capitals.Count; i++) {
                capitalStrings[i] = CreateCityIdString(country.Continent.Id, country.Id, capitals.ElementAt(i).Id);
            }
            result.Capitals = capitalStrings;

            var cities = country.GetCities();
            string[] CityStrings = new string[cities.Count];
            for (int i = 0; i < cities.Count; i++) {
                CityStrings[i] = CreateCityIdString(country.Continent.Id, country.Id, cities.ElementAt(i).Id);
            }
            result.Cities = CityStrings;

            return result;
        }
        public static RiverDTOutput ConvertRiverToDTOOut(River river) {
            RiverDTOutput result = new RiverDTOutput();
            result.RiverId = CreateRiverIdString(river.Id);
            result.Name = river.Name;
            result.Length = river.Length;


            var countries = river.GetCountries();
            string[] countryStrings = new string[countries.Count];
            for (int i = 0; i < countries.Count; i++) {
                countryStrings[i] = CreateCountryIdString(countries[i].Continent.Id, countries[i].Id);
            }
            result.Countries = countryStrings;
            return result;
        }


        //API CONNECTION STRINGS TO SETUP
        private static void CreateHostString(IConfiguration iConfiguration) {
            HostString = iConfiguration.GetValue<string>("iisSettings:IISExpress:applicationUrl");
        }
        private static string CreateContinentIdString(int continentId) {
            return HostString + @"/api/continent/" + continentId;
        }
        private static string CreateCityIdString(int continentId, int countryId, int cityId) {
            return HostString + @"/api/continent/" + continentId + "/Country/" + countryId + "/City/" + cityId;
        }
        private static string CreateCountryIdString(int continentId, int countryId) {
            return HostString + @"/api/continent/" + continentId + "/Country/" + countryId;
        }
        private static string CreateRiverIdString(int riverId) {
            return HostString + @"/api/river/" + riverId;
        }
    }
}
