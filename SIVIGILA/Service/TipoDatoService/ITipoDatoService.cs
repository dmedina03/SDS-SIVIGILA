using SIVIGILA.Commons.DTOs.TipoDatoDto;
using SIVIGILA.Commons.TipoDatoDTO;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.TipoDatoService
{
    public interface ITipoDatoService : ICreateService<TipoDatoDTO>, IUpdateService<TipoDatoDTO>,
                                        IGetService<TipoDatoGetDTO,TipoDatoGetDTO>
    {

    }
}
