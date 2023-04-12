using Microsoft.EntityFrameworkCore;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Commons.DTOs.TipoUbicacionDto;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Context;
using SIVIGILA.Models.Entities;
using SIVIGILA.Repository.BaseRepository;
using SIVIGILA.Repository.Interface;
using System.Linq;

namespace SIVIGILA.Repository
{
    public class DetalleUbicacionRepository : BaseRepository<DetalleUbicacion>, IDetalleUbicacionRepository
    {
        public DetalleUbicacionRepository(context context) : base(context)
        {

        }

        public async Task<IEnumerable<DetalleUbicacionDto>> GetDetalleUbicacinoByTipoUbicacion(int idTipoUbi)
        {
            var data = Entity.Where(x => x.Id == idTipoUbi).ToList();
            var list = await Task.WhenAll(data.Select(async (x) =>  new DetalleUbicacionDto()
            {
                Id = x.Id,
                Detalle = x.Detalle,
                Estado = x.Estado,
                FkTipoUbicacion = x.FkTipoUbicacion
            }));
            
            return list;
        }

    }
}
