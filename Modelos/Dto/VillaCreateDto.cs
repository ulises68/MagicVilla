using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace villaMagica.Modelos.Dto
{
    public class VillaCreateDto
    {
        // public int Id { get; set; }
        // admite valores nulos
        //Hay unas anotaciones incorporadas en .Net Core llamada DataAnnotations. 
        //Estas anotaciones pueden ser aplicadas en los modelos de clase.
        [Required(ErrorMessage = "El Nombre es Obligatorio.")]
        [MaxLength(30, ErrorMessage = "El Nombre debe tener una logitud maxima de 30 Caracteres.")]
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        [Required]
        public  double  Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Amenidad { get; set; }
    }
}