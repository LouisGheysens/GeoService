using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_Continent {

        [Fact]
        public void Test_setName_Valid() {
            Continent continent = new Continent("Asia");
            continent.setName("Asia");
            Assert.Equal("Asia", continent.Name);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Test_setName_InValid(string name) {
            Continent continent = new Continent("Asia");
            var exc = Assert.Throws<ContinentException>(() => continent.setName(name));
            Assert.Equal("Continent: setName - name is null", exc.Message);
        }

        [Fact]
        public void Test_add_ValidValue_ToCountryCollection() {
            List<Country> countries = new List<Country>();
            Continent c = new Continent("South-America");
            Country country = new Country("Mexico", 2345, 4567, c);
            c.addCountry(country);
            Assert.Contains(country, c.Countries);
            Country CountryOne = new Country("Cuba", 3456, 1289, c);
            c.addCountry(country);
            Assert.Contains(CountryOne, c.Countries);
            Assert.Equal(2, c.Countries.Count);
        }

        [Theory]
        [InlineData(null)]
        public void Test_add_InValidValue_ToCountryCollection(string country) {
            List<Country> countries = new List<Country>();
            Continent c = new Continent("South-America");
            Country countryOne = new Country(country, 2345, 4567, c);
            c.addCountry(countryOne);
            var exc = Assert.Throws<ContinentException>(() => c.addCountry(countryOne));
            Assert.Equal("Continent: addCountry - country is null", exc.Message);
            Assert.Equal(0, c.Countries.Count);
        }


        [Fact]
        public void Test_removeCountry_Succeed() {
            //ADD Values
            List<Country> countries = new List<Country>();
            Continent c = new Continent("South-America");
            Country countryOne = new Country("Mexico", 2345, 4567, c);
            c.addCountry(countryOne);


            //Delete Values
            c.removeCountry(countryOne);

            Assert.Equal(0, c.Countries.Count);

        }

        [Fact]
        public void Test_RemoveCountry_NotSucceed() {

        }


    }
}
