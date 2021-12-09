using GeoServiceAPP;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.DatabaseTesting {
    public class Test_Data_City {

        private TestDataAcces GetConnection() {
            return new TestDataAcces();
        }

        private Continent GetTestContinent(TestDataAcces acc) {
            Continent continent = new Continent("Europe");
            return acc.Continents.AddContinent(continent);
        }

        public Country GetTestCountry(TestDataAcces Data) {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("Netherlands", 23999, 567890, continent);
            return Data.Countries.AddCountry(country);
        }

        private City GetTestCity(TestDataAcces Data) {
            Country country = GetTestCountry(Data);
            string name = "Gent";
            int population = 34;
            bool capital = true;
            City city = new City(name, population, country, capital);
            return Data.Cities.AddCity(city);
        }

        [Fact]
        public void Test_AddCity_Valid() {
            var data = GetConnection();
            string name = "Gent";
            int population = 2000;
            bool capital = true;
            Country country = GetTestCountry(data);
            City ct = new City(name, population, country, capital);
            var rs = data.Cities.AddCity(ct);
            var rsTwo = data.Cities.GetCityById(1);
            Assert.True(rs.Equals(rsTwo));
        }

        [Fact]
        public void Test_DeleteCityValid() {
            var data = GetConnection();
            City c = GetTestCity(data);
            data.Cities.Delete(1);
            var result = data.Cities.GetCityById(1);

            Assert.True(result == null);
        }

        [Fact]
        public void Test_UpdateCity_ShouldUpdateValid() {
            var data = GetConnection();
            City addedCity = GetTestCity(data);
            string newName = "Waastmezel";
            Country newCountry = GetTestCountry(data);
            addedCity.Name = newName;
            addedCity.Capital = false;
            addedCity.Population = 123;
            addedCity.Country = newCountry;
            data.Cities.Update(addedCity);
            City updatedCity = data.Cities.GetCityById(1);
            Assert.True(updatedCity.Id == 1);
            Assert.True(updatedCity.Name == "Waastmezel");
            Assert.True(updatedCity.Capital == false);
            Assert.True(updatedCity.Population == 123);
            Assert.True(updatedCity.Country.Equals(newCountry));
        }

        [Fact]
        public void Test_FullSetOfCity() {
            var data = GetConnection();
            City d = GetTestCity(data);

            Assert.True(d.Country != null);
            Assert.True(d.Country.Continent != null);
        }

    }

}

