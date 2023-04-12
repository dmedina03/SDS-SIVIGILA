using SIVIGILA.Commons.DTOs.NovedadesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class NovedadRepository: BaseRepository<Novedades>,INovedadRepository
    {
        public NovedadRepository(context context):base(context)
        {
            
        }

        public async Task<DataCollection<NovedadesDTO>> GetByParamsAsync(SearchNovedadesDTO parameters)
        {
            var query = Entity.Select(x => new NovedadesDTO
            {
                NovedadesID = x.Id,
                NombreNovedad = x.NombreNovedad,
                Estado = x.Estado
            }).AsQueryable();

            if(!String.IsNullOrEmpty(parameters.NombreNovedad))
            {
                query = query.
                    Where(x => x.NombreNovedad.ToLower().Trim().
                                Contains(parameters.NombreNovedad.ToLower().Trim()));
            }
            return await query.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
