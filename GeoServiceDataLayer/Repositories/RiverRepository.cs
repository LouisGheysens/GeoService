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
    public class RiverRepository : IRiverRepository {

        protected DataContext context;

        public RiverRepository(DataContext context) {
            this.context = context;
        }


        public River addRiver(River river) {
            try {
                this.context.Rivers.Add(river);
                return river;
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: addRiver - gefaald", ex);
            }
        }

        public void delete(River river) {
            try {
                this.context.Rivers.Remove(river);
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: delete - gefaald", ex);
            }
        }

        public void deleteAll() {
            try {
                this.context.Rivers.RemoveRange(context.Rivers);
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: deleteAll - gefaald", ex);
            }
        }

        public bool exists(River river) {
            try {
                return this.context.Rivers.Contains(river);
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: exists - gefaald", ex);
            }
        }

        public IEnumerable<River> getAll() {
            try {
                return this.context.Rivers
                    .Include(river => river.Countries).ThenInclude(country => country.Continent)
                    .ThenInclude(continent => continent.Countries).Include(river => river.Countries)
                    .ThenInclude(country => country.Cities).ThenInclude(city => city.Country)
                    .Include(river => river.Countries).ThenInclude(country => country.Rivers)
                    .ThenInclude(river => river.Countries).Include(river => river.Countries)
                    .ThenInclude(country => country.Capitals).ThenInclude(capital => capital.Country)
                    .ToList<River>();
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: getAll - gefaald", ex);
            }
        }

        public River getRiverById(int id) {
            try {
                return this.context.Rivers
                    .Include(river => river.Countries).ThenInclude(country => country.Continent)
                    .ThenInclude(continent => continent.Countries).Include(river => river.Countries)
                    .ThenInclude(country => country.Cities).ThenInclude(city => city.Country)
                    .Include(river => river.Countries).ThenInclude(country => country.Rivers)
                    .ThenInclude(river => river.Countries).Include(river => river.Countries)
                    .ThenInclude(country => country.Capitals).ThenInclude(capital => capital.Country)
                    .Where(x => x.Id == id).SingleOrDefault();
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: getRiverById - gefaald", ex);
            }
        }

        public void updateRiver(River river) {
            try {
                this.context.Rivers.Update(river);
            }catch(Exception ex) {
                throw new RiverRepositoryException("RiverRepository: udateRiver - gefaald", ex);
            }
        }
    }
}
