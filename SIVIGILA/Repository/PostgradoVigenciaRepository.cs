using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class PostgradoVigenciaRepository : BaseRepository<PostgradoVigencia>, IPostgradoVigenciaRepository
    {
        public PostgradoVigenciaRepository(context context) : base(context)
        {

        }
    }
}
