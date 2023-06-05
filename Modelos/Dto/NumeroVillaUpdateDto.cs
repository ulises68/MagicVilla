using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace villaMagica.Modelos.Dto
{
    public class NumeroVillaUpdateDto
    {
       [Required]
         public int VillaNo { get; set; }
       [Required]
        public int VillaId { get; set; }
        public string  DetalleEspecial{ get; set; } = string.Empty; // inicializamos una cadena vacia
                
    }

}