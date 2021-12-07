using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface InterfaceCollection {
        ICityRepository Cities { get; set; }

        IContinentRepository Continents { get; set; }

        ICountryRepository Countries { get; set; }

        IRiverRepository Rivers { get; set; }

    }
}
