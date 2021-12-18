using GeoServiceBusinessLayer.Exceptions;
using GeoServiceBusinessLayer.Interfaces;
using GeoServiceBusinessLayer.Models;
using GeoServiceDataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoServiceDataLayer.Repositories {
    public class RiverRepository : IRiverRepository {

        protected CountryContext context;

        public RiverRepository(CountryContext context) {
            this.context = context;
        }

        public River AddRiver(River river) {
            DTRiver rt = DataConverter.ConvertRiverToRiverData(river);
            context.Rivers.Add(rt);
            context.SaveChanges();
            return DataConverter.ConvertRiverDataToRiver(rt);
        }

        public void Delete(int riverId) {
            DTRiver dt = context.Rivers.Find(riverId);
            context.Rivers.Remove(dt);
            context.SaveChanges();
        }

        public River GetRiverById(int id) {
            DTRiver dt = context.Rivers.Find(id);
            if (dt == null)
                return null;
            else
                return DataConverter.ConvertRiverDataToRiver(dt);
        }

        public River UpdateRiver(River river) {
            DTRiver dt = DataConverter.ConvertRiverToRiverData(river);
            DTRiver original = context.Rivers.Find(dt.Id);
            original.CountryLink = dt.CountryLink;
            original.Length = dt.Length;
            original.Name = dt.Name;
            context.Rivers.Update(original);
            context.SaveChanges();
            return DataConverter.ConvertRiverDataToRiver(original);
        }
    }
}
