using GeoServiceAPP;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.DatabaseTesting {
    public class Test_Data_Country {
        private TestDataAcces GetTestDataAccess()
        {
            return new TestDataAcces();
        }
        private Continent GetTestContinent(TestDataAcces Data)
        {
            Continent continent = new Continent("Europe");
            return Data.Continents.AddContinent(continent);
        }
        public Country GetTestCountry(TestDataAcces Data)
        {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("Begium", 15000, 14000, continent);
            return Data.Countries.AddCountry(country);
        }
        public Continent GetSecondTestContinent(TestDataAcces data)
        {
            Continent continent = new Continent("Africa");
            return data.Continents.AddContinent(continent);
        }
        public Country GetSecondTestCountry(TestDataAcces Data)
        {
            Continent continent = GetTestContinent(Data);
            Country country = new Country("Zimbabwe", 16000, 15000, continent);
            return Data.Countries.AddCountry(country);
        }

        private River GetTestRiver(TestDataAcces data) {
            Country country = GetTestCountry(data);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 4567;
            River river = new River(name, length, countries);

            return data.Rivers.AddRiver(river);
        }

        [Fact]
        public void Test_GetCountry() {
            var data = GetTestDataAccess();
            string name = "Albania";
            int population = 35;
            int surfaceArea = 12000;
            Continent continent = GetTestContinent(data);
            Country country = new Country(name, population, surfaceArea, continent);

            data.Countries.AddCountry(country);

            var result = data.Countries.GetCountryById(1);
            Assert.True(result.Name == name);
            Assert.True(result.Id == 1);
            Assert.True(result.Population == population);
            Assert.True(result.Surface == surfaceArea);
            Assert.True(result.Continent.Equals(continent));
        }

        [Fact]
        public void Test_AddCountry() {
            var data = GetTestDataAccess();
            string name = "Bulgaria";
            int population = 35;
            int surfaceArea = 12000;
            Continent continent = GetTestContinent(data);
            Country country = new Country(name, population, surfaceArea, continent);

            Country result1 = data.Countries.AddCountry(country);

            var result2 = data.Countries.GetCountryById(1);
            Assert.True(result1.Equals(result2));
        }

        [Fact]
        public void Test_DeleteCountry() {
            var data = GetTestDataAccess();
            Country country = GetTestCountry(data);
            data.Countries.Delete(1);
            var result = data.Countries.GetCountryById(1);
            Assert.True(result == null);
        }

        [Fact]
        public void Test_UpdateCountry() {
            var data = GetTestDataAccess();
            Country addedCountry = GetTestCountry(data);
            string newName = "Sweden";
            Continent newContinent = GetSecondTestContinent(data);

            addedCountry.Name = newName;
            addedCountry.Population = 123;
            addedCountry.Surface = 456;
            addedCountry.Continent = newContinent;

            data.Countries.Update(addedCountry);
            Country updatedCountry = data.Countries.GetCountryById(1);
            Assert.True(updatedCountry.Id == 1);
            Assert.True(updatedCountry.Name == newName);
            Assert.True(updatedCountry.Population == 123);
            Assert.True(updatedCountry.Surface == 456);
            Assert.True(updatedCountry.Continent.Equals(newContinent));
        }

        [Fact]
        public void Test_RiverData() {
            var dt = GetTestDataAccess();
            River rdt = GetTestRiver(dt);

            Assert.True(rdt.GetCountries().Count != 0);
            Assert.True(rdt.GetCountries()[0].Continent != null);
        }
    }
}
