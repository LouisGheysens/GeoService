using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface IRiverRepository {
        River AddRiver(River river);
        River GetRiverById(int id);
        void Delete(int riverId);
        River UpdateRiver(River river);
    }
}
