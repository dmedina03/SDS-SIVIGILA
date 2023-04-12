using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.PostgradoVigenciaService
{
    public interface IPostgradoVigenciaService : ICreateService<PerfilPostgradoVigenciaDto>,
                                                 IUpdateService<PerfilPostgradoVigenciaDto>
    {
    }
}
