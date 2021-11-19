using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer {
    public class Test_River {

        [Fact]
        public void Test_setName_Valid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukus", 12, countries);
            river.setName("Kaukus");
            Assert.Equal("Kaukus", river.Name);

        }

        [Theory]
        [InlineData(null)]
        public void Test_setName_Invalid(string name) {
            List<Country> countries = new List<Country>();
            River river = new(name, 12, countries);
            var exc = Assert.Throws<RiverException>(() => river.setName(name));
            Assert.Equal("River: setName - Name is empty" ,exc.Message);
        }


        [Fact]
        public void Test_setLength_Valid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukus", 12, countries);
            river.setLength(12);
            Assert.Equal(12, river.Length);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_setLength_Invalid(int length) {
            List<Country> countries = new List<Country>();
            River river = new("Kaukasus", 12, countries);
            var exc = Assert.Throws<RiverException>(() => river.setLength(length));
            Assert.Equal("River: setLength - Length is lower or is null", exc.Message);
        }

        [Fact]
        public void Test_addCountry_Valid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukus", 12, countries);
            Continent continenten = new Continent("Asia");
            Country country = new Country("Taiwan", 12356, 7897, continenten);
            river.addCountry(country);
            Assert.Contains(country, river.Countries);
            Country countryOne = new Country("Thailand", 1211, 4589, continenten);
            river.addCountry(countryOne);
            Assert.Contains(countryOne, river.Countries);
            Assert.Equal(2, river.Countries.Count);
        }

        [Fact]
        public void Test_addCountry_Invalid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukasus", 12, countries);
        }

        [Fact]
        public void Test_removeCountry_Valid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukus", 12, countries);
            river.setLength(12);
            Assert.Equal(12, river.Length);
        }
        [Fact]
        public void Test_removeCountry_Invalid() {
            List<Country> countries = new List<Country>();
            River river = new("Kaukasus", 12, countries);
        }
    }
}
