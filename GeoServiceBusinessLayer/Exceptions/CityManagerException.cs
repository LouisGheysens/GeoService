using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class CityManagerException : Exception {
        public CityManagerException(string message) : base(message) {
        }

        public CityManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
