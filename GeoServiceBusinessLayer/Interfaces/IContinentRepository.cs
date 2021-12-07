using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoServiceBusinessLayer.Models;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface IContinentRepository {
        Continent AddContinent(Continent continent);
        Continent GetContinentById(int id);
        void Delete(int continentId);
        Continent Update(Continent continent);
        bool IsNameAvailable(string name);
    }
}
