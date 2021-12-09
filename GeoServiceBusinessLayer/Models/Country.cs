using GeoServiceBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GeoServiceBusinessLayer.Models {
    public class Country {

        #region Constructor
        public Country(string name, int population, int surface, Continent continent) {
            Name = name;
            Population = population;
            Surface = surface;
            Continent = continent;
        }
        #endregion

        private int _Id;
        public int Id {
            get { return _Id; }
            set {
                if (value < 1)
                    throw new CountryException("Country: Id has to be bigger than 0!");
                else { _Id = value; }
            }
        }

        private Continent _Continent;
        public Continent Continent {
            get { return _Continent; }
            set {
                if (value == null)
                    throw new CountryException("Country: Country can't be null!");
                else {
                    if(_Continent != null) {
                        _Continent.RemoveCountryFromContinent(this);
                    }
                    _Continent = value;
                    _Continent.AddCountryToContinent(this);
                }
            }
        }

        internal void AddRiver(River r) {
            Rivers.Add(r);
        }

        internal void RemoveRiver(River r) {
            Rivers.Remove(r);
        }

        private string _Name;
        public string Name {
            get { return _Name; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CountryException("Country: the name of a country can't be null!");
                else _Name = value;
            }
        }

        private int _Population;
        public int Population {
            get { return _Population; }
            set {
                if(value < 1)
                    throw new CountryException("Country: the population must be bigger than 0");
                    else {
                        _Population = value;
                    }
            }
        }

        private int _Surface;
        public int Surface {
            get { return _Surface; }
            set {
                if (value < 1)
                    throw new CountryException("Country: the surfacee area of a country must be bigger than 0");
                else {
                    _Surface = value;
                }
            }
        }

        private List<City> Capitals { get; set; } = new List<City>();
        private List<City> Cities { get; set; } = new List<City>();

        public ReadOnlyCollection<City> GetCities() {
            return Cities.AsReadOnly();
        }

        public ReadOnlyCollection<City> GetCapitals() {
            return Capitals.AsReadOnly();
        }

        public void AddCapital(City c) {
            if (!c.Capital)
                throw new CountryException("Country: The city was not a capital");
            else if (Capitals.Contains(c))
                throw new CountryException("Country: This city is allready a capital of this country");
            else if (!Cities.Contains(c)) {
                throw new CountryException("Country: This city is not a part of this country");
            }
            Capitals.Add(c);
        }

        public void AddCity(City c) {
            if (Cities.Contains(c))
                throw new CountryException("This city is allready oart of this country");
            else if (c.Country != this)
                throw new CountryException("Country: The country of this city did not equal this country");
            else {
                int total = 0;
                foreach(City r in Cities) {
                    total += r.Population;
                }
                total += c.Population;
                if (total > Population)
                    throw new CountryException("Country: The population of ths cities in a country can not be bigger than the" +
                        " population of that country");
                Cities.Add(c);
            }
        }

        public void RemoveCity(City c) {
            Cities.Remove(c);
            Capitals.Remove(c);
        }

        public void RemoveAsCapital(City c) {
            if (Capitals.Contains(c)) {
                Capitals.Remove(c);
                c.Capital = false;
            }
        }

        private List<River> Rivers { get; set; } = new List<River>();

        public ReadOnlyCollection<River> GetRivers() {
            return Rivers.AsReadOnly();
        }

        public override string ToString() {
            return string.Format("Country: {0}, {1}, {2}", this.Name, this.Population, this.Surface);
        }

        public override bool Equals(object obj) {
            if (obj is Country) {
                Country pop = obj as Country;

                return Name == pop.Name &&
                    Population == pop.Population &&
                    Surface == pop.Surface &&
                    Capitals.SequenceEqual(pop.Capitals) &&
                    Cities.SequenceEqual(pop.Cities);
            }
            else return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name, Population, Surface);
        }
    }
}
