 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Models
{
    public class SerieModel
    {
        
        public int SerieId { get; set; }
        public string SerieName { get; set; }
        public string Country { get; set; }
        public string Rate { get; set; }
        public string   PlataformId { get; set; }
    }
}
