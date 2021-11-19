using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class CountryManagerException : Exception {
        public CountryManagerException(string message) : base(message) {
        }

        public CountryManagerException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
