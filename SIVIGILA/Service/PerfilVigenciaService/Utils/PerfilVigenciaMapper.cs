using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIVIGILA.Service.PerfilVigenciaService.Utils
{
    public static class PerfilVigenciaMapper
    {
        public static IEnumerable<PostgradoVigencia> MapToEntity(this IEnumerable<PerfilPostgradoVigenciaDto> Dtos)
        {
            return Dtos.Select(Dto => new PostgradoVigencia
            {
                PostgradoVigenciaID = Dto.PostgradoVigenciaID,
                ProfesionVigenciaID = Dto.ProfesionVigenciaID,
                PostgradoID = Dto.PostgradoID,
                ResponsableCambio = Dto.ResponsableID
            });
        }
        public static IEnumerable<ProfesionVigencia> MapToEntity(this IEnumerable<PerfilProfesionVigenciaDto> Dtos)
        {
            List<ProfesionVigencia> listaProfesion = new();
            foreach (var dto in Dtos)
            {
                if (dto.ListPostgrado.Equals(false))
                {
                    ProfesionVigencia profesion = new()
                    {
                        PerfilVigenciaID = dto.PerfilVigenciaID,
                        ProfesionVigenciaID = dto.ProfesionVigenciaID,
                        ProfesionID = dto.ProfesionID,
                        ResponsableCambio = dto.ResponsableID
                    };
                    listaProfesion.Add(profesion);
                }
                else
                {
                    ProfesionVigencia profesion = new()
                    {
                        PerfilVigenciaID = dto.PerfilVigenciaID,
                        ProfesionVigenciaID = dto.ProfesionVigenciaID,
                        ProfesionID = dto.ProfesionID,
                        Postgrados = new Collection<PostgradoVigencia>(dto.PostgradoVigencia?.MapToEntity().ToList() ?? new()),
                        ResponsableCambio = dto.ResponsableID
                    };
                    listaProfesion.Add(profesion);
                }
            }
            return listaProfesion;
        }
        public static PerfilVigencia MapToEntity(this PerfilVigenciaDto Dto)
        {
            return new PerfilVigencia
            {
                PerfilVigenciaID = Dto.PerfilVigenciaID,
                PerfilID = Dto.PerfilID,
                VigenciaID = Dto.VigenciaID,
                Profesiones = new Collection<ProfesionVigencia>(Dto.ProfesionVigencia?.MapToEntity().ToList() ?? new ()),
                ResponsableCambio = Dto.ResponsableID
            };
        }

        public static IEnumerable<PerfilVigencia> MapToEntity(this IEnumerable<PerfilVigenciaDto> Dtos)
        {
            return Dtos.Select(x => x.MapToEntity());
        }


    }
}
