using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DpCondiDiscapaService
{
    public interface IDpCondiDiscapaService : ICreateService<DpCondiDiscapaDTO>,
                                              IUpdateService<DpCondiDiscapaDTO>,
                                              IGetService<DpCondiDiscapaDTO,DpCondiDiscapaDTO>,
                                              IServiceSearch<DpCondiDiscapaDTO,SearchDpCondiDiscapaDTO>
    {
    }
}
