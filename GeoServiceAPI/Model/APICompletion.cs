using GeoServiceAPI.Interfaces;
using GeoServiceAPI.Model.Input;
using GeoServiceAPI.Model.Output;
using GeoServiceBusinessLayer;
using GeoServiceBusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace GeoServiceAPI.Model {

    public class APICompletion : IApiCompletion {
        private ICountryManager countryM;
        private DTOConverter dtoConverting;

        public APICompletion(ICountryManager ic, IConfiguration config) {
            countryM = ic;
            dtoConverting = new DTOConverter(config);
        }

        public CityDTOutput AddCity(CityDTOInput city) {
            Country countryElement = countryM.GetCountryForId(city.CountryId);
            City res = countryM.AddCity(city.Name, city.Population, countryElement, city.Capital);
            return DTOConverter.ConvertCityToDTOOut(res);
        }

        public ContinentDTOutput AddContinent(ContinentDTOInput continent) {
            Continent continenten = countryM.AddContinent(continent.Name);
            return DTOConverter.ConvertContinentToDTOOut(continenten);
        }

        public CountryDTOutput AddCountry(CountryDTOInput country) {
            Continent elementOne = countryM.GetContinentForId(country.ContinentId);
            Country rs = countryM.AddCountry(country.Name, country.Population, country.Surface, elementOne);
            return DTOConverter.ConvertCountryToDTOOut(rs);
        }

        public RiverDTOutput AddRiver(RiverDTOInput rivier) {
            List<Country> countries = new List<Country>();
            foreach (int id in rivier.CountryIdArray) {
                countries.Add(countryM.GetCountryForId(id));
            }
            River rs = countryM.AddRiver(rivier.Name, rivier.Length, countries);
            return DTOConverter.ConvertRiverToDTOOut(rs);
        }

        public void DeleteCity(int cityId) {
            countryM.DeleteCity(cityId);
        }

        public void DeleteContinent(int id) {
            countryM.DeleteContinent(id);
        }

        public void DeleteCountry(int countryId) {
            countryM.DeleteCountry(countryId);
        }

        public void DeleteRiver(int id) {
            countryM.DeleteRiver(id);
        }

        public CityDTOutput GetCityForId(int id) {
            City result = countryM.GetCityForId(id);
            return DTOConverter.ConvertCityToDTOOut(result);
        }

        public ContinentDTOutput GetContinentForId(int id) {
            Continent result = countryM.GetContinentForId(id);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOutput GetCountryForId(int id) {
            Country csr = countryM.GetCountryForId(id); ;
            return DTOConverter.ConvertCountryToDTOOut(csr);
        }

        public RiverDTOutput GetRiverForId(int id) {
            River rv = countryM.GetRiverForId(id);
            return DTOConverter.ConvertRiverToDTOOut(rv);
        }

        public CityDTOutput UpdateCity(CityDTOInput city) {
            City original = countryM.GetCityForId(city.CityId);
            original.Population = city.Population;
            original.Name = city.Name;
            original.Capital = city.Capital;

            City result = countryM.UpdateCity(original);
            return DTOConverter.ConvertCityToDTOOut(result);
        }

        public ContinentDTOutput UpdateContinent(ContinentDTOInput continent) {
            Continent original = countryM.GetContinentForId(continent.ContinentId);
            original.Name = continent.Name;
            Continent result = countryM.UpdateContinent(original);
            return DTOConverter.ConvertContinentToDTOOut(result);
        }

        public CountryDTOutput UpdateCountry(CountryDTOInput countryIn) {
            Country original = countryM.GetCountryForId(countryIn.CountryId);
            original.Population = countryIn.Population;
            Continent cont = countryM.GetContinentForId(countryIn.ContinentId);
            if (!original.Continent.Equals(cont))
                original.Continent = countryM.GetContinentForId(countryIn.ContinentId);
            original.Name = countryIn.Name;
            original.Surface = countryIn.Surface;

            Country result = countryM.UpdateCountry(original);
            return DTOConverter.ConvertCountryToDTOOut(result);
        }

        public RiverDTOutput UpdateRiver(RiverDTOInput rivier) {
            River original = countryM.GetRiverForId(rivier.RiverId);
            original.Length = rivier.Length;
            original.Name = rivier.Name;
            List<Country> countries = new List<Country>();
            foreach (int id in rivier.CountryIdArray) {
                countries.Add(countryM.GetCountryForId(id));
            }
            original.SetCountries(countries);
            River result = countryM.UpdateRiver(original);
            return DTOConverter.ConvertRiverToDTOOut(result);
        }

        ContinentDTOutput IApiCompletion.GetCountryForId(int id) {
            throw new System.NotImplementedException();
        }

        ContinentDTOutput IApiCompletion.UpdateCountry(CountryDTOInput countryIn) {
            throw new System.NotImplementedException();
        }
    }
}