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
    public class City {

        #region Constructor
        public City(string name, int population, Country country, bool capital){
            Name = name;
            Population = population;
            Country = country;
            Capital = capital;
        }

        public City() { }
        #endregion

        #region Properties
        public int Id { get; set; }

        private string _Name;
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value;
            }
        }

        private int _Population;
        public int Population {
            get { return _Population; }
            set {
                if (value < 1)
                    throw new CityException("City: The population must be bigger than 0");
                _Population = value;
            }
        }

        private Country _Country;
        public Country Country {
            get { return _Country; }
            set {
                if (value == null)
                    throw new CityException("City: City must belong to a country!");
                else {
                    Country oldCountry = Country;
                    _Country = value;
                    if(oldCountry != null) {
                        oldCountry.RemoveCity(this);
                    }
                    Country.AddCity(this);
                    if (Capital)
                        Country.AddCapital(this);
                }
            }
        }

        private bool _Capital;
        public bool Capital {
            get { return _Capital;}
            set {
                bool oldValye = _Capital;
                _Capital = value;
                if(value ==true && oldValye == false) {
                    Country.AddCapital(this);
                }
                else if (value == false && oldValye == true) {
                    Country.RemoveAsCapital(this);
                }
            }
        }

        #endregion

        #region ToString
        public override string ToString() {
            return string.Format("City: {0}, {1}", this.Name, this.Population);
        }

        public override bool Equals(object obj) {
            if (obj is City) {
                City p = obj as City;
                return p.Name == Name && p.Population == Population && p.Capital == Capital;
            }
            else return false;
        }
        #endregion
    }
}
