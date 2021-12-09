using GeoServiceAPP;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeoServiceTestLayer.DatabaseTesting {
    public class Test_Data_Continent {

        private TestDataAcces GetConnection() {
            return new TestDataAcces();
        }

        [Fact]
        public void Test_GetNewContinent() {
            string name = "Asia";
            Continent continent = new Continent(name);
            var data = GetConnection();

            data.Continents.AddContinent(continent);

            var result = data.Continents.GetContinentById(1);
            Assert.True(result.Name == name);
            Assert.True(result.Id == 1);
        }

        [Fact]
        public void Test_ReturnNewContinent() {
            string name = "Africa";
            Continent continent = new Continent(name);
            var data = GetConnection();

            var rt = data.Continents.AddContinent(continent);
            var result = data.Continents.GetContinentById(1);
            Assert.True(rt.Equals(result));
        }

        [Fact]
        public void Test_DeleteContinent() {
            string name = "North-America";
            Continent co = new Continent(name);
            var data = GetConnection();

            data.Continents.AddContinent(co);
            data.Continents.Delete(1);
            var result = data.Continents.GetContinentById(1);

            Assert.True(result == null);
        }

        [Fact]
        public void Test_UpdateContinent() {
            string name = "Antartica";
            string newName = "Europe";
            Continent continent = new Continent(name);
            var data = GetConnection();

            Continent addedContinent = data.Continents.AddContinent(continent);
            addedContinent.Name = newName;
            data.Continents.Update(addedContinent);
            Continent updatedContinent = data.Continents.GetContinentById(1);
            Assert.True(updatedContinent.Id == 1);
            Assert.True(updatedContinent.Name == newName);
        }

        [Fact]
        public void Test_IsNameAvailable() {
            string name = "testname";
            Continent continent = new Continent(name);
            var data = GetConnection();
            Assert.True(data.Continents.IsNameAvailable(name), "The name returned false when there was nothing in the database.");

            data.Continents.AddContinent(continent);
            Assert.True(!data.Continents.IsNameAvailable(name), "The name returned true when the name was in the database.");
            Assert.True(data.Continents.IsNameAvailable(name + "s"), "The name returned false when the name wasn't in the database.");
        }

    }
}
