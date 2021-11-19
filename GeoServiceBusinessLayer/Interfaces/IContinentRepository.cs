using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoServiceBusinessLayer.Models;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface IContinentRepository {
        Continent addContinent(Continent continent);
        Continent getContinentById(int id);
        IEnumerable<Continent> getAll();
        void delete(Continent continent);
        void deleteAll();
        void update(Continent continent);
        bool exists(Continent continent);
    }
}
