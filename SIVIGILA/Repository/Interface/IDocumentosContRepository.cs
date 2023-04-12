using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IDocumentosContRepository: IBaseGenericRepository<DocumentacionContratacion>,
                                             ISearchRepository<DocumentoCDTO,SearchDocumentoDTO>
    {
    }
}
