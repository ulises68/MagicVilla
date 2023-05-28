using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using villaMagica.Datos;
using villaMagica.Modelos;
using villaMagica.Modelos.Dto;
using villaMagica.Repositorio.IReposiorio;

namespace villaMagica.Controllers
{
    // Esto es la ruta donde indicamos que la ruta va a ser API. 
    // Luego de eso viene un slash y luego de eso va a venir el nombre del controlador, el nombre del controlador
    // sera villa.
    [Route("api/[controller]")]
     // Tenemos un atributo llamado API control, el que nos identifica que este es un controlador,
     // pero es un controlador de tipo API.

     // hereda de una clase llamada controllerBase, esto hace que sea  una clase de tipo controlador.
    public class VillaController : ControllerBase
    {
        // usamos un _ a las variables privadas
        // y pasamos como parametro a Ilogger en la clase que estamos
       private readonly ILogger<VillaController> _logger;
       // quitamos el ApplicationDbContext
       // private readonly ApplicationDbContext _db;
       private readonly IVillaRepositorio _villaRepo;
       // inyectamos el servicio de Automapper y usamo la interfaz IMapper control. using AutoMapper
       private readonly IMapper _imapper;
       // Esta variable no hay que inyectarlo pero si debemos hacer es inicializarlo en nuestro controlador.
       protected APiResponse _response;
        // escribimos ctor
        // inyectamos el ApplicationDbContext en contructor y el IMapper
       public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepo,IMapper imapper )
       {
         _logger = logger;
         _villaRepo=villaRepo;
         _imapper= imapper;
         _response = new();
       }
       // una lista de tipo villa, villa es un modelo
       // colocamos el verbo http colocamos un atributo por medio de corchetes
       [HttpGet]
       // aquí estamos indicando que nuestro tipo de dato de retorno será una lista IEnumerable
       // de tipo VillaDto
       // Pero si todo resulta bien, en este caso, nosotros podemos retornar un objeto OK
       // que produce un código de estado vacío que si podemos observar este OK es de tipo 200
       [ProducesResponseType(StatusCodes.Status200OK)]

       // Ahora  ya no sera de tipo IEnnumerable de nuestra lista, sino que sea de tipo APiResponse.
       // Es decir, ahora cada uno de nuestros Endpoints va a retornar un APiResponse
       // IEnumerable<VillaDto>
        public async Task <ActionResult <APiResponse>> GetVillas()
        { 
            // shift-alt-Flecha Arriba copiar
            // Alt-Flecha mover codigo
            // ctr-alt- Flecha abajo multicurso
        try
            {
                _logger.LogInformation("Obteniendo todas las villas");
                 // creamos una lista de IEnumerable de tipo Villa, que se llamara villaList
                IEnumerable<Villa> villaList = await _villaRepo.ObtenerTodos(); 
                _response.Resultado = _imapper.Map<IEnumerable<VillaDto>>(villaList);
                _response.statusCode = HttpStatusCode.OK; // using System.Net;
                // enviamos toda la respuesta
                return Ok(_response); 
                
            }
        catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMenssages = new List<string>() {ex.ToString()};
            }
            // si llega a pasar un error se retorna el error.
           return _response;
        }
        
        [HttpGet("id:int", Name ="GetVilla")]
         // para documentar usaremos el atributo  [ProducesResponseType]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  async Task<ActionResult<APiResponse>> GetVilla(int id)
        {
            try
            {
                 // verificamos que si el id = 0
                if (id ==0){
                 _logger.LogError("Error al traer villa con Id " + id);
                 _response.statusCode = HttpStatusCode.BadRequest;
                 _response.IsExitoso = false;
                 return BadRequest(_response);
                }
                // var villa = VillaStore.villaList!.FirstOrDefault(v=>v.Id == id)!;
                  // traenos un registro en base el id que le pasamos.
                  var villa = await _villaRepo.Obtener(v=>v.Id == id); //_db.Villas.FirstOrDefaultAsync(v=>v.Id == id)!;
                // verificamos que si no hay ninguno
                 if (villa == null){
                     _response.statusCode = HttpStatusCode.NotFound;
                     _response.IsExitoso = false;
                     return NotFound(_response);
                 }
                 // Lo que queremos retorna es VillaDto y de donde obtenemos los datos, en este caso de villa
                 _response.Resultado = _imapper.Map<VillaDto>(villa);
                 _response.statusCode = HttpStatusCode.OK;
                 return Ok(_response);
            }
            catch (System.Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMenssages = new List<string>() {ex.ToString()};
            }
            return _response;
        }

        [HttpPost]
        // Ahora hay  que cambiarle para ya no sea un 200 si no un 201 que es cuando creamos un registro
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // El atributo FromBody nos indica que nosotros vamos a recibir datos;  ademas le vamos a indicar
        // el tipo de dato que vamos a recibir, en este caso sera del  tipo del modelo VillaDto
        // cambiamos el VillaDto por VillaCreateDto
        public async Task<ActionResult<APiResponse>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
             try
             {
             if (!ModelState.IsValid){
                // Model State me va evitar de que se siga con las siguientes líneas de código.
                  return BadRequest(ModelState);
             }
             
             if (await _villaRepo.Obtener(v=>v.Nombre!.ToLower() == createDto.Nombre!.ToLower()) !=null)
             {
                ModelState.AddModelError("NombreExiste","La Villa con ese nombre ya Existe!");
                return BadRequest(ModelState);
             }
             if (createDto== null) // No, nos estan enviando datos, por lo tanto no podemos guardar la informacion
             {
                return BadRequest(createDto);
             }
             // este codigo equivale al villa modelo = new () {......}
                 Villa modelo = _imapper.Map<Villa>(createDto);

                       
             await _villaRepo.Crear(modelo); // El SaveChangesAsync ya esta incluidor en crear()
             _response.Resultado = modelo;
             _response.statusCode = HttpStatusCode.Created;

          return CreatedAtRoute("GetVilla", new {id=modelo.Id}, _response);
                
             }
             catch (System.Exception ex)
             {
                _response.IsExitoso = false;
                _response.ErrorMenssages = new List<string>() {ex.ToString()};
             }
             
             return _response;
        }

           //  Esta vez no vamos a utilizar el ActionResult, vamos a utilizar la interfaz IActionResult 
           // porque la interfaz, no va a necesitar del modelo. Aquí no vamos a necesitar el modelo como tal, 
           // porque siempre cuando estamos trabajando con Delete o eliminacion se debe retornar un NoContent()
           
           [HttpDelete("id:int")]
           [ProducesResponseType(StatusCodes.Status204NoContent)]
           [ProducesResponseType(StatusCodes.Status400BadRequest)]
           [ProducesResponseType(StatusCodes.Status404NotFound)]
           public async Task<IActionResult> DeleteVilla(int id)
           {
                try
                {
                   if (id==0)
                   {
                       _response.IsExitoso = false;
                       _response.statusCode = HttpStatusCode.BadRequest;
                       return BadRequest(_response);
                   }
            
                   var villa = await _villaRepo.Obtener(v=>v.Id == id);
                  if (villa==null)
                  {
                      _response.IsExitoso = false;
                      _response.statusCode = HttpStatusCode.NotFound;
                      return NotFound(_response);
                  }
                       
                   await _villaRepo.Remover(villa); // el SaveChangeASync ya viene incorporado en remover.
                   _response.statusCode = HttpStatusCode.NoContent;
                   // En NoContent no le podemos enviar el statusCode ya que no recibe ningun parametro
                   // para eso vamos a usar Ok
                   return Ok(_response);
                    
                }
                catch (System.Exception ex)
                {
                       _response.IsExitoso = false;
                       _response.ErrorMenssages = new List<string>() {ex.ToString()};
                }
                return BadRequest(_response);
           }
        
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla (int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto==null || id!=updateDto.Id)
            {
                 _response.IsExitoso = false;
                 _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
         Villa modelo = _imapper.Map<Villa>(updateDto);
           
            await _villaRepo.Actualizar(modelo);
             // como no vamos a retornar el modelo usamos No contenido
             _response.statusCode = HttpStatusCode.NoContent;
             return Ok(_response);
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla (int id,[FromBody] JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto==null || id==0)
            {
                return BadRequest();
            }
            
            var villa = await _villaRepo.Obtener(v=>v.Id == id, tracked:false)!; // false para que sea AsNoTracking
              
              VillaUpdateDto villaDto = _imapper.Map<VillaUpdateDto>(villa);
              
              if (villa == null) return BadRequest();
          
              patchDto.ApplyTo(villaDto, ModelState);

             if (!ModelState.IsValid){
             // retornamos un BadRequest y le pasamos el ModelState para que nos indique donde ocurrio el error.
                return BadRequest(ModelState);
             }
             // ahora hacemos hacer un modelo de tipo Villa y va a tener las mismas Propiedades que VillaDto
               
               Villa modelo = _imapper.Map<Villa>(villaDto);
            
            //   _db.Villas.Update(modelo); // no existe un metodo asincrono de
            //   await _db.SaveChangesAsync();
           await  _villaRepo.Actualizar(modelo);
           // como no vamos a retornar el modelo usamos No contenido
           _response.statusCode = HttpStatusCode.NoContent;
             return Ok(_response);
                        
        }
    }
}