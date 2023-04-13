using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.TablaCostosService
{
    public interface ITablaCostosService : ICreateService<TablaCostosDTO>,
                                           ICreateRangeService<TablaCostosDTO>
    {
    }
}
