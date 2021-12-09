using GeoServiceAPP;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.DatabaseTesting {
    public class Test_Data_River {

        private TestDataAcces GetConnection() {
            return new TestDataAcces();
        }
        private Continent GetTestContinent(TestDataAcces Data) {
            Continent continent = new Continent("Asia");
            return Data.Continents.AddContinent(continent);
        }
        public Country GetTestCountry(TestDataAcces Data) {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("Taiwan", 15000, 12000, continent);
            return Data.Countries.AddCountry(country);
        }
        private City GetTestCity(TestDataAcces Data) {
            Country country = GetTestCountry(Data);
            string name = "Xhionping";
            int population = 234560;
            bool capital = true;
            City city = new City(name, population, country, capital);
            return Data.Cities.AddCity(city);
        }
        private River GetTestRiver(TestDataAcces data) {
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "Umapola";
            int length = 124;
            River river = new River(name, length, countries);
            return data.Rivers.AddRiver(river);
        }

        [Fact]
        public void Test_GetRiver() {
            var data = GetConnection();
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "Louis";
            int length = 4567;
            River river = new River(name, length, countries);

            data.Rivers.AddRiver(river);

            var result = data.Rivers.GetRiverById(1);
            Assert.True(result.Name == name);
            Assert.True(result.Id == 1);
            Assert.True(result.Length == length);
            Assert.True(result.GetCountries().SequenceEqual(countries));
        }

        [Fact]
        public void Test_DeleteRiver() {
            var data = GetConnection();
            River river = GetTestRiver(data);

            data.Rivers.Delete(1);
            var result = data.Rivers.GetRiverById(1);

            Assert.True(result == null);
        }

        [Fact]
        public void Test_UpdateRiver() {
            var data = GetConnection();
            River addedRiver = GetTestRiver(data);
            string newName = "newTestName";
            int newLength = 12;
            Country newCountry = GetTestCountry(data);
            List<Country> newCountries = new List<Country> { newCountry };

            addedRiver.Name = newName;
            addedRiver.Length = newLength;
            addedRiver.SetCountries(newCountries);

            data.Rivers.UpdateRiver(addedRiver);
            River updatedRiver = data.Rivers.GetRiverById(1);
            Assert.True(updatedRiver.Id == 1);
            Assert.True(updatedRiver.Name == newName);
            Assert.True(updatedRiver.Length == newLength);
            Assert.True(updatedRiver.GetCountries().SequenceEqual(newCountries));
        }
    }
}
