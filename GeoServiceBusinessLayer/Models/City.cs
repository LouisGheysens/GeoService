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
        public City(string name, Country country, int population) {
            setName(name);
            setCountry(country);
            setPopulation(population);
        }

        public City() { }
        #endregion

        #region Properties
        public int Id { get; private set; }

        public string Name { get; private set; }

        public int Population { get; private set; }

        public virtual Country Country { get; private set; }
        #endregion

        #region Methods
        public void setPopulation(int population) {
            if (population < 0) throw new CityException("City: setPopulation - population can't be lower than zero");
            if (population < this.Country.Population) throw new CityException("City: setPopulation - Population can't be greater than countrypopulation");
            this.Population = population;
        }

        public void setCountry(Country country) {
            if (country == null) throw new CityException("City: setCountry - country is null");
            this.Country = country;
        }

        public void setName(string name) {
            if (string.IsNullOrWhiteSpace(name)) throw new CityException("City: setName - name is null");
            this.Name = name;
        }
        #endregion

        #region GetHashCode
        public override bool Equals(object obj) {
            return obj is City city &&
                   Id == city.Id &&
                   Name == city.Name &&
                   Population == city.Population &&
                   EqualityComparer<Country>.Default.Equals(Country, city.Country);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name, Population, Country);
        }
        #endregion

        #region ToString
        public override string ToString() {
            return string.Format("City: {0}, {1}", this.Name, this.Population);
        }
        #endregion
    }
}
