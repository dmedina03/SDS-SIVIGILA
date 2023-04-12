using SIVIGILA.Commons.DTOs.PostgradoDto;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class PostgradoRepository : BaseRepository<Postgrado>, IPostgradoRepository
    {
        public PostgradoRepository(context context) : base(context)
        {

        }

        public async Task<DataCollection<PostgradoDTO>> GetByParamsAsync(SearchPostgradoDTO parameters)
        {
            var data = Entity.Select(x => new PostgradoDTO
            {
                PostgradoID = x.Id,
                NombrePostgrado = x.NombrePostgrado,
                Estado = x.Estado,
            });
            if (!String.IsNullOrEmpty(parameters.NombrePostgrado))
            {
                data = data.Where(x => x.NombrePostgrado.ToLower()
                            .Trim().Contains(parameters.NombrePostgrado.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
