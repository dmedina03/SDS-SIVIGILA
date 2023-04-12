using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class LineaRepository: BaseRepository<Linea>, ILineaRepository
    {
        public LineaRepository(context context):base(context)
        {
            
        }

        public async Task<DataCollection<LineaDTO>> GetByParamsAsync(SearchLineaDTO parameters)
        {
            var data =  Entity.Select(x => new LineaDTO
            {
                LineaID = x.Id,
                NombreLinea = x.NombreLinea,
                Estado = x.Estado,
            });
            if(!String.IsNullOrEmpty(parameters.NombreLinea))
            {
                data = data.Where(x => x.NombreLinea.ToLower()
                            .Trim().Contains(parameters.NombreLinea.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
