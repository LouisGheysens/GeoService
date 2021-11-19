using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class ContinentException : Exception {
        public ContinentException(string message) : base(message) {
        }

        public ContinentException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
