using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.VigenciaService
{
    public interface IVigenciaService: ICreateOrUpdateRangeService<VigenciaDTO>,
                                       IServiceSearch<VigenciaGetDTO,SearchVigenciaDTO>,
                                       IGetService<VigenciaGetDTO, VigenciaSimpleGetDTO>

    {
        /// <summary>
        /// Método para obtener el ID y el nombre de todas las vigencias que se encuentra disponibles
        /// </summary>
        /// <returns>Colección de elementos del tipo <see cref="VigenciaShortInfoDTO"/></returns>
        public Task<IEnumerable<VigenciaShortInfoDTO>> GetShortInfoActiveDTO();
        /// <summary>
        /// Método para obtener los estados de las vigencias
        /// </summary>
        /// <returns>Lista de estados Vigencia</returns>
        public  Task<IEnumerable<EstadoVigenciaDTO>> GetStatesVigenciaAsync();
        /// <summary>
        /// Método para guardar las vigencias
        /// </summary>
        public Task<int> AddAsync(VigenciaDTO vigencia);
        /// <summary>
        /// Método para actualizar las vigencias
        /// </summary>
        public Task<bool> UpdateAsync(VigenciaDTO vigencia);
    }
}
