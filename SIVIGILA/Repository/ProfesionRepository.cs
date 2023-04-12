using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class ProfesionRepository : BaseRepository<Profesion>, IProfesionRepository
    {
        public ProfesionRepository(context context) : base(context)
        {

        }

        public async Task<DataCollection<ProfesionDTO>> GetByParamsAsync(SearchProfesionDTO parameters)
        {
            var data = Entity.Select(x => new ProfesionDTO
            {
                ProfesionID = x.Id,
                NombreProfesion = x.NombreProfesion,
                Estado = x.Estado,
            });
            if (!String.IsNullOrEmpty(parameters.NombreProfesion))
            {
                data = data.Where(x => x.NombreProfesion.ToLower()
                            .Trim().Contains(parameters.NombreProfesion.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
