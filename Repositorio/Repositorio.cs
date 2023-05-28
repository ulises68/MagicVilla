using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using villaMagica.Datos;
using villaMagica.Repositorio.IReposiorio;

namespace villaMagica.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        // Necesitamos Inyectar nuestro DbContext
        // Creamos una variable privada solo de lectura del la clase
        //  ApplicationDbContext
        private readonly ApplicationDbContext _db;
        // Vamos a necesitar una varible internal de tipo DbSet<>
        // Que es el que va a ser la conversión con <T> que es lo que recibiremos
        // para convertirlo en una entidad.
        internal DbSet<T> dbSet;
        // creamos un constructor con ctor para inyectar el DbContext
        public Repositorio(ApplicationDbContext db)
        {
            _db=db;
            // de esta forma estamos convietiendo la T en una Entidad
            this.dbSet = _db.Set<T>(); // La diferencia del Dbset<> es que conocemos el Dbcontext
                                       // y el Set<> no lo conocemos.
        }
        public async Task Crear(T entidad)
        {
            // dbset nuestra entidad y usamos el metodo de DbContext AddAsync Para agregar un registro
            // en la base de datos.
            await dbSet.AddAsync(entidad);
            // grabamos los cambios
            await Grabar();
        }

        public async Task Grabar()
        {
             // grabamos los datos en la base de datos.
            await _db.SaveChangesAsync();
        }
           // Ahora vamos  a crea rel metodo obtener que recibe Un filtro y solamente nos va a retornar un solo registro.
        public async Task<T> Obtener(Expression<Func<T, bool>> filtro = null!, bool tracked = true)
        {
            // necesitamos una variable IQueryable ya que vamos a realizar Querys o consultas dentro de este metodo.
            // dbset es la conversion de la Entidad;
            IQueryable<T> query = dbSet; 
            // si no es Tracked
            if (!tracked) // si es falso aplicamos el AsNoTracking
            {
                   query = query.AsNoTracking();
            }
            // Si este filtro es diferente a null, es decir, nos están enviando un filtro
            // nos están enviando una expresión linq
            if (filtro !=null)
            {
                // para el filtrado usamo Where , que trabaja con una expresion Linq
                query = query.Where(filtro);
            } 
            // Se cumpla o no se cumpla cualquiera de estos if al final siempre va a ser un retorno
            // del query usando FirsrorDefaultAsync() porque solamente queremos retornar un solo registro.
            // FirsrorDefaultAsync() es un metodo propio de DbContext.
            return await query.FirstOrDefaultAsync() ?? null!;  // No nulo
                     
        }
        // El metodo ObtenerTodos pero a diferencia del obtener,este va a retornar una lista.
        // El tracked acá no es necesario, pero el filtro si es necesario.
        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null)
        {
            IQueryable<T> query = dbSet; 

            if (filtro !=null){
                query = query.Where(filtro);
            } 
            
            // retornmamos una Lista ya sea filtrada o solo retorna todo los registros sin filtro
            return await query.ToListAsync(); 

        }

        public async Task Remover(T entidad)
        {
           dbSet.Remove(entidad);
           await Grabar();
        }
    }
}