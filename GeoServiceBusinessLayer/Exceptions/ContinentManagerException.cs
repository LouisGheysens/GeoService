using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class ContinentManagerException : Exception {
        public ContinentManagerException(string message) : base(message) {
        }

        public ContinentManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
