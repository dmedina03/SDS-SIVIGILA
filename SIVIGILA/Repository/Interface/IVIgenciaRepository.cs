using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository.Interface
{
    public interface IVIgenciaRepository: IBaseGenericRepository<Vigencia>,
                                          ISearchRepository<VigenciaGetDTO, SearchVigenciaDTO>
    {
    }
}
