using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Repositories {
    public class ContinentRepository : IContinentRepository {

        protected DataContext context;

        public ContinentRepository(DataContext context) {
            this.context = context;
        }


        public Continent addContinent(Continent continent) {
            try {
                this.context.Continents.Add(continent);
                return continent;
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository: addContinent -  gefaald", ex);
            }
        }

        public void delete(Continent continent) {
            try {
                this.context.Continents.Remove(continent);
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository : delete - gefaald", ex);
            }
        }

        public void deleteAll() {
            try {
                this.context.Continents.RemoveRange(context.Continents);
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository: deleteAll - gefaald", ex);
            }
        }

        public bool exists(Continent continent) {
            try {
                return this.context.Continents.Contains(continent);
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContientRepository: exists - gefaald", ex);
            }
        }

        public IEnumerable<Continent> getAll() {
            try {
                return this.context.Continents
                    .Include(continent => continent.Countries).ThenInclude(country => country.Cities)
                    .ThenInclude(city => city.Country).Include(continent => continent.Countries)
                    .ThenInclude(country => country.Rivers).ThenInclude(river => river.Countries)
                    .Include(continent => continent.Countries).ThenInclude(country => country.Capitals)
                    .ThenInclude(capital => capital.Country).ToList<Continent>();
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository: getAll - gefaald", ex);
            }
        }

        public Continent getContinentById(int id) {
            try {
                return this.context.Continents
                    .Include(continent => continent.Countries).ThenInclude(country => country.Cities)
                    .ThenInclude(city => city.Country).Include(continent => continent.Countries)
                    .ThenInclude(country => country.Rivers).ThenInclude(rivers => rivers.Countries)
                    .Include(continent => continent.Countries).ThenInclude(country => country.Capitals)
                    .ThenInclude(capital => capital.Country).Where(c => c.Id == id).SingleOrDefault();
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository: getContinentById(int id) - gefaald", ex);
            }
        }

        public void update(Continent continent) {
            try {
                this.context.Continents.Update(continent);
            }catch(Exception ex) {
                throw new ContinentRepositoryException("ContinentRepository: update - gefaald", ex);
            }
        }
    }
}
