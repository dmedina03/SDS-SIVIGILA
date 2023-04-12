using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.Search;
using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Repository.Interface.BaseInterface;

namespace SIVIGILA.Repository
{
    public class VigenciaRepository: BaseRepository<Vigencia>,IVIgenciaRepository
    {
        public VigenciaRepository(context context):base(context)
        {
            
        }

        public async Task<DataCollection<VigenciaGetDTO>> GetByParamsAsync(SearchVigenciaDTO parameters)
        {
            var query = Entity.
                Include(x=>x.VigenciasAdicionales).
                Include(x=>x.EstadoVigencia).
                Where(x => x.FK_Vigencia_Inicial == null)   //Se traen las Vigencias Iniciales
                .Select(x => new VigenciaGetDTO
                {
                    VigenciaID = x.VigenciaID,
                    Presupuesto = x.Presupuesto,
                    _fechaInicio = x.FechaInicio.Date,
                    _fechaFin = x.FechaFin,
                    Estado = new EstadoVigenciaDTO
                    {
                        EstadoID = x.Estado_Vigencia_ID,
                        Descripcion = x.EstadoVigencia.Descripcion
                    },
                    _adicionTiempo = x.AdicionTiempo,
                    Disponible = x.Estado,
                    VigenciasAdicionales = x.VigenciasAdicionales.Select(c => new VigenciaSimpleGetDTO
                    {
                        VigenciaID=c.VigenciaID,
                        Presupuesto = c.Presupuesto,
                        _fechaInicio = c.FechaInicio.Date,
                        _fechaFin = c.FechaFin,
                        Estado = new EstadoVigenciaDTO
                        {
                            EstadoID = c.Estado_Vigencia_ID,
                            Descripcion = c.EstadoVigencia.Descripcion
                        },
                        _adicionTiempo = c.AdicionTiempo,
                        Disponible = c.Estado,
                    }).ToList()
                }).IgnoreAutoIncludes()
            .AsNoTracking();

            //Aca se incluiría la logica para realizar el filtrado (si es requerido)

            //Si se desea ordenar los datos de forma desendente
            if (!parameters.ascending)
                query = query.OrderByDescending(x => x.VigenciaID);

            return  await query.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
