using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Data.Entities
{
    public class PlataformEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Column(TypeName="nvarchar(100)")]
        public string PlataformName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(4)")]
        public string FundationYear { get; set; }
        public virtual ICollection<SerieEntity> Series { get; set; }
    }
}
