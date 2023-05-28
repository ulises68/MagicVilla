using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace villaMagica.Modelos
{
    public class APiResponse
    {
        // Va a almacenar el código estado que retorne el EndPoint.
        public HttpStatusCode statusCode { get; set; }
        // para checar si fue exitoso
        public bool IsExitoso { get; set; } = true;
        // Esta va a ser una lista de tipo string  de todos los errores que se presenten 
        // que puede ser uno o varios errores.
        public List<string>? ErrorMenssages { get; set;}
        // El Resultado es de tipo objeto porque el resultado del EndPoint, puede ser una lista, 
        // puede ser un objeto con un solo resultado. No sabemos realmente qué es lo que a retornar
        public object? Resultado { get; set; }
        
    }

    
}