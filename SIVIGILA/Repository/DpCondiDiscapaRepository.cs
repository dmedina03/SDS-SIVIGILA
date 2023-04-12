using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class DpCondiDiscapaRepository : BaseRepository<DpCondiDiscapa>, IDpCondiDiscapaRepository
    {
        public DpCondiDiscapaRepository(context context) : base(context)
        {
        }

        public async Task<DataCollection<DpCondiDiscapaDTO>> GetByParamsAsync(SearchDpCondiDiscapaDTO parameters)
        {
            var data = Entity.Select(x => new DpCondiDiscapaDTO
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
            return await data.GetPagedAsync(parameters.page,parameters.take);
        }
    }
}
