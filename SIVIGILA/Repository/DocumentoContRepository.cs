using SIVIGILA.Commons.DTOs.DocumentoContratacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository
{
    public class DocumentoContRepository: BaseRepository<DocumentacionContratacion>, IDocumentosContRepository
    {
        public DocumentoContRepository(context context):base(context)
        {
            
        }

        public async Task<DataCollection<DocumentoCDTO>> GetByParamsAsync(SearchDocumentoDTO parameters)
        {
            var data = Entity.Select(x => new DocumentoCDTO
            {
                DocumentoConID = x.Id,
                NombreDocumento = x.NombreDocumento,
                Estado = x.Estado,
            });
            if (!String.IsNullOrEmpty(parameters.NombreDocumento))
            {
                data = data.Where(x => x.NombreDocumento.ToLower()
                            .Trim().Contains(parameters.NombreDocumento.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
