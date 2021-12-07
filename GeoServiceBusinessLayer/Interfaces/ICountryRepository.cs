using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface ICountryRepository {
        Country AddCountry(Country country);
        Country GetCountryById(int id);
        void Delete(int countryId);
        Country Update(Country country);
    }
}
