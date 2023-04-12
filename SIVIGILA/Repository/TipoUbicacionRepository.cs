using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.Linea;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using System.Runtime.InteropServices;

namespace SIVIGILA.Repository
{
    public class TipoUbicacionRepository : BaseRepository<TipoUbicacion>, ITipoUbicacionRepository
    {
        private readonly IDetalleUbicacionRepository _detalleUbicacionRepository;
        public TipoUbicacionRepository(context context, IDetalleUbicacionRepository detalleUbicacionRepository) : base(context)
        {
            _detalleUbicacionRepository = detalleUbicacionRepository;
        }

        public async Task<DataCollection<TipoUbicacionDto>> GetByParamsAsync(SearchTipoUbicacionDTO parameters)
        {
            var data = Entity
                .Include(x => x.DetalleUbicacions)
                .Select(x => new TipoUbicacionDto
                {
                Id = x.Id,
                Ubicacion = x.Ubicacion,
                Estado = x.Estado,
                FkTipoDato = x.FkTipoDato,
                    ValoresListaUbicacion = x.DetalleUbicacions.Select(c => new DetalleUbicacionDto
                    {
                    Id = c.Id,
                    Detalle = c.Detalle,
                    Estado = c.Estado,
                    FkTipoUbicacion = c.FkTipoUbicacion
                    }).ToList() 
                }).IgnoreAutoIncludes()
                .AsNoTracking();

            if (!String.IsNullOrEmpty(parameters.Ubicacion))
            {
                data = data.Where(x => x.Ubicacion.ToLower()
                            .Trim().Contains(parameters.Ubicacion.ToLower().Trim()));
            }
            return await data.GetPagedAsync(parameters.page, parameters.take);
        }
    }
}
