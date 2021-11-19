using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_Country {

        [Fact]
        public void Test_setName_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 3456, 7897, continenten);
            country.setName("China");
            Assert.Equal("China", country.Name);
        }

        [Theory]
        [InlineData(null)]
        public void Test_setName_Invalid(string name) {
            Continent continenten = new Continent("Asia");
            Country country = new Country(name, 3456, 7897, continenten);
            var exc = Assert.Throws<CountryException>(() => country.setName(name));
            Assert.Equal("Country: setName - Name is null", exc.Message);
        }

        [Fact]
        public void Test_setPopulation_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 0, 7897, continenten);
            country.setPopulation(0);
            Assert.Equal(0, country.Population);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_setPopulation_Invalid(int population) {
            Continent continenten = new Continent("Asia");
            Country country = new Country("Taiwan", population, 7897, continenten);
            var exc = Assert.Throws<CountryException>(() => country.setPopulation(population));
            Assert.Equal("Country: setPopulation - Population can't be zero or lower than one", exc.Message);
        }

        [Fact]
        public void Test_setSurface_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 1113, 0, continenten);
            country.setPopulation(0);
            Assert.Equal(0, country.Surface);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_setSurface_Invalid(int surface) {
            Continent continenten = new Continent("Asia");
            Country country = new Country("Taiwan", 12212, surface, continenten);
            var exc = Assert.Throws<CountryException>(() => country.setSurface(surface));
            Assert.Equal("Country: setSurface - Surface can't be zero or lower than one", exc.Message);
        }


        [Fact]
        public void Test_setContinent_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 1113, 122220, continenten);
            country.setContinent(continenten);
            Assert.Equal(continenten, country.Continent);
        }

        [Theory]
        [InlineData(null)]
        public void Test_Continent_Invalid(Continent continent) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
            var exc = Assert.Throws<CountryException>(() => country.setContinent(continent));
            Assert.Equal("Country: setContinent - continent is null", exc.Message);
        }

        [Fact]
        public void Test_addCity_Valid() {
            Continent continenten = new Continent("Asia");
            Country coun = new Country("China", 1113, 122220, continenten);
            City c = new City("Shangai", coun, 1222);
            coun.addCity(c);
            Assert.Contains(c, coun.Cities);
            City cChina = new City("Chinatown", coun, 3456);
            coun.addCity(cChina);
            Assert.Equal(2, coun.Cities.Count);
        }

        [Theory]
        [InlineData(null)]
        public void Test_addCity_Invalid(City city) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
        }

        [Theory]
        [InlineData(null)]
        public void Test_addCity_BecauseCityAllreadyExists(City city) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
        }

        [Fact]
        public void Test_removeCity_Valid() {
            Continent continenten = new Continent("Asia");
            Country coun = new Country("China", 1113, 122220, continenten);
            City c = new City("Shangai", coun, 1222);
            coun.addCity(c);
            Assert.Contains(c, coun.Cities);

        }

        [Theory]
        [InlineData(null)]
        public void Test_Test_removeCity_Invalid(City city) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
        }


        [Fact]
        public void Test_addCapital_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 1113, 122220, continenten);

        }

        [Theory]
        [InlineData(null)]
        public void Test_Test_removeCapital_Invalid(City capital) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
        }


        [Fact]
        public void Test_addRiver_Valid() {
            Continent continenten = new Continent("Asia");
            Country country = new Country("China", 1113, 122220, continenten);

        }

        [Theory]
        [InlineData(null)]
        public void Test_Test_removeRiverInvalid(River river) {
            Continent continenten = new Continent(null);
            Country country = new Country("Taiwan", 12212, 12122, continenten);
        }


    }
}
