using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.MetaService
{
    public interface IMetaService:  ICreateOrUpdateRangeService<MetaDTO>, ICreateService<MetaDTO>,
                                    IUpdateService<MetaDTO>, IGetService<MetaGetDTO, MetaGetDTO>,
                                    IExistService,
                                    IServiceSearch<MetaGetDTO, SearchMetaDTO>
    {
        /// <summary>
        /// Método para copiar (Obtener) la info de la última Vigencia que se inertó
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<MetaGetDTO>> CopyInfoInLastVigenciaAsync();
    }
}
