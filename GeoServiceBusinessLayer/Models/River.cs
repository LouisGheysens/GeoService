using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GeoServiceAPI.Mappings;
using GeoServiceBusinessLayer.Exceptions;

namespace GeoServiceBusinessLayer.Models {
    public class River {
        #region Constructor
        public River(string name, int length, List<Country> countries) {
            setName(name);
            setLength(length);
            addCountries(countries);
        }
        public River() { }
        #endregion

        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Length { get; private set; }
        public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();
        #endregion

        #region Methods
        public void addCountries(List<Country> countries) {
            if (countries == null) throw new RiverException("River: addCountries - countries is null");
            foreach(Country c in countries) {
                addCountry(c);
            }
        }

        public void addCountry(Country c) {
            if (c == null) throw new RiverException("River: addCountry - country is null");
            if (this.Countries.Contains(c)) throw new RiverException("River: addCountry - country allready exists");
            this.Countries.Add(c);
        }

        public void removeCountry(Country c) {
            if (c == null) throw new RiverException("River: removeCountry - country is null");
            if (!this.Countries.Contains(c)) throw new RiverException("River: removeCountry: - country doesn't exists");
            this.Countries.Remove(c);
        }

        public void setLength(int length) {
            if (length < 0) throw new RiverException("River: setLength - Length is lower or is null");
            this.Length = length;
        }

        public void setName(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new RiverException("River: setName - Name is empty");
            this.Name = name;
        }
        #endregion

        #region GetHashCode()
        public override bool Equals(object obj) {
            return obj is River river &&
                   Id == river.Id &&
                   Name == river.Name &&
                   Length == river.Length &&
                   EqualityComparer<ICollection<Country>>.Default.Equals(Countries, river.Countries);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name, Length, Countries);
        }
        #endregion
        #region ToString()
        public override string ToString() {
            return string.Format("River: {0}, {1}", this.Name, this.Length);
        }
        #endregion

    }
}
