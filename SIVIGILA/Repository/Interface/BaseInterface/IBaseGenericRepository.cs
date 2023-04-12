using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SIVIGILA.Models.Context;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SIVIGILA.Repository.Interface.BaseInterface
{
    public interface IBaseGenericRepository<T> where T : class
    {
        context _context { get; set; }
        DbSet<T> Entity => _context.Set<T>();
        /// <summary>
        /// Método generico para obtener la primera entidad o atributo de <typeparamref name="T"/> 
        /// que cumpla con la condición
        /// </summary>
        /// <typeparam name="U">Entidad o propiedad de <typeparamref name="T"/> que se desea obtener</typeparam>
        /// <param name="parameter">Expresión lambda que determina el objeto respuesta</param>
        /// <param name="searchQuery">Expresión lambda con la condición a buscar en la BD</param>
        /// <param name="include">Expresión lambda que determina las propiedades de navegación que se deben incluir en la consulta</param>
        /// <returns><typeparamref name="U"/></returns>
        public Task<U> GenericGetAsync<U>(Expression<Func<T, U>> parameter,
                                            Expression<Func<T, bool>> searchQuery = null,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Método para obtener una lista de entidades <typeparamref name="T"/> que cumplemncon la condición suministrada
        /// </summary>
        /// <remarks>Si no se incluye una condición, se traen todas las entidades</remarks>
        /// <param name="predicate">Expresión lambda con la condición a buscar en la BD< </param>
        /// <param name="include">Expresión lambda que determina las propiedades de navegación que se deben incluir en la consulta</param>
        /// <param name="orderBy">Expresión lambda que deterina la forma de ordenamiento de la consulta</param>
        /// <param name="selector">Expresión lambda que determina las propiedades a recuperar de la base de datos</param>
        /// <param name="distinct">Boolean para determinar si se debe aplicat un DISTINC a la consulta
        /// <remarks>Por defecto no se incluye</remarks></param>
        /// <returns>Lista de entidades <typeparamref name="T"/></returns>
        public Task<List<T>> GenericGetAllAsync(Expression<Func<T, bool>> predicate = null,
                                     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     Expression<Func<T,T>> selector = null,
                                     bool distinct = false);
        /// <summary>
        /// Método para obtener una lista de Elementos <typeparamref name="U"/> de la entidad <typeparamref name="T"/> que cumplemncon la condición suministrada
        /// </summary>
        /// <remarks>Si no se incluye una condición, se traen todas las entidades</remarks>
        /// <remarks>SI SE DESEA TRAER TODAS LAS ENTIDADES SE DEBE USAR <see cref="GenericGetAllAsync(Expression{Func{T, bool}}, Func{IQueryable{T}, IIncludableQueryable{T, object}}, Func{IQueryable{T}, IOrderedQueryable{T}}, Expression{Func{T, T}}, bool)"
        ///  /> YA QUE ESTE METODO NO TIENE DESACTIVADO EK TRACKING DE ENTIDADES Y PUEDE TENER CONSECUENCIAS DE RENDIMIENTO</remarks>
        /// <param name="predicate">Expresión lambda con la condición a buscar en la BD< </param>
        /// <param name="include">Expresión lambda que determina las propiedades de navegación que se deben incluir en la consulta</param>
        /// <param name="orderBy">Expresión lambda que deterina la forma de ordenamiento de la consulta</param>
        /// <param name="selector">Expresión lambda que determina las propiedades a recuperar de la base de datos</param>
        /// <param name="distinct">Boolean para determinar si se debe aplicat un DISTINC a la consulta
        /// <remarks>Por defecto no se incluye</remarks></param>
        /// <returns>Lista de Elementos <typeparamref name="U"/></returns>
        public Task<IEnumerable<U>> GenericGetAllValuesAsync<U>(Expression<Func<T, U>> selector, 
                                                                 Expression<Func<T, bool>> predicate = null,
                                                                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                 bool distinct = false);

        /// <summary>
        /// Método para determinar la existencia de una entidad <typeparamref name="T"/> que cumpla con la condición
        /// suministrada
        /// </summary>
        /// <remarks>Si no se incluye una condición, revisa la existencia de prolomenos un registro en la B.D</remarks>
        /// <param name="predicate">Expresión lambda con la condición a buscar en la BD</param>
        /// <param name="include">Expresión lambda que determina las propiedades de navegación que se deben incluir en la consulta</param>
        /// <returns>True si existe alún registro que cumple la condición, False si no</returns>
        public Task<bool> ExistGenericAsync(Expression<Func<T, bool>> predicate = null,
                                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Método para actualizar un registro de la entidad <typeparamref name="T"/> en la Base de datoss
        /// </summary>
        /// <param name="value">Entidad a actualizar</param>
        /// <param name="saveChanges">Boolean para determinar si se salvan cambios  
        /// <remarks>Por defecto es True</remarks></param>
        /// <param name="propertyExpresion">Arreglo de expresiones Lambda que determina las propiedades a actualizar</param>
        /// <remarks>Si no se incluye ninguna <paramref name="propertyExpresion"/> se actualizan todas las propiedades
        ///  de la entidad</remarks>
        /// <returns>True</returns>
        public Task<bool> UpdateGenericAsync(T value, bool saveChanges = true,
                                            params Expression<Func<T, object>>[] propertyExpresion);
        /// <summary>
        /// Método para eliminar un conjunto de registros de la entidad <typeparamref name="T"/> que cumplan
        /// con la condición suministrada
        /// </summary>
        /// <remarks>Si no se incluye ninguna condición se eliminan todos los registros</remarks>
        /// <param name="predicate">>Expresión lambda con la condición a buscar en la BD</param>
        /// <param name="SaveChanges">Boolean para determinar si se salvan cambios</param>
        /// <param name="include">Expresión lambda que determina las propiedades de navegación que se deben incluir en la consulta</param>
        /// <returns>True</returns>
        public Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate = null, bool SaveChanges = true,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        /// <summary>
        /// Método para agregar una entidad de <typeparamref name="T"/> a la base de datos
        /// </summary>
        /// <param name="entity">Entidad a agregar</param>
        /// <param name="SaveChanges">Boolean para determinar si se salvan cambios 
        /// <remarks>Por defecto es True</remarks></param>
        /// <returns>True</returns>
        public Task<bool> AddAsync(T entity, bool SaveChanges = true);
        /// <summary>
        /// Método para agregar una lista de entidades de <typeparamref name="T"/> a la base de datos
        /// </summary>
        /// <param name="lista">Lista de entidades a agregar</param>
        /// <param name="SaveChanges">Boolean para determinar si se salvan cambios 
        /// <remarks>Por defecto es True</remarks></param>
        /// <returns>True</returns>
        public Task<bool> AddRangeAsync(IEnumerable<T> lista, bool SaveChanges = true);
        /// <summary>
        /// Método para actualizar un conjunto de registros de la entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="lista">Lista de entidades a actualizar</param>
        /// <param name="SaveChanges">Boolean para determinar si se salvan cambios 
        /// <remarks>Por defecto es True</remarks></param>
        /// <returns>True</returns>
        public Task<bool> UpdateRangeAsync(IEnumerable<T> lista, bool SaveChanges = true);
        /// <summary>
        /// Método para llamar a la función <see cref="DbContext.SaveChangesAsync(bool, CancellationToken)"/>
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync();
        /// <summary>
        /// Método para Obtener valores de Operaciones como Suma, Resta,Producto,Division,Maximo,Minimo,Promedio, entre otros
        /// según la condicion suministrada
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="parameter">Expresion lambda que representa la condición de selección de los datos</param>
        /// <param name="Operation">Función lambda que incluye la operación a realizar en la BD</param>
        /// <param name="include">Funcion lambda que determina las propiedades de navegación que se uan con un INNER JOIN</param>
        /// <returns></returns>
        public Task<U> ApplyOperationAsync<U>(Func<IQueryable<T>, Task<U>> Operation, Expression<Func<T, bool>> parameter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}
