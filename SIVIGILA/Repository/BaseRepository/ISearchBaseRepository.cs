using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;

namespace SIVIGILA.Repository.BaseRepository
{
    public interface ISearchRepository<T, U> where U : SearchBaseDTO
    {
        public Task<DataCollection<T>> GetByParamsAsync(U parameters);
    }
}
