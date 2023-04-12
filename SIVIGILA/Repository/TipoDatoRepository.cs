using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class TipoDatoRepository : BaseRepository<TipoDato> ,ITipoDatoRepository
    {
        public TipoDatoRepository(context context) : base(context)
        {

        }
    }
}
