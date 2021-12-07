using GeoServiceAPI.Mappings;
using GeoServiceBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Models {
    public class Continent {
        #region Constructor
        public Continent(string name) {
            Name = name;
        }

        #endregion

        #region Properties
        private int _Id;
        public int Id {
            get { return _Id; }
            set {
                if (value < 1)
                    throw new ContinentException("Contient: Id can't be lower than 1");
                else
                    _Id = value;
            }
        }
        private string _Name;
        public string Name {
            get { return _Name; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ContinentException("Continent: Name can't be null or empty!");
                else _Name = value;
            }
        }

        public int GetPopulation() {
            int aantal = 0;
            foreach(Country l in Countries) {
                aantal += l.Population;
            }
            return aantal;
        }

        public void RemoveCountryFromContinent(Country c) {
            if (Countries.Contains(c))
                Countries.Remove(c);
            else throw new ContinentException("Continent: the givzn country is not part of the continent that was given!");
        }

        public void AddCountryToContinent(Country c) {
            if (!c.Continent.Equals(this))
                throw new ContinentException("Continent: the continent of this country did not equal this continent");
            foreach (Country r in Countries) {
                if (r.Equals(c))
                    throw new ContinentException("Continent: this country is allready part of this continent!");
                else if (r.Name == c.Name)
                    throw new ContinentException("Continent: The name of the country must be unique within the continent!");
            }
            Countries.Add(c);
        }

        private List<Country> Countries { get; set; } = new List<Country>();

        public void SetCountries(List<Country> countries) {
            List<Country> newCountries = new List<Country>();
            foreach(Country p in countries) {
                newCountries.Add(p);
            }
            Countries = newCountries;
        }

        public ReadOnlyCollection<Country> GetCountries() {
            return Countries.AsReadOnly();
        }
        #endregion


        #region ToString
        public override string ToString() {
            return string.Format("Continent: {0}", this.Name);
        }

        public override bool Equals(object obj) {
            if (obj is Continent) {
                Continent c = obj as Continent;
                return c.Name == Name &&
                    c.Countries.SequenceEqual(Countries);
            }
            else return false;
        }

        #endregion
    }
}
