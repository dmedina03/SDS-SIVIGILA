using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.TerminalesPortuarioService
{
    public interface ITerminalesPortuarioService : ICreateOrUpdateRangeService<TerminalesPortuarioDTO>, ICreateService<TerminalesPortuarioDTO>,
                                    IUpdateService<TerminalesPortuarioDTO>, IGetService<TerminalesPortuarioDTO, TerminalesPortuarioDTO>,
                                    IUpdateStateService, IExistService,
                                    IServiceSearch<TerminalesPortuarioDTO, SearchTerminalesPortuarioDTO>
    {
        public Task<bool> ExistByNameAsync(string nombreTerminalesPortuario);
    }
}
