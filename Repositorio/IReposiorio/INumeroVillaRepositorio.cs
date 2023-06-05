using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using villaMagica.Modelos;

namespace villaMagica.Repositorio.IReposiorio
{
    // Vamos a indicarle que esta interfaz herede de la interfaz de repositorio genérico
    // y la entidad que va a trabajar se va a llamar NumeroVilla.
    public interface INumeroVillaRepositorio: IRepositorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar (NumeroVilla entidad);
    }
    // Esto es toda la declaración de nuestra interfaz;  y al heredar de la interfaz genérica,
    // va a tener acceso a todos los métodos definidos en la interfaz genérica.
}