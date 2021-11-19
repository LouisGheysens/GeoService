using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Interfaces {
    public interface InterfaceCollection: IDisposable {
        ICityRepository Cities { get; }

        IContinentRepository Continents { get; }

        ICountryRepository Countries { get; }

        IRiverRepository Rivers { get; }

        int Complete();
    }
}
