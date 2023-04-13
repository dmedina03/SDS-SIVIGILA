using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Models.Entities;

namespace SIVIGILA.Service.TablaCostosService.Utils
{
    public static class TablaCostosMapper
    {

        public static TablaCostos MapToEntity(this TablaCostosDTO Dto)
        {
            return new TablaCostos()
            {
                ID = Dto.ID,
                PerfilVigencia_ID = Dto.PerfilVigencia_ID,
                Pct_TalentoHumano = Dto.Pct_TalentoHumano,
                Pct_Administracion = Dto.Pct_Administracion,
                Pct_Ruralidad = Dto.Pct_Ruralidad,
                Pct_Insumos = Dto.Pct_Insumos,
                HoraMes = Dto.HoraMes,
                Valor_Administracion = Dto.Valor_Administracion,
                Valor_HoraRural = Dto.Valor_HoraRural,
                Valor_HoraUrbano = Dto.Valor_HoraUrbano,
                Valor_Insumos = Dto.Valor_Insumos,
                Valor_MesHoraRural = Dto.Valor_MesHoraRural,
                Valor_MesHoraUrbano = Dto.Valor_MesHoraUrbano,
                Valor_TalentoHumano = Dto.Valor_TalentoHumano,
                Valor_TalentoHumanoHoraRural = Dto.Valor_TalentoHumanoHoraRural,
                Valor_TalentoHumanoHoraUrbano = Dto.Valor_TalentoHumanoHoraUrbano
                
            };
        }

        public static IEnumerable<TablaCostos> MapToEntity(this IEnumerable<TablaCostosDTO> Dtos)
        {
            return Dtos.Select(x => x.MapToEntity());
        }

    }
}
