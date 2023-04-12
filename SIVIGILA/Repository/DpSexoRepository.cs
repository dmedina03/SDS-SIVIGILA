using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class DpSexoRepository : BaseRepository<DpSexo>,IDpSexoRepository
    {
        public DpSexoRepository(context context) : base (context)
        {

        }

        public async Task<DataCollection<DpSexoDTO>> GetByParamsAsync(SearchDpSexoDTO parameters)
        {
            var data = Entity.Select(x => new DpSexoDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                Ivc = x.Ivc,
                TalentoHumano = x.TalentoHumano,
                Vsa = x.Vsa
            });
            if (!String.IsNullOrEmpty(parameters.Descripcion))
            {
                data = data.Where(x => x.Descripcion.ToLower()
                            .Trim().Contains(parameters.Descripcion.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
