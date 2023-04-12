using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.TerminalesPortuario;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface ITerminalesPortuarioRepository : IBaseGenericRepository<TerminalesPortuario>, ISearchRepository<TerminalesPortuarioDTO, SearchTerminalesPortuarioDTO>
    {
    }
}
