using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.TipoUbicacionService
{
    public interface ITipoUbicacionService : ICreateOrUpdateRangeService<TipoUbicacionDto>,
                                             ICreateService<TipoUbicacionDto>,
                                             IUpdateService<TipoUbicacionDto>,
                                             IGetService<TipoUbicacionGetDTO, TipoUbicacionGetDTO>
    {
        public Task<DataCollection<TipoUbicacionDto>> GetByParamsAsync(SearchTipoUbicacionDTO Dto);
    }
}
