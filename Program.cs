using Microsoft.EntityFrameworkCore;
using villaMagica;
using villaMagica.Datos;
using villaMagica.Repositorio;
using villaMagica.Repositorio.IReposiorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// De esta manera el soporte será agregado al servicio.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Agregamos un servicio con Builder.Services
builder.Services.AddDbContext<ApplicationDbContext>( option =>
    {
        // dentro del parentesis colocamos la cadena de conexion
        // para acceder al cadena de conexion usamos Builder.Configuration.GetConnectionString
        option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConexion"));
    }
);
builder.Services.AddAutoMapper(typeof(MappingConfig));

// En el AddScoped se se agregue la interfaz de IVilaRepositorio,
//  con su respectiva implementación, que viene a ser Villarepositorio.
builder.Services.AddScoped<IVillaRepositorio, VillaRepositorio>();
builder.Services.AddScoped<INumeroVillaRepositorio, NumeroVillaRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
