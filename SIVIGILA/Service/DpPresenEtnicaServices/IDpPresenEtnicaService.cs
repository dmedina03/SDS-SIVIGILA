using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DpPresenEtnicaServices
{
    public interface IDpPresenEtnicaService : ICreateService<DpPresenEtnicaDTO>,
                                              IUpdateService<DpPresenEtnicaDTO>,
                                              IGetService<DpPresenEtnicaGetDTO,DpPresenEtnicaGetDTO>,
                                              IServiceSearch<DpPresenEtnicaDTO,SearchDpPrensenEtnicaDTO>
    {
    }
}
