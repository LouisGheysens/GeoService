using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Managers {
    public class ContinentManager {

        private readonly InterfaceCollection _icollection;

        public ContinentManager(InterfaceCollection _icollection) {
            this._icollection = _icollection;
        }

        public Continent add(Continent continent) {
            if (_icollection.Continents.exists(continent)) throw new ContinentManagerException("ContinentManager: add - continent allready exists");
            try {
                continent = _icollection.Continents.addContinent(continent);
                _icollection.Complete();
                return continent;
            }catch(Exception ex) {
                throw new ContinentManagerException("ContinentManager: add - failed", ex);
            }
        }

        public Continent get(int id) {
            try {
               return _icollection.Continents.getContinentById(id);
            }catch(Exception ex) {
                throw new ContinentManagerException("ContinentManager: get - failed", ex);
            }
        }

        public IEnumerable<Continent> getAll() {
            try {
                return _icollection.Continents.getAll();
            }catch(Exception ex) {
                throw new ContinentManagerException("ContinentManager: getAll - failed", ex);
            }
        }

        public void delete(Continent continent) {
            try {
                if (continent.Countries.Count != 0) throw new ContinentManagerException("ContinentManager: delete - There are still countries");
                _icollection.Continents.delete(continent);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new ContinentManagerException("ContinentManager: delete - failed", ex);
            }
        }

        public void deleteAll() {
            try {
                _icollection.Continents.deleteAll();
                _icollection.Complete();
            }
            catch (Exception ex) {
                throw new ContinentManagerException("ContinentManager: deleteAll - failed", ex);
            }
        }

        public void update(Continent continent) {
            try {
                _icollection.Continents.update(continent);
                _icollection.Complete();
            }
            catch (Exception ex) {
                throw new ContinentManagerException("ContinentManager: update - failed", ex);
            }
        }

        public bool exists(Continent c) {
            return _icollection.Continents.exists(c);
        }
    }
}
