using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.ProfesionService
{
    public interface IProfesionService : ICreateOrUpdateRangeService<ProfesionDTO>, ICreateService<ProfesionDTO>,
                                    IUpdateService<ProfesionDTO>, IGetService<ProfesionDTO, ProfesionDTO>,
                                    IUpdateStateService, IExistService,
                                    IServiceSearch<ProfesionDTO, SearchProfesionDTO>
    {
        public Task<bool> ExistByNameAsync(string nombreProfesion);
    }
}
