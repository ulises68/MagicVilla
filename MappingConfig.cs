using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using villaMagica.Modelos;
using villaMagica.Modelos.Dto;

namespace villaMagica
{
    // Esta nueva clase tiene que heredar de algo llamado Profile
    //  que  es propio de nuestro paquete AutoMapper.
    public class MappingConfig: Profile
    {
        // ctor
        public MappingConfig()
        {
            // CreateMap<Fuente,Destino>
            // control . en villa para exportar Modelos y VillaDto para exportar Modelos.Dto
            CreateMap<Villa,VillaDto>();
            CreateMap<Villa,VillaDto>();

            CreateMap<Villa,VillaCreateDto>().ReverseMap();
            CreateMap<Villa,VillaUpdateDto>().ReverseMap();


        }
    }
}