using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.LineaService
{
    public interface ILineaService: ICreateOrUpdateRangeService<LineaDTO>,ICreateService<LineaDTO>,
                                    IUpdateService<LineaDTO>, IGetService<LineaDTO,LineaDTO>,
                                    IExistService, IUpdateStateGenericService<LineaPatchDTO>,
                                    IServiceSearch<LineaDTO,SearchLineaDTO>,
                                    IGetAllNamesService
    {
        public Task<bool> ExistByNameAsync(string nombreLinea);
    }
}
