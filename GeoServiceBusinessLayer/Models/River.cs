using System;
using System.Collections.Generic;
using System.Linq;
using GeoServiceBusinessLayer.Exceptions;
using System.Collections.ObjectModel;

namespace GeoServiceBusinessLayer.Models {
    public class River {
        #region Constructor
        public River(string name, int length, List<Country> countries) {
            Name = name;
            Length = length;
            SetCountries(countries);
        }
        #endregion

        public void SetCountries(List<Country> countries) {
            if (countries == null || countries.Count < 1)
                throw new RiverException("River: River must belong to at least one country");
            else {
                if (countries.Count == countries.Distinct().Count()) {
                    foreach (Country cr in Countries) {
                        cr.RemoveRiver(this);
                    }
                    Countries = new List<Country>();
                    foreach (Country r in countries) {
                        Countries.Add(r);
                        r.AddRiver(this);
                    }
                }
                else throw new RiverException("River: The list of countries contained doubles");
            }
        }
        public ReadOnlyCollection<Country> GetCountries() {
            return Countries.AsReadOnly();
        }

        private List<Country> Countries { get; set; } = new List<Country>();

        public int Id { get; set; }

        private string _Name;
        public string Name {
            get { return _Name; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new RiverException("River: A ruver's name can not be null or empty");
                else _Name = value;
            }
        }

        private int _Length;
        public int Length {
            get { return _Length; }
            set {
                if (value < 1)
                    throw new RiverException("River: A river's length must be longer than 0");
                else _Length = value;
            }
        }


        #region ToString()
        public override string ToString() {
            return string.Format("River: {0}, {1}", this.Name, this.Length);
        }

        public override bool Equals(object obj) {
            if (obj is River) {
                River r = obj as River;
                return r.Name == Name && r.Length == Length && r.GetCountries().SequenceEqual(GetCountries());
            }
            else return false;
        }
        #endregion

    }
}
