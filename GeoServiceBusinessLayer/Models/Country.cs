using GeoServiceAPI.Mappings;
using GeoServiceBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public partial class Country {
        #region Constructor
        public Country(string name, int population, int surface, Continent continent) {
            setName(name);
            setPopulation(population);
            setSurface(surface);
            setContinent(continent);
        }

        public Country() { }
        #endregion

        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Population { get; private set; }

        public int Surface { get; private set; }

        public Continent Continent { get; private set; }

        public virtual ICollection<City> Cities { get; private set; } = new List<City>();

        public virtual ICollection<City> Capitals { get; private set; } = new List<City>();

        public virtual ICollection<River> Rivers { get; private set; } = new List<River>();
        #endregion

        #region Methods

        public void setContinent(Continent continent) {
            if (continent == null) throw new CountryException("Country: setContinent - continent is null");
            this.Continent = continent;
        }

        public void setSurface(int surface) {
            if (surface < 1) throw new CountryException("Country: setSurface - Surface can't be zero or lower than one");
        }

        public void setPopulation(int population) {
            if (population < 1) throw new CountryException("Country: setPopulation - Population can't be zero or lower than one");
            this.Population = population;
        }

        public void setName(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new CountryException("Country: setName - Name is null");
            this.Name = name;
        }

        public void addCities(List<City> cities) {
            if (cities == null) throw new CountryException("Country: addCities - city is null!");
            foreach(City c in cities) {
                addCity(c);
            }
        }

        public void addCity(City city) {
            if (city == null) throw new CountryException("Country: addCity - city is null!");
            if (this.Cities.Contains(city)) throw new CountryException("Country: addCity - city allready exists");
            this.Cities.Add(city);
        }

        public void removeCities(City c) {
            if (c == null) throw new CountryException("Country: removeCities - city is null");
            if (!this.Cities.Contains(c)) throw new CountryException("Country: removeCities - city doesn't exist");
            this.Cities.Remove(c);
        }

        public void addCapitals(List<City> capitals) {
            if (capitals == null) throw new CountryException("Country: addCapitals - capital is null");
            foreach(City c in capitals) {
                addCapital(c);
            }
        }

        public void addCapital(City capital) {
            if (capital == null) throw new CountryException("Country: addCapital - capital is null");
            if (this.Capitals.Contains(capital)) throw new CountryException("Country: addCapital - capital allready exists");
            if (!this.Cities.Contains(capital)) throw new CountryException("Country: addCapital - there was no capital in this city");
            this.Capitals.Add(capital);
        }


        public void removeCapitals(City cp) {
            if (cp == null) throw new CountryException("Country: removeCapitals - capital is null");
            if (!this.Capitals.Contains(cp)) throw new CountryException("Country: removeCapitals - there is no capital found");
            this.Capitals.Remove(cp);
        }

        public void addRivers(List<River> rivers) {
            if (rivers == null) throw new CountryException("Country: addRivers - rivern is null!");
            foreach(River r in rivers) {
                addRiver(r);
            }
        }


        public void addRiver(River riv) {
            if (riv == null) throw new CountryException("Country: addRiver - river is null!");
            if (this.Rivers.Contains(riv)) throw new CountryException("Country: addRiver - river allready exists");
            this.Rivers.Add(riv);
        }



        public void removeRiver(River r) {
            if (r == null) throw new CountryException("Country: removeRiver - river is null");
            if (!this.Rivers.Contains(r)) throw new CountryException("Country: removeRiver - river doesn't exist");
            this.Rivers.Remove(r);
        }
        #endregion

        #region GetHashCode
        public override bool Equals(object obj) {
            return obj is Country country &&
                   Id == country.Id &&
                   Name == country.Name &&
                   Population == country.Population &&
                   Surface == country.Surface &&
                   EqualityComparer<Continent>.Default.Equals(Continent, country.Continent) &&
                   EqualityComparer<ICollection<City>>.Default.Equals(Cities, country.Cities) &&
                   EqualityComparer<ICollection<City>>.Default.Equals(Capitals, country.Capitals) &&
                   EqualityComparer<ICollection<River>>.Default.Equals(Rivers, country.Rivers);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name, Population, Surface, Continent, Cities, Capitals, Rivers);
        }

        public override string ToString() {
            return string.Format("Country: {0}, {1}, {2}", this.Name, this.Population, this.Surface);
        }

        #endregion
    }
}
