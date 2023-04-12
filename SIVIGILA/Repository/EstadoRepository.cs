using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository
{
    public class EstadoRepository: BaseRepository<Estado>,IEstadoRepository
    {
        public EstadoRepository(context context): base(context)
        {
            
        }

    }
}
