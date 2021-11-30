using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class ContinentRepositoryException : Exception {
        public ContinentRepositoryException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
