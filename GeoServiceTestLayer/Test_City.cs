using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_City {

        //Help methodes
        private City GetStandardCity() {
            string cityName = "testCity";
            int population = 35000;
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            City city = new City(cityName, population, country, true);

            return city;
        }

        private Country GetStandardCountry() {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            return country;
        }

        [Fact]
        private void Test_City_Data_Succeed() {
            string name = "Olsene";
            int population = 35000;
            Continent c = new Continent("Europe");
            Country ctry = new Country("Belgium", 40000, 10, c);
            City city = new City(name, population, ctry, true);

            Assert.True(city.Name == name, "The name of the city was not correct");
            Assert.True(city.Population == population, "The population of the city was not correct");
            Assert.True(city.Country.Equals(ctry), "The country of the city was not correct");
            Assert.True(ctry.GetCities().Contains(city), "The country did not contain the city");
            Assert.True(ctry.GetCapitals().Contains(city), "The country's capitals did not contain the city");
            Assert.True(city.Capital == true, "The city did not show it was a capital");
        }

        [Fact]
        public void Test_PopulationLessThanZero_ShouldThrowException() {
            Country country = GetStandardCountry();
            Assert.Throws<CityException>(() => new City("Zulte", -1, country, false));
        }

        [Fact]
        public void Test_PopulationIsZero_ShouldThrowException() {
            Country country = GetStandardCountry();
            Assert.Throws<CityException>(() => new City("Zulte", 0, country, false));
        }

        [Fact]
        public void Test_CountryNullValue_ShouldThrowException() {
             Assert.Throws<CityException>(() => new City("Zulte", 49000, null, false));
        }


    }
}
