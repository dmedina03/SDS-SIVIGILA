using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.PerfilVigenciaService
{
    public interface IPerfilVigenciaService : ICreateService<PerfilVigenciaDto>,
                                              IUpdateService<PerfilVigenciaDto>,
                                              ICreateOrUpdateRangeService<PerfilVigenciaDto>,
                                              IServiceSearch<PerfilVigenciaGetDTO,SearchPerfilVigenciaDTO>
    {
        /// <summary>
        /// Método que funciona para traer los perfiles que se han parametrizado de acuerdo a una vigencia
        /// </summary>
        /// <param name="Id">Id de la Vigencia</param>
        /// <returns>Lista de Perfiles con sus respectivos PerfilVigencia_ID</returns>
        public Task<IEnumerable<PerfilVigenciaTablaCostoDTO>> GetPerfilesByIdVigencia(int Id);

    }
}
