using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Model.Input {
    public class RiverDTOInput {
        public int RiverId { get; set; }

        public string Name { get; set; }

        public int Length { get; set; }

        public int[] CountryIdArray { get; set; }
    }
}
