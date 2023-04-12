using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.PerfilVigenciaService
{
    public interface IPerfilVigenciaService : ICreateService<PerfilVigenciaDto>,
                                              IUpdateService<PerfilVigenciaDto>,
                                              ICreateOrUpdateRangeService<PerfilVigenciaDto>,
                                              IServiceSearch<PerfilVigenciaGetDTO,SearchPerfilVigenciaDTO>
    {
    }
}
