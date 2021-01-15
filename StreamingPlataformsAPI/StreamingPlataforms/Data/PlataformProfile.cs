using AutoMapper;
using StreamingPlataforms.Data.Entities;
using StreamingPlataforms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingPlataforms.Data
{
    public class PlataformProfile : Profile
    {
        public PlataformProfile()
        {
            this.CreateMap<PlataformEntity, PlataformModel>()
                .ReverseMap();
            this.CreateMap<SerieEntity, SerieModel>()
               .ReverseMap();
        }
    }
}
