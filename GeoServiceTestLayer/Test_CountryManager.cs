using GeoServiceAPP;
using GeoServiceBusinessLayer;
using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_CountryManager {

        public CountryManager GetTestingManager() {
            return new CountryManager(new TestDataAcces());
        }
        public River CreateFirstTestRiver(CountryManager cM) {
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            return cM.AddRiver(name, length, countries);
        }
        public Country CreateSecondTestCountry(CountryManager cM) {
            Continent continent = CreateSecondTestContinent(cM);
            string name = "TestCountry2";
            int population = 500000;
            int surfaceArea = 8000;
            return cM.AddCountry(name, population, surfaceArea, continent);
        }
        public Continent CreateFirstTestContinent(CountryManager cM) {
            string name = "TestContinent1";
            return cM.AddContinent(name);
        }
        public Continent CreateSecondTestContinent(CountryManager cM) {
            string name = "TestContinent2";
            return cM.AddContinent(name);
        }
        public City CreateFirstTestCity(CountryManager cM) {
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = true;
            return cM.AddCity(name, population, country, capital);
        }
        public Country CreateFirstTestCountry(CountryManager cM) {
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry1";
            int population = 300000;
            int surfaceArea = 18000;
            return cM.AddCountry(name, population, surfaceArea, continent);
        }

        [Fact]
        public void Test_AddsCorrectly() {
            CountryManager cM = GetTestingManager();
            string name = "TestString";
            cM.AddContinent(name);
            Continent continent = cM.GetContinentForId(1);

            Assert.True(continent.Name == name, "The name was not correct");
            Assert.True(continent.Id == 1, "The name was not correct");
        }

        [Fact]
        public void Test_ReturnsAddedContinent() {
            CountryManager cM = GetTestingManager();
            string name = "TestString";
            Continent continent2 = cM.AddContinent(name);
            Continent continent1 = cM.GetContinentForId(1);

            Assert.True(continent1.Equals(continent2), "the continent were not equal");
        }
        [Fact]
        public void Test_AddValuesCorrectly() {
            CountryManager cM = GetTestingManager();
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry";
            int population = 300;
            int surfaceArea = 18000;
            Country c = cM.AddCountry(name, population, surfaceArea, continent);
            Country country = cM.GetCountryForId(1);

            Assert.True(country.Name == name, "The name was not correct.");
            Assert.True(country.Population == population, "the population was not correct.");
            Assert.True(country.Surface == surfaceArea, "The surface area was not correct.");

        }
        [Fact]
        public void Test_ReturnsAdddedCountry() {
            CountryManager cM = GetTestingManager();
            Continent continent = CreateFirstTestContinent(cM);
            string name = "TestCountry";
            int population = 300;
            int surfaceArea = 18000;
            Country c = cM.AddCountry(name, population, surfaceArea, continent);
            Country country = cM.GetCountryForId(1);

            Assert.True(country.Equals(c), "The countries were not equal.");

        }

        [Fact]
        public void Test_AddedCorrectly() {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = false;
            City c = cM.AddCity(name, population, country, capital);
            City result = cM.GetCityForId(1);

            Assert.True(result.Capital == capital, "The capital check was not correct.");
            Assert.True(result.Country.Equals(country), "The countries were not equal.");
            Assert.True(result.Id == 1, "The id was not correct.");
            Assert.True(result.Name == name, "The name was not correct.");
            Assert.True(result.Population == population, "The population was not correct");

            Assert.True(result.Country.GetCities().Contains(result), "The country did not add the city to its collection.");
        }

        [Fact]
        public void Test_ReturnsAddedCity() {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            string name = "testCity";
            int population = 13000;
            bool capital = false;
            City c = cM.AddCity(name, population, country, capital);
            City result = cM.GetCityForId(1);

            Assert.True(c.Equals(result), "The cities were not equal.");
        }


        [Fact]
        public void Test_AddedValueCorrectly() {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            River r = cM.AddRiver(name, length, countries);
            River result = cM.GetRiverForId(1);

            Assert.True(result.Length == length, "The length was not correct.");
            Assert.True(result.Id == 1, "The id was not correct.");
            Assert.True(result.Name == name, "The name was not correct.");
            Assert.True(result.GetCountries().SequenceEqual(countries), "The countries were not correct");

            Assert.True(result.GetCountries()[0].GetRivers()[0].Equals(result), "The country did not add the river to its collection.");
        }

        [Fact]
        public void Test_ReturnsAddedRiver() {
            CountryManager cM = GetTestingManager();
            Country country = CreateFirstTestCountry(cM);
            List<Country> countries = new List<Country> { country };
            string name = "testRiver";
            int length = 13000;
            River r = cM.AddRiver(name, length, countries);
            River result = cM.GetRiverForId(1);

            Assert.True(r.Equals(result), "The rivers were not equal.");
        }


        #region updateTests
        [Fact]
        public void Test_ShouldUpdateNameCorrectly() {
            CountryManager cM = GetTestingManager();
            Continent c = CreateFirstTestContinent(cM);
            string newName = "newTestingContinentnew";
            c.Name = newName;
            cM.UpdateContinent(c);

            Continent result = cM.GetContinentForId(1);
            Assert.True(result.Name == newName, "The name was not updated correctly.");
        }
        [Fact]
        public void Test_ShouldReturnTheCorrectContinent() {
            CountryManager cM = GetTestingManager();
            Continent c = CreateFirstTestContinent(cM);
            string newName = "newTestingContinentnew";
            c.Name = newName;
            Continent secondresult = cM.UpdateContinent(c);

            Continent result = cM.GetContinentForId(1);
            Assert.True(result.Equals(secondresult), "The continents were not equal.");
        }
        [Fact]
        public void Test_ShouldUpdateCorrectly() {
            CountryManager cM = GetTestingManager();
            Country c = CreateFirstTestCountry(cM);
            Continent newContinent = CreateSecondTestContinent(cM);
            string newName = "newTestingCountrynew";
            int population = 15;
            int surface = 10;
            c.Name = newName;
            c.Population = population;
            c.Surface = surface;
            c.Continent = newContinent;
            Country result = cM.UpdateCountry(c);
            Country country = cM.GetCountryForId(1);

            Continent firstContinent = cM.GetContinentForId(1);

            Assert.True(country.Id == 1, "The id was not correct.");
            Assert.True(country.Name == newName, "The name was not updated correctly.");
            Assert.True(country.Population == population, "The population was not updated correctly.");
            Assert.True(country.Surface == surface, "The surface area was not updated correctly.");
            Assert.True(country.Continent.Equals(newContinent), "The continent was not updated correctly");

            Assert.True(firstContinent.GetCountries().Count == 0, "The country was not properly removed from the first continent.");
        }

        [Fact]
        public void Test_ShouldReturnUpdatedCountry() {
            CountryManager cM = GetTestingManager();
            Country c = CreateFirstTestCountry(cM);
            Continent newContinent = CreateSecondTestContinent(cM);
            string newName = "newTestingCountrynew";
            int population = 15;
            int surface = 10;
            c.Name = newName;
            c.Population = population;
            c.Surface = surface;
            c.Continent = newContinent;
            Country result = cM.UpdateCountry(c);
            Country country = cM.GetCountryForId(1);

            Assert.True(country.Equals(result), "The countries were not equal.");
        }

        [Fact]
        public void Test_CMShouldUpdateCorrectly() {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            Country country = CreateSecondTestCountry(cM);
            string name = "NewTestNameStringNew";
            int population = 12345;
            city.Country = country;
            city.Name = name;
            city.Population = population;
            cM.UpdateCity(city);

            Country firstCountry = cM.GetCountryForId(1);

            City result = cM.GetCityForId(1);
            Assert.True(result.Population == population, "The population was not properly updated");
            Assert.True(result.Name == name, "The name was not properly updated");
            Assert.True(result.Country.Equals(country), "The country was not properly updated.");
            Assert.True(country.GetCities()[0].Equals(city), "the city was not properly added to the country");
            Assert.True(firstCountry.GetCities().Count == 0, "The city was not properly removed from the original country");
            Assert.True(country.GetCities().Count == 1, "The city was not properly added to the new country.");
            Assert.True(country.GetCapitals().Count == 1, "The city was not properly added to capitals of the new country.");

        }

        [Fact]
        public void Test_ShouldReturnUpdatedCity() {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            Country country = CreateSecondTestCountry(cM);
            string name = "NewTestNameStringNew";
            int population = 12345;
            city.Country = country;
            city.Name = name;
            city.Population = population;
            City updated = cM.UpdateCity(city);

            City firstCity = cM.GetCityForId(1);


            Assert.True(firstCity.Equals(updated), "The cities were not equal.");

        }

        [Fact]
        public void Test_ShouldReturnUpdatedRiver() {
            CountryManager cM = GetTestingManager();
            River r = CreateFirstTestRiver(cM);
            string name = "NewTestNameStringNew";
            int length = 12345;
            r.Length = length;
            r.Name = name;
            River updated = cM.UpdateRiver(r);

            River firstRiver = cM.GetRiverForId(1);

            Assert.True(firstRiver.Equals(updated), "The cities were not equal.");

        }
        [Fact]
        public void Test_ManagerShouldUpdateCorrectly() {
            CountryManager cM = GetTestingManager();
            River r = CreateFirstTestRiver(cM);
            List<Country> countries = new List<Country> { CreateSecondTestCountry(cM) };
            string name = "NewTestNameStringNew";
            int length = 12345;
            r.Length = length;
            r.Name = name;
            r.SetCountries(countries);
            River updated = cM.UpdateRiver(r);

            Country firstCountry = cM.GetCountryForId(1);
            River firstRiver = cM.GetRiverForId(1);

            Assert.True(firstRiver.Length == length, "The length was not properly updated.");
            Assert.True(firstRiver.Name == name, "The name was not properly updated.");
            Assert.True(firstCountry.GetRivers().Count() == 0, "The rivers of the original country did not get updated correctly.");
            Assert.True(firstRiver.GetCountries()[0].GetRivers().Count() == 1, "The rivers of the new country was not properly updated.");

        }
        [Fact]
        public void Test_StillHasCities_ShouldPreventDeletingCountry() {
            CountryManager cM = GetTestingManager();
            City city = CreateFirstTestCity(cM);
            cM.DeleteCountry(1);
        }

        [Fact]
        public void Test_NameMustBeUnique() {
            CountryManager cM = GetTestingManager();
            Continent firstContinent = CreateFirstTestContinent(cM);
            cM.AddContinent(firstContinent.Name);
        }
    }
}
#endregion