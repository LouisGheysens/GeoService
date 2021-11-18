using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface ICountryRepository {
        void AddCountry(Country country, int id);
        Country GetCountry(int continentId, int countryid);
        IEnumerable<Country> GetAll();
        void RemoveCountry(int continentId, int countryId);
        void UpdateCountry(Country country);
        bool ExistsCountry(int id);
    }
}
