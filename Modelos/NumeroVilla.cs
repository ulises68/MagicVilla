using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace villaMagica.Modelos
{
    public class NumeroVilla
    {
        // usamos notacion de DataAnnotations [Key] para indicarle que la propiedad
        // VillaNo va ser la llave primaria, ademas No querenos que se asigne automaticamente
        // el Id, para poder ingresarlo manualmente hacemos uso de DatabaseGenerated
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)] // using System.ComponentModel.DataAnnotations.Schema;
        // [Key] using System.ComponentModel.DataAnnotations;
         public int VillaNo { get; set; }
        // vamos a tener una relacion con la tabla Villa, para crear una relacion tenemos que crear una 
        // propiedad que sea lo mas descriptiva, para este caso usamos VillaId
        [Required]
        public int VillaId { get; set; }
        // La villaId la tenemos que relacionar con la tabla Villa
        [ForeignKey("VillaId")]
        public Villa Villa { get; set; } = null!;

        public string  DetalleEspecial{ get; set; } = string.Empty; // inicializamos una cadena vacia
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion{ get; set; }
  
    }
}