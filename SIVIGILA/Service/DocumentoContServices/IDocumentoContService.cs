using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DocumentoContServices
{
    public interface IDocumentoContService: ICreateOrUpdateRangeService<DocumentoCDTO>, ICreateService<DocumentoCDTO>,
                                            IUpdateService<DocumentoCDTO>, IGetService<DocumentoCDTO, DocumentoCDTO>,
                                            IUpdateStateGenericService<DocumentacionCPatchDTO>, IExistService,
                                            IServiceSearch<DocumentoCDTO, SearchDocumentoDTO>,
                                            IGetAllNamesService
    {
        public Task<bool> ExistByNameAsync(string nombreDocumento);
    }
}
