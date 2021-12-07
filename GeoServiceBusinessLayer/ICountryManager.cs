using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer {
    public interface ICountryManager {
        City AddCity(string name, int population, Country country, bool capital = false);
        Continent AddContinent(string name);
        Country AddCountry(string name, int population, int surfaceArea, Continent continent);
        River AddRiver(string name, int length, List<Country> countries);
        void DeleteCity(int cityId);
        void DeleteContinent(int continentId);
        void DeleteCountry(int countryId);
        void DeleteRiver(int riverId);
        City GetCityForId(int id);
        Continent GetContinentForId(int id);
        Country GetCountryForId(int id);
        River GetRiverForId(int id);
        City UpdateCity(City city);
        Continent UpdateContinent(Continent continent);
        Country UpdateCountry(Country country);
        River UpdateRiver(River river);
    }
}
