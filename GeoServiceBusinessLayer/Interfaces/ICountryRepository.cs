using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface ICountryRepository {
        Country addCountry(Country country);
        Country getById(int id);
        IEnumerable<Country> getAll();
        void delete(Country country);
        void deleteAll();
        void update(Country country);
        bool exists(Country country);
    }
}
