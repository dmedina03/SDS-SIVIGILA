using SIVIGILA.Commons.DTOs.Perfil;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(context context) : base(context)
        {

        }

        public async Task<DataCollection<PerfilDTO>> GetByParamsAsync(SearchPerfilDTO parameters)
        {
            var data = Entity.Select(x => new PerfilDTO
            {
                PerfilID = x.Id,
                NombrePerfil = x.NombrePerfil,
                Estado = x.Estado,
            });
            if (!String.IsNullOrEmpty(parameters.NombrePerfil))
            {
                data = data.Where(x => x.NombrePerfil.ToLower()
                            .Trim().Contains(parameters.NombrePerfil.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
