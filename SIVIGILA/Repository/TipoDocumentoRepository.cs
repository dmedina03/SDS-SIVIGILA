using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TipoDocumento;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class TipoDocumentoRepository : BaseRepository<TipoDocumento>, ITipoDocumentoRepository
    {
        public TipoDocumentoRepository(context context) : base(context)
        {
        }

        public async Task<DataCollection<TipoDocumentoDTO>> GetByParamsAsync(SearchBaseTipoDocumentoDTO parameters)
        {
            var data = Entity.Select(x => new TipoDocumentoDTO
            {
                TipoDocumentoID = x.Id,
                NombreDocumento = x.NombreDocumento,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
                Sispic = x.Sispic
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
