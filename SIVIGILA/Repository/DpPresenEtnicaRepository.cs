using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using System.Runtime.InteropServices;

namespace SIVIGILA.Repository
{
    public class DpPresenEtnicaRepository : BaseRepository<DpPresenEtnica>, IDpPresenEtnicaRepository
    {
        public DpPresenEtnicaRepository(context context): base(context)
        {

        }

        public async Task<DataCollection<DpPresenEtnicaDTO>> GetByParamsAsync(SearchDpPrensenEtnicaDTO parameters)
        {
            var data = Entity.Select(x => new DpPresenEtnicaDTO
            {
                ID = x.Id,
                Descripcion = x.Descripcion,
                TalentoHumano = x.TalentoHumano,
                Ivc = x.Ivc,
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
