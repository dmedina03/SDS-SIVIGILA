using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;

namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IServiceSearch<T, U> where U : SearchBaseDTO
    {
        /// <summary>
        /// Método para obtener una lista de entidades paginadas de acuerdo al DTO con los parametros de busqueda
        /// </summary>
        /// <param name="Dto">DTO con los parametros de busqueda</param>
        /// <returns>DTO con la lista paginada y los atributos de la paginación</returns>
        public Task<DataCollection<T>> GetByParamsAsync(U Dto);
    }
}
