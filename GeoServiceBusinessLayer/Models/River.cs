using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using GeoServiceAPI.Mappings;

namespace GeoServiceBusinessLayer.Models {
    public class River {
        #region Constructor
        public River(string name, int length) {
            this.Name = name;
            this.Length = length;
        }

        public River() { }
        #endregion

        #region Properties
        [Key]
        public int RiverId { get; set; }

        public string Name { get; set; }

        public int Length { get; set; }
        public ICollection<RiverMapping> RiverMapping { get; set; }
        #endregion
    }
}
