using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using villaMagica.Datos;
using villaMagica.Modelos;
using villaMagica.Repositorio.IReposiorio;

namespace villaMagica.Repositorio
{
    // VillaRepositorio vamos a hacerle que herede del repositorio genérico
    //  y obviamente como repositorio genérico acepta cualquier tipo de entidad.
    //  Y adicional también que herede de su interfaz de IVillaRepositorio.
    public class VillaRepositorio : Repositorio<Villa>, IVillaRepositorio
    {
        // creamos una variable privada y solo de lectura para crear DbContext
        private readonly ApplicationDbContext _db;
        // ctor
        public VillaRepositorio(ApplicationDbContext db): base(db)
        {
            _db=db;
        }
        public async Task<Villa> Actualizar(Villa entidad)
        {
            // actualizamos con la fecha del sistema
            entidad.FechaCreacion = DateTime.Today;
            entidad.FechaActualizacion = DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}