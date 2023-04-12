using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.NovedadesService
{
    public interface INovedadService: ICreateOrUpdateRangeService<NovedadesDTO>, ICreateService<NovedadesDTO>,
                                    IUpdateService<NovedadesDTO>, IGetService<NovedadesDTO, NovedadesDTO>,
                                    IUpdateStateGenericService<NovedadesPatchDTO>, IExistService,
                                    IServiceSearch<NovedadesDTO, SearchNovedadesDTO>,
                                    IGetAllNamesService
    {
        public Task<bool> ExistByNameAsync(string nombreNovedad);
    }
}
