using GeoServiceBusinessLayer;
using GeoServiceDataLayer;
using System;
using static System.Console;

namespace GeoServiceAPP {
    class Program {
        static void Main(string[] args) {
            DataAcces DA = new DataAcces("Test");
            CountryManager cg = new CountryManager(DA);

        }
    }
}
