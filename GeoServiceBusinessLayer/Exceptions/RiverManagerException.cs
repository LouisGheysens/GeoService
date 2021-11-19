using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class RiverManagerException : Exception {
        public RiverManagerException(string message) : base(message) {
        }

        public RiverManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
