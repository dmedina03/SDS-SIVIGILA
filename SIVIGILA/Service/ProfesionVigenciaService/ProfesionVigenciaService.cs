using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.ProfesionVigenciaService
{
    public class ProfesionVigenciaService : IProfesionVigenciaService
    {
        private readonly IProfesionVigenciaRepository _profesionVigenciaRepository;
        public ProfesionVigenciaService(IProfesionVigenciaRepository profesionVigenciaRepository)
        {
            _profesionVigenciaRepository = profesionVigenciaRepository;
        }

        public async Task<int> AddAsync(PerfilProfesionVigenciaDto dto)
        {
            ProfesionVigencia entidad = new ProfesionVigencia()
            {
                ProfesionVigenciaID = dto.ProfesionVigenciaID,
                PerfilVigenciaID = dto.PerfilVigenciaID,
                ProfesionID = dto.ProfesionID
            };
            var data = await _profesionVigenciaRepository.AddAsync(entidad);
            return entidad.ProfesionVigenciaID;
        }
        public async Task<bool> UpdateAsync(PerfilProfesionVigenciaDto dto)
        {
            ProfesionVigencia entidad = new ProfesionVigencia()
            {
                ProfesionVigenciaID = dto.ProfesionVigenciaID,
                PerfilVigenciaID = dto.PerfilVigenciaID,
                ProfesionID = dto.ProfesionID
            };
            var data = await _profesionVigenciaRepository.UpdateGenericAsync(entidad, true,
                x => x.PerfilVigenciaID, x => x.ProfesionID);
            return data;
        }

    }
}
