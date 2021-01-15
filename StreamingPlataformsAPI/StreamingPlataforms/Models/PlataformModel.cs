using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Models
{
    public class PlataformModel
    {
        public int Id { get; set; }
        public string PlataformName { get; set; }
        public string Address { get; set; }
        [Range(1970, 2021)]
        public string FundationYear { get; set; }
        public IEnumerable<SerieModel> Series { get; set; }
    }
}
