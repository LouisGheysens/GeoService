using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface IRiverRepository {
        River addRiver(River river);
        River getRiverById(int id);
        IEnumerable<River> getAll();
        void delete(River river);
        void deleteAll();
        void updateRiver(River river);
        bool exists(River river);
    }
}
