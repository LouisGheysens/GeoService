using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoServiceBusinessLayer.Models;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface ICityRepository {
        City AddCity(City city);
        City GetCityById(int id);
        void Delete(int city);
        City Update(City cityId);
    }
}
