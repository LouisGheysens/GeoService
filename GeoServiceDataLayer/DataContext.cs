using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using GeoServiceBusinessLayer.Models;

namespace GeoServiceDataLayer {
    public class DataContext: DbContext {
        private string connectionstring;

        public DataContext() { }

        public DataContext(string db = "production"): base() {
            SetConnectionstring(db);
        }

        private void SetConnectionstring(string db = "production") {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var conf = builder.Build();
            switch (db.ToLower()) {
                case "production":
                    this.connectionstring = conf.GetConnectionString("Production").ToString();
                    break;
                case "development":
                    this.connectionstring = conf.GetConnectionString("Development").ToString();
                    break;
            }
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<River> Rivers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder) {
            if(this.connectionstring == null) {
                this.SetConnectionstring();
                optionsbuilder.UseSqlServer(this.connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Country>()
                .HasMany<City>(c => Cities)
                .WithOne(x => x.Country);
            base.OnModelCreating(builder);
        }
    }
}
