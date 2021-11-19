using GeoServiceAPI.Mappings;
using GeoServiceBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public class Continent {
        #region Constructor
        public Continent(string name) {
            setName(name);
        }

        public Continent() { }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();
        #endregion

        #region Methods
        public void setName(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new ContinentException("Continent: setName - name is null");
            this.Name = name;
        }

        public void addCountries(List<Country> countries) {
            if (countries == null) throw new ContinentException("Continent: addCountries - country is null");
            foreach(Country c in countries) {
                addCountry(c);
            }
        }

        public void addCountry(Country country) {
            if (country == null) throw new ContinentException("Continent: addCountry - country is null");
            if (this.Countries.Contains(country)) throw new ContinentException("Continent: addCountry - country allready exists");
            this.Countries.Add(country);
            this.Population = this.Countries.Sum(x => x.Population);
        }

        public void removeCountry(Country countries) {
            if (countries == null) throw new ContinentException("Continent: removeCountry - country is null");
            if (!this.Countries.Contains(countries)) throw new ContinentException("Continent: removeCountry - country doesn't exist");
            this.Countries.Remove(countries);
            this.Population = this.Countries.Sum(x => x.Population);
        }
        #endregion

        #region GetHashCode
        public override bool Equals(object obj) {
            return obj is Continent continent &&
                   Id == continent.Id &&
                   Name == continent.Name &&
                   Population == continent.Population &&
                   EqualityComparer<ICollection<Country>>.Default.Equals(Countries, continent.Countries);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name, Population, Countries);
        }
        #endregion

        #region ToString
        public override string ToString() {
            return string.Format("Continent: {0}", this.Name);
        }

        #endregion
    }
}
