﻿using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_Country {

        //Hulp methodes
        private Country GetStandardCountry() {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry", 40000, 10, continent);
            return country;
        }
        private Country GetSecondCountry() {
            Continent continent = new Continent("testContinent");
            Country country = new Country("testCountry2", 40000, 10, continent);
            return country;
        }

        [Fact]
        public void Test_CreateCountry_ShouldSucceed() {
            Continent cont = new Continent("Europe");
            string testName = "name";
            int population = 2500;
            int surface = 35;
            Country ctr = new Country(testName, population, surface, cont);
            Assert.True(ctr.Name == testName, "The name was not set correctly.");
            Assert.True(ctr.Population == population, "The population was not set correctly.");
            Assert.True(ctr.Surface == surface, "The surface area was not set correctly.");
            Assert.True(ctr.Continent.Equals(cont), "The continent was not set correctly.");
            Assert.True(ctr.GetCities().Count == 0, "The cities were not set correctly.");
            Assert.True(ctr.GetCapitals().Count == 0, "The capitals were not set correctly.");
            Assert.True(ctr.GetRivers().Count == 0, "The rivers were not set correctly.");
        }

        [Fact]
        public void Test_CountryWithContinentAsNull_ShouldThrowException() {
            Assert.Throws<CountryException>(() => new Country("France", 12000, 4500, null));
        }


        [Fact]
        public void Test_PopulationLessThanZero_ShouldThrowException() {
            Continent cont = new Continent("Europe");
            string testName = "Denmark";
            int population = -1;
            int surface = 35;
            Assert.Throws<CountryException>(() => new Country(testName, population, surface, cont));
        }

        [Fact]
        public void Test_PopulationIsZero_ShouldThrowException() {
            Continent cont = new Continent("Europe");
            string testName = "Serbia";
            int population = 0;
            int surface = 35;
            Assert.Throws<CountryException>(() => new Country(testName, population, surface, cont));
        }

        [Fact]
        public void Test_SurfaceLessThanZero_ShouldThrowException() {
            Continent cont = new Continent("Europe");
            string testName = "Denmark";
            int population = 34000;
            int surface = -1;
            Assert.Throws<CountryException>(() => new Country(testName, population, surface, cont));
        }

        [Fact]
        public void Test_SurfaceIsZero_ShouldThrowException() {
            Continent cont = new Continent("Europe");
            string testName = "Serbia";
            int population = 20908;
            int surface = 0;
            Assert.Throws<CountryException>(() => new Country(testName, population, surface, cont));
        }

        [Fact]
        public void Test_AddingCapitals_ShouldAddToBothCapitalsAndCities() {
            Country country = GetStandardCountry();
            City city = new City("testCity1", 10, country, true);
            Assert.True(country.GetCapitals().Count == 1, "The amount of capitals was not correct");
            Assert.True(country.GetCapitals() is IReadOnlyCollection<City>, "The collection was not read only.");
            Assert.True(country.GetCities().Count == 1, "The amount of capitals was not correct");
            Assert.True(country.GetCities() is IReadOnlyCollection<City>, "The collection was not read only.");
        }

        [Fact]
        public void Test_AddingCity_ShouldAddToCitiesAndNotCapitals() {
            Country country = GetStandardCountry();
            City city = new City("testCity1", 10, country, false);

            Assert.True(country.GetCapitals().Count == 0, "The amount of capitals was not correct");
            Assert.True(country.GetCapitals() is IReadOnlyCollection<City>, "The collection was not read only.");
            Assert.True(country.GetCities().Count == 1, "The amount of capitals was not correct");
            Assert.True(country.GetCities() is IReadOnlyCollection<City>, "The collection was not read only.");
        }


        [Fact]
        public void Test_RemoveAsCapital_CityStaysInCountry() {
            Country country1 = GetStandardCountry();

            City testCity = new City("testName", 15, country1, true);
            Assert.True(country1.GetCapitals().Count == 1, "The capitals in the second country was not correctly added");
            Assert.True(country1.GetCities().Count == 1, "The Cities in the second country were not correctly added.");
            country1.RemoveAsCapital(testCity);

            Assert.True(testCity.Capital == false, "The city was no longer a capital");
            Assert.True(testCity.Country.Equals(country1), "The country was not correctly updated");
            Assert.True(country1.GetCapitals().Count == 0, "The capitals in the second country was not correctly removed");
            Assert.True(country1.GetCities().Count == 1, "The Cities in the second country were not kept in the cities collection.");
        }

        [Fact]
        public void Test_PopulationMustAlwaysBeBiggerThanTheSumOfTheCities__ShouldThrowException() {
            Country country1 = GetStandardCountry();
            Assert.Throws<CountryException>(() => new City("Gent", country1.Population + 1, country1, true));
        }

        [Fact]
        public void Test_PopulationMustAlwaysBeBiggerThanTheSumOfTheCities_EqualToThePopulation_ShouldThrowException() {
            Country country1 = GetStandardCountry();
            City testCity = new City("Waregem", country1.Population - 1, country1, true);

            City testCity2 = new City("Ooike", 1, country1, true);

            Assert.True(country1.Population == testCity.Population + testCity2.Population, "The population did not match up.");
        }







    }
}
