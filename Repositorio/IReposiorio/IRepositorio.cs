using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace villaMagica.Repositorio.IReposiorio
{
    // Esta va a ser nuestra interfaz de repositorio genérico. Que signfica generico
    // signfica Que si nosotros vamos agregando modelos, pues todos los modelos
    // que vayamos agregando pueden utilizar este repositorio.  Como hacemos una interfaz de
    //  repositorio que sea genérico?. Agremos como tipo T y la palabra where T: class
    public interface IRepositorio<T> where T : class
    {
        // los metodos que se declara solo son contratos
        // Task son tareas - que representa operaciones asincronas
        Task Crear(T entidad);
        // va a retornar una lista segun la entidad que le enviemos
        // tendra un filtro, este filtro es una expresion Linq, Func<Parametro de Entrada,Parametro Salida>
        // que es una funcion, al colocar el signo ? es para indicarle que no es obligaorio
        // Es decir si no se envia un filtro solo retornara la lista
        Task<List<T>> ObtenerTodos(Expression<Func<T,bool>> ? filtro=null);
        // Este metodo solamente se encargará de retornar un solo registro segun la entidad que le enviemos
        Task<T> Obtener(Expression<Func<T,bool>> filtro=null!, bool tracked=true);
        // este metodo pasamos la entidad que va a eliminar
        Task Remover(T entidad);

        // saveChange()
        Task Grabar();
    }
}