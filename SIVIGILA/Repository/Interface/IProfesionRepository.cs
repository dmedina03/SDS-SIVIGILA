using SIVIGILA.Commons.DTOs.Profesion;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IProfesionRepository : IBaseGenericRepository<Profesion>, ISearchRepository<ProfesionDTO, SearchProfesionDTO>
    {
    }
}
