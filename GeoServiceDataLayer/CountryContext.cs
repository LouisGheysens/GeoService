using GeoServiceDataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer {
    public class CountryContext: DbContext {

        private string ConnectionString;
        public CountryContext(string db = "Production") : base() {
            ConfigureConnectionString(db);
        }
        private void ConfigureConnectionString(string db) {
            switch (db) {
                case "Production":
                    ConnectionString = @"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=GeoServiceDB;Integrated Security=True";
                    Database.EnsureCreated();
                    break;
                case "Test":
                    ConnectionString = @"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=GeoServiceTEST;Integrated Security=True";
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
