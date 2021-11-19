using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoServiceBusinessLayer.Models;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface ICityRepository {
        City addCity(City city);
        City getCityById(int id);
        IEnumerable<City> getAll();
        void delete(City city);
        void deleteAll();
        void update(City city);
        bool exists(City city);
    }
}
