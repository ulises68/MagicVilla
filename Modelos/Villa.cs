using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace villaMagica.Modelos
{
    public class Villa
    {
        // escribimos prop y presionamos tap
        // con Key indicamos que va ser nuestra llave primarioa
        [Key]
        // Genere automaticamente el Id
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // ? admite valores nulos
        public string? Nombre { get; set; }
        public string? Detalle { get; set; }
        [Required]
        public  double  Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
        public string? ImagenUrl { get; set; }
        // Son todos aquellos espacios o instalaciones dentro de
        // una propiedad capaces de proporcionar una mejor calidad 
        //de vida a sus habitantes
        public string? Amenidad { get; set; }
        public DateTime FechaCreacion{ get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}