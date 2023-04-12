using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class PerfilVigenciaRepository : BaseRepository<PerfilVigencia>, IPerfilVigenciaRepository
    {

        public PerfilVigenciaRepository(context context) : base(context)
        {

        }

        public async Task<DataCollection<PerfilVigenciaGetDTO>> GetByParamsAsync(SearchPerfilVigenciaDTO parameters)
        {
            var data = await Entity.Select(x => new PerfilVigenciaGetDTO
            {
                PerfilVigenciaID = x.PerfilVigenciaID,
                PerfilID = x.PerfilID,
                VigenciaID = x.VigenciaID,
                ProfesionVigencia = x.Profesiones.Select(c => new Commons.DTOs.PerfilVigenciaDTOs.PerfilProfesionVigenciaGetDto
                {
                    ProfesionVigenciaID = c.ProfesionVigenciaID,
                    PerfilVigenciaID = c.PerfilVigenciaID,
                    ProfesionID = c.ProfesionID,
                    PostgradoVigencia = c.Postgrados.Select(p => new Commons.DTOs.PerfilVigenciaDTOs.PerfilPostgradoVigenciaGetDto
                    {
                        PostgradoVigenciaID = p.PostgradoVigenciaID,
                        ProfesionVigenciaID = p.ProfesionVigenciaID,
                        PostgradoID = p.PostgradoID
                    })
                })
            }).GetPagedAsync(parameters.page, parameters.take);

            return data;
        }
    }
}
