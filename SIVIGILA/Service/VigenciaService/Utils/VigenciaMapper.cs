using SIVIGILA.Commons.DTOs.Vigencia;
using SIVIGILA.Commons.Utils.Pagging;
using SIVIGILA.Models.Entities;
using System.Collections.ObjectModel;

namespace SIVIGILA.Service.VigenciaService.Utils
{
    public static class VigenciaMapper
    {
        public static IEnumerable<VigenciaSimpleGetDTO> MapToDTO(this IEnumerable<Vigencia> data)
        {
            return data.Select(x => new VigenciaSimpleGetDTO
            {
                VigenciaID = x.VigenciaID,
                Presupuesto = x.Presupuesto,
                _fechaInicio = x.FechaInicio,
                _fechaFin = x.FechaFin,
                _adicionTiempo = x.AdicionTiempo,
                Disponible = x.Estado,
                Estado = new EstadoVigenciaDTO()
                {
                    EstadoID = x.Estado_Vigencia_ID,
                    Descripcion = x.EstadoVigencia.Descripcion
                },

            });
        }

        public static Vigencia MapToEntity(this VigenciaDTO dto) 
        {
            return new Vigencia()
            {
                VigenciaID = dto.VigenciaID,
                Presupuesto = dto.Presupuesto,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado_Vigencia_ID = dto.EstadoID,
                AdicionTiempo = dto.AdicionTiempo,
                Estado = dto.Disponible,
                VigenciasAdicionales = new Collection<Vigencia>(dto.VigenciasAdicionales?.Select(x => new Vigencia
                {
                    VigenciaID = x.VigenciaID,
                    Presupuesto = x.Presupuesto,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    Estado_Vigencia_ID = x.EstadoID,
                    AdicionTiempo = x.AdicionTiempo,
                    Estado = x.Disponible
                }).ToList()?? new List<Vigencia>())
            };
        }

        public static IEnumerable<Vigencia> MapToEntity(this IEnumerable<VigenciaDTO> Dto)
        {
            return Dto.Select(x => new Vigencia
            {
                VigenciaID = x.VigenciaID,
                Presupuesto = x.Presupuesto,
                FechaInicio = x.FechaInicio,
                FechaFin = x.FechaFin,
                Estado_Vigencia_ID = x.EstadoID,
                AdicionTiempo = x.AdicionTiempo,
                Estado = x.Disponible,
                VigenciasAdicionales = new Collection<Vigencia>(x.VigenciasAdicionales?.Select(x => new Vigencia
                {
                    VigenciaID = x.VigenciaID,
                    Presupuesto = x.Presupuesto,
                    FechaInicio = x.FechaInicio,
                    FechaFin = x.FechaFin,
                    Estado_Vigencia_ID = x.EstadoID,
                    AdicionTiempo = x.AdicionTiempo,
                    Estado = x.Disponible
                }).ToList() ?? new List<Vigencia>())
            });
        }
    }
}
