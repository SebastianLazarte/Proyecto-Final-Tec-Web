using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Data.Entities
{
    public class SerieEntity
    {
        [Key]
        [Required]
        public int SerieId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string SerieName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Country { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public int Rate { get; set; }
        [ForeignKey("PlataformId")]
        public virtual PlataformEntity Plataform { get; set; }
        public int PlataformId { get; set; }


    }
}
