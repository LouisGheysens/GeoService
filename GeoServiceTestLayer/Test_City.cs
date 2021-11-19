using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_City {
        [Fact]
        public void Test_setName_Valid() {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            City city = new City("Louis", country, 23455);
            city.setName("Louis");
            Assert.Equal("Louis", city.Name);
        }

        [Fact]
        public void Test_setName_InValid() {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            var exc = Assert.Throws<CityException>(() => new City(null, country, 1235));
            Assert.Equal("City: setName - name is null", exc.Message);
        }

        [Fact]
        public void Test_setPopulation_Valid() {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            City city = new City("Louis", country, 23455);
            city.setPopulation(23455);
            Assert.Equal(23455, city.Population);
        }

        [Fact]
        public void Test_setPopulation_InValid() {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            var exc = Assert.Throws<CityException>(() => new City("Antwerp", country, -1));
            Assert.Equal("City: setPopulation - population can't be lower than zero", exc.Message);
        }
    }
}
