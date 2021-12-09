using GeoServiceAPI.Model;
using GeoServiceAPI.Model.Input;
using GeoServiceAPI.Model.Output;
using GeoServiceBusinessLayer;
using GeoServiceBusinessLayer.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.ApiTesting {
    public class Test_APIComplete {
        /// <summary>
        /// Testing data API
        /// </summary>

        private APICompletion api;
        private Mock<ICountryManager> MockManager;
        private Mock<IConfiguration> MockConfig;

        private Continent GetContinentWithCountries(int amount) {
            Continent continent = new Continent("Africa");
            continent.Id = 1;
            for (int i = 0; i < amount; i++) {
                Country c = new Country($"testCountry{i}", (i + 1) * 100, (i + 1) * 200, continent);
                c.Id = i + 1;
            }
            return continent;
        }

        private Country GetCountryWithCities(int amount) {
            Continent continent = GetContinentWithCountries(2);
            Country country = continent.GetCountries()[0];

            for (int i = 0; i < amount; i++) {
                City c = new City($"testCity{i}", (i + 1) * 10, country, i % 2 == 0 ? true : false);
                c.Id = i + 1;
            }
            return country;
        }
        private City GetCity() {
            Country country = GetCountryWithCities(3);
            return country.GetCapitals()[0];
        }
        private River GetRiverWithCities() {
            Country country = GetCountryWithCities(1);
            List<Country> countries = new List<Country>();
            foreach (Country c in country.Continent.GetCountries()) {
                countries.Add(c);
            }
            River river = new River("Oera", 15000, countries);
            river.Id = 1;
            return river;
        }

        public Test_APIComplete() {

            MockManager = new Mock<ICountryManager>();
            var MockConfig = new ConfigurationBuilder().AddJsonFile
                (Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json")).Build();
            api = new APICompletion(MockManager.Object, MockConfig);
        }

        [Fact]
        public void Test_RetrieveContinentId() {
            Continent continent = GetContinentWithCountries(2);
            MockManager.Setup(m => m.GetContinentForId(1)).Returns(continent);
            var result = api.GetContinentForId(2);
            string expectedId = @"http://localhost:5000/api/continent/1";
            string expectedCountryString = @"http://localhost:5000/api/continent/1/Country/1";
            string expectedSecondCountryString = @"http://localhost:5000/api/continent/1/Country/2";
            Assert.True(result is ContinentDTOutput);
            Assert.True(result.ContinentId == expectedId);
            Assert.True(result.Name == continent.Name);
            Assert.True(result.Population == continent.GetPopulation());
            Assert.True(result.Countries[0] == expectedCountryString);
            Assert.True(result.Countries[1] == expectedSecondCountryString);
        }

        [Fact]
        public void Test_RetrieveRiverId() {
            River river = GetRiverWithCities();
            MockManager.Setup(m => m.GetRiverForId(1)).Returns(river);
            var result = api.GetRiverForId(1);
            string riverId = @"http://localhost:5000/api/river/1";
            string countryId1 = @"http://localhost:5000/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:5000/api/continent/1/Country/2";
            Assert.True(result is RiverDTOutput);
            Assert.True(result.Name == river.Name);
            Assert.True(result.Length == river.Length);
            Assert.True(result.Countries[0] == countryId1);
            Assert.True(result.Countries[1] == countryId2);
        }

        [Fact]
        public void Test_AddContinent() {

            ContinentDTOInput DTOin = new ContinentDTOInput() { Name = "TestContinent" };
            Continent continent = new Continent(DTOin.Name);
            continent.Id = 1;
            MockManager.Setup(m => m.AddContinent(DTOin.Name)).Returns(continent);
            var result = api.AddContinent(DTOin);
            string expectedId = @"http://localhost:5000/api/continent/1";
            Assert.True(result is ContinentDTOutput);
            Assert.True(result.ContinentId == expectedId);
            Assert.True(result.Name == DTOin.Name);
        }

        [Fact]
        public void Test_AddCountry() {
            Continent continent = new Continent("Europe");
            CountryDTOInput DTOin = new CountryDTOInput() 
            { Name = "Europe", ContinentId = 1, Population = 12, Surface = 15 };
            Country country = new Country(DTOin.Name, DTOin.Population, DTOin.Surface, continent);
            continent.Id = 1;
            country.Id = 1;
            MockManager.Setup(m => m.GetContinentForId(continent.Id)).Returns(continent);
            MockManager.Setup(m => m.AddCountry(DTOin.Name, DTOin.Population, DTOin.Surface, continent)).Returns(country);
            var result = api.AddCountry(DTOin);
            string continentId = @"http://localhost:5000/api/continent/1";
            string countryId = @"http://localhost:5000/api/continent/1/Country/1";
            Assert.True(result is CountryDTOutput);
            Assert.True(result.Continent == continentId);
            Assert.True(result.Name == country.Name);
            Assert.True(result.CountryId == countryId);
            Assert.True(result.Population == country.Population);
            Assert.True(result.Surface == country.Surface);
        }

        [Fact]
        public void Test_AddRiver() {
            River river = GetRiverWithCities();
            List<Country> RiverCountries = new List<Country>() { river.GetCountries()[0], river.GetCountries()[1] };
            RiverDTOInput DTOin = new RiverDTOInput() { Name = river.Name, Length = 
                river.Length, CountryIdArray = new int[] { 1, 2 } };
            MockManager.Setup(m => m.GetCountryForId(1)).Returns(RiverCountries[0]);
            MockManager.Setup(m => m.GetCountryForId(2)).Returns(RiverCountries[1]);
            MockManager.Setup(m => m.AddRiver(river.Name, river.Length, RiverCountries)).Returns(river);
            var result = api.AddRiver(DTOin);
            string riverId = @"http://localhost:5000/api/river/1";
            string countryId1 = @"http://localhost:5000/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:5000/api/continent/1/Country/2";

            Assert.True(result is RiverDTOutput);
            Assert.True(result.Name == river.Name);
            Assert.True(result.Length == river.Length);
            Assert.True(result.Countries[0] == countryId1);
            Assert.True(result.Countries[1] == countryId2);
        }

        [Fact]
        public void Test_AddCity() {
            City city = GetCity();
            CityDTOInput DTOin = new CityDTOInput() { Name = city.Name,Population = city.Population,CityId = city.Id,ContinentId = city.Country.Continent.Id,CountryId = city.Country.Id,Capital = city.Capital
            };
            MockManager.Setup(m => m.GetCountryForId(city.Country.Id)).Returns(city.Country);
            MockManager.Setup(m => m.AddCity(DTOin.Name, DTOin.Population, city.Country, city.Capital)).Returns(city);
            var result = api.AddCity(DTOin);
            string cityId = @"http://localhost:5000/api/continent/1/Country/1/City/1";
            string countryId = @"http://localhost:5000/api/continent/1/Country/1";
            Assert.True(result.CityId == cityId);
            Assert.True(result.Country == countryId);
            Assert.True(result.Name == city.Name);
            Assert.True(result.Population == city.Population);
            Assert.True(result.Population == city.Population);
        }

        [Fact]
        public void Test_DeleteContinent() {
            api.DeleteCity(1);
            MockManager.Verify(m => m.DeleteContinent(1), Times.Once);
        }

        [Fact]
        public void Test_DeleteCity() {
            api.DeleteCity(1);
            MockManager.Verify(m => m.DeleteCity(1), Times.Once);
        }

        [Fact]
        public void Test_DeleteRiver() {
            api.DeleteRiver(1);
            MockManager.Verify(m => m.DeleteRiver(1), Times.Once);
        }

        [Fact]
        public void Test_DeleteCountry() {
            api.DeleteCountry(1);
            MockManager.Verify(m => m.DeleteCountry(1), Times.Once);
        }

        [Fact]
        public void Test_UpdateContinent() {
            Continent continent = GetContinentWithCountries(2);
            ContinentDTOInput DTOin = new ContinentDTOInput() { Name = continent.Name, ContinentId = continent.Id };
            MockManager.Setup(m => m.GetContinentForId(continent.Id)).Returns(continent);
            MockManager.Setup(m => m.UpdateContinent(continent)).Returns(continent);
            var result = api.UpdateContinent(DTOin);
            string expectedId = @"http://localhost:5000/api/continent/1";
            string expectedCountryString = @"http://localhost:5000/api/continent/1/Country/1";
            string expectedSecondCountryString = @"http://localhost:5000/api/continent/1/Country/2";
            Assert.True(result is ContinentDTOutput);
            Assert.True(result.ContinentId == expectedId);
            Assert.True(result.Name == continent.Name);
            Assert.True(result.Population == continent.GetPopulation());
            Assert.True(result.Countries[0] == expectedCountryString);
            Assert.True(result.Countries[1] == expectedSecondCountryString);
        }

        [Fact]
        public void Test_UpdateCity() {
            City city = GetCity();
            CityDTOInput DTOin = new CityDTOInput()
            {
                Name = city.Name,
                Capital = city.Capital,
                CityId = city.Id,
                ContinentId = city.Country.Continent.Id,
                CountryId = city.Country.Id,
                Population = city.Population
            };

            MockManager.Setup(m => m.UpdateCity(city)).Returns(city);
            MockManager.Setup(m => m.GetCityForId(city.Id)).Returns(city);
            MockManager.Setup(m => m.GetCountryForId(city.Country.Id)).Returns(city.Country);
            var result = api.UpdateCity(DTOin);
            string cityId = @"http://localhost:5000/api/continent/1/Country/1/City/1";
            string countryId = @"http://localhost:5000/api/continent/1/Country/1";
            Assert.True(result.CityId == cityId);
            Assert.True(result.Country == countryId);
            Assert.True(result.Name == city.Name);
            Assert.True(result.Population == city.Population);
        }

        [Fact]
        public void Test_UpdateCountry() {
            Country country = GetCountryWithCities(2);
            CountryDTOInput DTOin = new CountryDTOInput()
            {
                Name = country.Name,
                ContinentId = country.Continent.Id,
                CountryId = country.Id,
                Population = country.Population,
                Surface = country.Surface
            };

            MockManager.Setup(m => m.UpdateCountry(country)).Returns(country);
            MockManager.Setup(m => m.GetCountryForId(country.Id)).Returns(country);
            MockManager.Setup(m => m.GetContinentForId(country.Continent.Id)).Returns(country.Continent);
            var result = api.UpdateCountry(DTOin);
            string continentId = @"http://localhost:5000/api/continent/1";
            string countryId = @"http://localhost:5000/api/continent/1/Country/1";
            string cityId1 = @"http://localhost:5000/api/continent/1/Country/1/City/1";
            string cityId2 = @"http://localhost:5000/api/continent/1/Country/1/City/2";
            Assert.True(result is CountryDTOutput);
            Assert.True(result.Continent == continentId);
            Assert.True(result.Name == country.Name);
            Assert.True(result.Population == country.Population);
            Assert.True(result.Cities[0] == cityId1);
            Assert.True(result.Cities[1] == cityId2);
            Assert.True(result.Capitals[0] == cityId1);
        }

        [Fact]
        public void Test_UpdateRiver() {
            River river = GetRiverWithCities();
            int[] countryIds = new int[] { river.GetCountries()[0].Id, river.GetCountries()[1].Id };
            RiverDTOInput DTOin = new RiverDTOInput()
            {
                Name = river.Name,
                Length = river.Length,
                RiverId = river.Id,
                CountryIdArray = countryIds
            };
            MockManager.Setup(m => m.GetCountryForId(countryIds[0])).Returns(river.GetCountries()[0]);
            MockManager.Setup(m => m.GetCountryForId(countryIds[1])).Returns(river.GetCountries()[1]);
            MockManager.Setup(m => m.GetRiverForId(river.Id)).Returns(river);
            MockManager.Setup(m => m.UpdateRiver(river)).Returns(river);
            var result = api.UpdateRiver(DTOin);
            string riverId = @"http://localhost:5000/api/river/1";
            string countryId1 = @"http://localhost:5000/api/continent/1/Country/1";
            string countryId2 = @"http://localhost:5000/api/continent/1/Country/2";

            Assert.True(result is RiverDTOutput);
            Assert.True(result.Name == river.Name);
            Assert.True(result.Length == river.Length);
            Assert.True(result.Countries[0] == countryId1);
            Assert.True(result.Countries[1] == countryId2);
        }
    }
}
