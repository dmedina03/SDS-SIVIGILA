using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Repository
{
    public class MetaRepository: BaseRepository<Meta>, IMetaRepository
    {
        public MetaRepository(context context):base(context)
        {
            
        }

        public async Task<DataCollection<MetaGetDTO>> GetByParamsAsync(SearchMetaDTO parameters)
        {
            var data = await Entity.Where(x => x.FkVigencia == parameters.VigenciaID)
                .Select(x=> new MetaGetDTO
                {
                    MetaId=x.Id,
                    NombreMeta=x.NombreMeta,
                    DetalleMeta=x.DetalleMeta,
                    VigenciaID= x.FkVigencia,
                    Actividades= x.Actividads.Select(c=>new Commons.DTOs.Actividad.ActividadGetDTO
                    {
                        ActividadID=c.Id,
                        NombreActividad=c.NombreActividad,
                        MetaActividad=x.NombreMeta+" - "+c.NombreActividad,
                        DetalleActividad=c.DetalleActividad,
                        Estado=c.Estado,
                        MetaID=c.FkMeta,
                    })
                })
                .GetPagedAsync(parameters.page, parameters.take);

            return data;
        }
    }
}
