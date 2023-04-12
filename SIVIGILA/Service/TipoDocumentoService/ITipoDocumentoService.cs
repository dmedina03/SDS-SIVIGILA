using SIVIGILA.Commons.DTOs.TipoDocumento;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.TipoDocumentoService
{
    public interface ITipoDocumentoService : ICreateOrUpdateRangeService<TipoDocumentoDTO>, ICreateService<TipoDocumentoDTO>,
                                    IUpdateService<TipoDocumentoDTO>, IGetService<TipoDocumentoDTO, TipoDocumentoDTO>,
                                    IUpdateStateService, IExistService,
                                    IServiceSearch<TipoDocumentoDTO, SearchBaseTipoDocumentoDTO>
    {
        public Task<bool> ExistByNameAsync(string NombreDocumento);
    }
}
