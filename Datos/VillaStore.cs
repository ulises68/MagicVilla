using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using villaMagica.Modelos.Dto;

namespace villaMagica.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto>? villaList = new List<VillaDto>{
            
             new VillaDto { Id=1, Nombre="Vista a la Piscina", Ocupantes=3, MetrosCuadrados=50},
             new VillaDto { Id=2, Nombre="Vista a la playa",  Ocupantes=4, MetrosCuadrados=70}
            
        };
    }
}