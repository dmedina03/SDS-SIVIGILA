using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class ProfesionVigenciaRepository : BaseRepository<ProfesionVigencia>, IProfesionVigenciaRepository
    {
        public ProfesionVigenciaRepository(context context) : base(context)
        {

        }
    }
}
