using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Exceptions {
    public class CountryException : Exception {
        public CountryException(string message) : base(message) {
        }

        public CountryException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
