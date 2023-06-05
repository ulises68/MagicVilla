using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using villaMagica.Datos;
using villaMagica.Modelos;
using villaMagica.Repositorio.IReposiorio;

namespace villaMagica.Repositorio
{
    // NumeroVillaRepositorio vamos a hacerle que herede del repositorio genérico
    //  y obviamente como repositorio genérico acepta cualquier tipo de entidad.
    //  Y adicional también que herede de su interfaz de IVillaRepositorio.
    public class NumeroVillaRepositorio : Repositorio<NumeroVilla>, INumeroVillaRepositorio
    {
        // creamos una variable privada y solo de lectura para crear DbContext
        private readonly ApplicationDbContext _db;
        // ctor
        public NumeroVillaRepositorio(ApplicationDbContext db): base(db)
        {
            _db=db;
        }
        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            // actualizamos con la fecha del sistema
            entidad.FechaActualizacion = DateTime.Now;
            _db.NumeroVillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}