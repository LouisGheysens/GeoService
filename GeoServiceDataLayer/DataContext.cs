using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GeoServiceBusinessLayer.Models;
using GeoServiceDataLayer.Model;

namespace GeoServiceDataLayer {
    public class DataContext: DbContext {
        private string ConnectionString;
        public DataContext(string db = "Production") : base() {
            ConfigureConnectionString(db);
        }
        private void ConfigureConnectionString(string db) {
            switch (db) {
                case "Production":
                    ConnectionString = @"";
                    Database.EnsureCreated();
                    break;
                case "Test":
                    ConnectionString = @"";
                    Database.EnsureDeleted();
                    Database.EnsureCreated();
                    break;
            }
        }
        public DbSet<DTCity> Cities { get; set; }
        public DbSet<DTContinent> Continents { get; set; }
        public DbSet<DTCountry> Countries { get; set; }
        public DbSet<DTRiver> Rivers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<DTCountryRiver>().HasKey(x => new { x.CountryId, x.RiverId });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
