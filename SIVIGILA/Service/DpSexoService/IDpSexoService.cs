using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DpSexoService
{
    public interface IDpSexoService : ICreateService<DpSexoDTO>,IUpdateService<DpSexoDTO>,
                                      IGetService<DpSexoGetDTO,DpSexoDTO>,
                                      IServiceSearch<DpSexoDTO,SearchDpSexoDTO>
    {

    }
}
