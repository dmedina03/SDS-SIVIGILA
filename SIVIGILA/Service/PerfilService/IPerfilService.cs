using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.PerfilService
{
    public interface IPerfilService : ICreateOrUpdateRangeService<PerfilDTO>, ICreateService<PerfilDTO>,
                                    IUpdateService<PerfilDTO>, IGetService<PerfilDTO, PerfilDTO>,
                                    IUpdateStateService, IExistService,
                                    IServiceSearch<PerfilDTO, SearchPerfilDTO>
    {
        public Task<bool> ExistByNameAsync(string nombrePerfil);
    }
}
