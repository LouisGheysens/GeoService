using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface IRiverRepository {
        void AddRiver(River river, int countryId);
        River GetRiver(int id);
        IEnumerable<River> GetAll();
        void RemoveRiver(int riverId);
        void UpdateRiver(River river);
        bool ExistsRiver(int id);
    }
}
