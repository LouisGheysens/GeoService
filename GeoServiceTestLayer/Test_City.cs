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

        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Test_setName_InValid(string name) {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            City city = new City(name, country, 23455);
            var exc = Assert.Throws<CityException>(() => city.setName(name));
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

        [Theory]
        [InlineData(0)]
        public void Test_setPopulation_InValid(int population) {
            Continent cont = new Continent("Europe");
            Country country = new Country("Belgium", 23000, 4444, cont);
            City city = new City("Louis", country, population);
            var exc = Assert.Throws<CityException>(() => city.setPopulation(population));
            Assert.Equal("City: setPopulation - population can't be lower than zero", exc.Message);
        }
    }
}
