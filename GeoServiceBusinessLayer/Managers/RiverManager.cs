using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceBusinessLayer.Managers {
    public class RiverManager {

        private readonly InterfaceCollection _icollection;

        public RiverManager(InterfaceCollection _icollection) {
            this._icollection = _icollection;
        }

        public River add(River river) {
            if (_icollection.Rivers.exists(river)) throw new RiverManagerException("RiverManager: add - riverError");
            try {
                foreach(Country c in river.Countries) {
                    _icollection.Countries.update(c);
                }
                return _icollection.Rivers.getAll().Last();
            }catch(Exception ex) {
                throw new RiverManagerException("RiverManager: add - failed", ex);
            }
        }

        public River get(int id) {
            try {
                return _icollection.Rivers.getRiverById(id);
            }catch(Exception ex) {
                throw new RiverManagerException("RiverManager: get - failed", ex);
            }
        }

        public IEnumerable<River> getAll() {
            try {
                return _icollection.Rivers.getAll();
            }catch(Exception ex) {
                throw new RiverManagerException("RiverManager: getAll - failed", ex);
            }
        }

        public void delete(River river) {
            try {
                _icollection.Rivers.delete(river);
                _icollection.Complete();
            }catch(Exception ex) {
                throw new RiverManagerException("RiverManager: delete - failed", ex);
            }
        }

        public void deleteAll() {
            try {
                _icollection.Rivers.deleteAll();
                _icollection.Complete();
            }catch(Exception ex) {
                throw new RiverManagerException("RiverManager: deleteAll - failed", ex);
            }
        }

        public void update(River river) {
            try {
                _icollection.Rivers.updateRiver(river);
                _icollection.Complete();
            }
            catch (Exception ex) {
                throw new RiverManagerException("RiverManager: update - failed", ex);
            }
        }

        public bool exists(River river) {
            return _icollection.Rivers.exists(river);
        }
    }
}
