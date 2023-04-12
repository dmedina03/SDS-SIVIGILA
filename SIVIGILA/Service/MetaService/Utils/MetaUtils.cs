using Microsoft.VisualBasic;
using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Models.Entities;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SIVIGILA.Service.MetaService.Utils
{
    public static class MetaUtils
    {
        public static Actividad MapToEntity(this ActividadDTO Dto)
        {
            return new Actividad()
            {
                Id = Dto.ActividadID,
                NombreActividad = Dto.NombreActividad,
                DetalleActividad = Dto.DetalleActividad,
                Estado = Dto.Estado,
                FkMeta = Dto.MetaID,
                Responsable=Dto.ResponsableID
            };
        }

        public static IEnumerable<Actividad> MapToEntity(this IEnumerable<ActividadDTO> Dtos)
        {
            return Dtos.Select(Dto => new Actividad
            {
                Id = Dto.ActividadID,
                NombreActividad = Dto.NombreActividad,
                DetalleActividad = Dto.DetalleActividad,
                Estado = Dto.Estado,
                FkMeta = Dto.MetaID,
                Responsable = Dto.ResponsableID
            });
        }
        /// <summary>
        /// Método para convertir la entidad <see cref="Actividad"/> en el DTO <see cref="ActividadGetDTO"/>
        /// </summary>
        /// <param name="entities">Lista de <see cref="Actividad"/></param>
        /// <remarks>Para que funcione correctamente es importante que la Actividad tenga la Info de la meta <see cref="Actividad.FkMetaNavigation"/></remarks>
        /// <returns></returns>
        public static IEnumerable<ActividadGetDTO> MapToDTO(this IEnumerable<Actividad> entities)
        {
            return entities.Select(x => new ActividadGetDTO
            {
                ActividadID = x.Id,
                NombreActividad = x.NombreActividad,
                DetalleActividad = x.DetalleActividad,
                MetaActividad= x.FkMetaNavigation.NombreMeta+" - "+x.NombreActividad,
                Estado = x.Estado,
                MetaID = x.FkMeta
            });
        }

        public static Meta MapToEntity(this MetaDTO Dto)
        {
            return new Meta
            {
                Id = Dto.MetaId,
                NombreMeta = Dto.NombreMeta,
                DetalleMeta = Dto.DetalleMeta,
                FkVigencia = Dto.VigenciaID,
                Actividads = new Collection<Actividad>(Dto.Actividades?.MapToEntity().ToList() ?? new()),
                Responsable = Dto.ResponsableID
            };
        }
        public static IEnumerable<Meta> MapToEntity(this IEnumerable<MetaDTO> Dto)
        {
            return Dto.Select(x => x.MapToEntity());
        }

        public static IEnumerable<MetaGetDTO> MapToDto(this IEnumerable<Meta> entities, bool CopyID=true)
        {
            return entities.Select(x => new MetaGetDTO
            {
                MetaId=CopyID? x.Id:0,
                NombreMeta=x.NombreMeta,
                DetalleMeta=x.DetalleMeta,
                VigenciaID=x.FkVigencia,
                Actividades = x.Actividads.Select(c => new ActividadGetDTO
                {
                    ActividadID=CopyID? c.Id:0,
                    NombreActividad=c.NombreActividad,
                    DetalleActividad=c.DetalleActividad,
                    MetaActividad= x.NombreMeta+" - "+c.NombreActividad,
                    Estado=c.Estado,
                    MetaID=CopyID? c.FkMeta:0
                })
            });
        }
    }
}
