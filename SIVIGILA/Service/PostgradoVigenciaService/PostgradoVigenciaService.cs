using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.PostgradoVigenciaService
{
    public class PostgradoVigenciaService : IPostgradoVigenciaService
    {
        private readonly IPostgradoVigenciaRepository _postgradoVigenciaRepository;
        public PostgradoVigenciaService(IPostgradoVigenciaRepository postgradoVigenciaRepository)
        {
            _postgradoVigenciaRepository = postgradoVigenciaRepository;
        }

        public async Task<int> AddAsync(PerfilPostgradoVigenciaDto Dto)
        {
            PostgradoVigencia entidad = new()
            {
                PostgradoVigenciaID = Dto.PostgradoVigenciaID,
                ProfesionVigenciaID = Dto.ProfesionVigenciaID,
                PostgradoID = Dto.PostgradoID
            };
            var data = await _postgradoVigenciaRepository.AddAsync(entidad);
            return entidad.PostgradoVigenciaID;
        }

        public async Task<bool> UpdateAsync(PerfilPostgradoVigenciaDto Dto)
        {
            PostgradoVigencia entidad = new()
            {
                PostgradoVigenciaID = Dto.PostgradoVigenciaID,
                ProfesionVigenciaID = Dto.ProfesionVigenciaID,
                PostgradoID = Dto.PostgradoID
            };
            var data = await _postgradoVigenciaRepository.UpdateGenericAsync(entidad, true,
                x => x.PostgradoID);
            return data;
        }
    }
}
