using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DpOrientSexualService
{
    public interface IDpOrientSexualService : ICreateService<DpOrientSexualDTO>, IUpdateService<DpOrientSexualDTO>,
                                      IGetService<DpOrientSexualGetDTO, DpOrientSexualDTO>,
                                      IServiceSearch<DpOrientSexualDTO, SearchDpOrientSexualDTO>
    {

    }
}
