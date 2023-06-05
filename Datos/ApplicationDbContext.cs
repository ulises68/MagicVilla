using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using villaMagica.Modelos;

namespace villaMagica.Datos
{
    public class ApplicationDbContext:DbContext
    {
        // ctor
        // La clase de Dbcontext de donde se hereda es de base que es el padre y pasamos las opciones
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; } = null!;
        public DbSet<NumeroVilla> NumeroVillas { get; set; } = null!;

        //  Vamos crear un  método que pertenece al DbContext llamado un OnModelCreating().
        //  Este método nosotros lo vamos a hacer override para cambiar sus características
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Para agregar datos utilizamos HasData() 
            modelBuilder.Entity<Villa>().HasData(
                // de esta forma podemos crear registros cuando ejecutamos una migracio
                new Villa()
                     {
                        Id=1,
                        Nombre ="Villa Real",
                        Detalle="Villa en el lago  ....",
                        ImagenUrl="",
                        Ocupantes=5,
                        MetrosCuadrados=50,
                        Tarifa=200,
                        Amenidad="Sonido Inteligente",
                        FechaCreacion = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                     },
                     new Villa()
                     {
                        Id=2,
                        Nombre ="Premiu Vista a la Piscina",
                        Detalle="Villa con puesta al mar....",
                        ImagenUrl="",
                        Ocupantes=4,
                        MetrosCuadrados=40,
                        Tarifa=150,
                        Amenidad="Luz Inteligente",
                        FechaCreacion = DateTime.Now,
                        FechaActualizacion = DateTime.Now
                     }
             );
        }
    }
}
