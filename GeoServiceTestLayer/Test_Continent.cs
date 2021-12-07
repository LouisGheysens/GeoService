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
        public void Test_ContinentNameEmpty_ShouldThrowException() {
            Assert.Throws<ContinentException>(() => new Continent(""));
        }

        [Fact]
        public void Test_ContinentNameNull_ShouldThrowException() {
            Assert.Throws<ContinentException>(() => new Continent(null));
        }

        [Fact]
        public void Test_FullContinent_ShouldSucceed() {
            Continent c = new Continent("Europe");
            List<Country> countries = new List<Country>();
            Country country1 = new Country("Spain", 15, 10, c);
            Country country2 = new Country("Portugal", 25, 20, c);
            c.SetCountries(countries);
            Assert.True(c.GetCountries().Count == 0, "The amount of countries in the continent was not correct");
            countries.Add(country1);
            countries.Add(country2);
            c.SetCountries(countries);
            Assert.True(c.GetCountries().Count == 2, "The amount of countries was not added correctly.");
        }

        [Fact]
        public void Test_AddCountryToContinent_ShouldSucceed() {
            Continent c = new Continent("Asia");
            Country country1 = new Country("Thailand", 15, 10, c);
            Country country2 = new Country("Nepal", 25, 20, c);
            Assert.True(c.GetCountries().Count == 2, "The amount of countries in the continent was not correct");
        }

        [Fact]
        public void Test_PopulationCorrectlyShowTheSumOfTheCountries_ShouldSucceeed() {
            Continent c = new Continent("Africa");
            Assert.True(c.GetPopulation() == 0, "The Population dit not start at 0");
            int population1 = 15;
            Country country1 = new Country("IvorCoast", population1, 10, c);
            Assert.True(c.GetPopulation() == population1, "The population did not correctly get updated");
            int population2 = 25;
            Country country2 = new Country("Oeganda", population2, 20, c);
            Assert.True(c.GetPopulation() == population1 + population2, "The population did not correctly get updated");
            c.RemoveCountryFromContinent(country1);
            Assert.True(c.GetPopulation() == population2);
        }

    }
    }
