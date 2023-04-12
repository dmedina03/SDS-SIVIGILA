using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class ActividadRepository: BaseRepository<Actividad>, IActividadRepository
    {
        public ActividadRepository(context context): base(context)
        {
            
        }
    }
}
