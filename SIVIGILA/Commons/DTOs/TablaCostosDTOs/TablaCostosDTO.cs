using System.ComponentModel.DataAnnotations.Schema;

namespace SIVIGILA.Commons.DTOs.TablaCostosDTOs
{
    public class TablaCostosDTO
    {
        public int ID { get; set; }
        public int PerfilVigencia_ID { get; set; }
        public int Pct_TalentoHumano { get; set; }
        public int Pct_Insumos { get; set; }
        public int Pct_Administracion { get; set; }
        public int Pct_Ruralidad { get; set; }
        public int HoraMes { get; set; }
        public decimal Valor_TalentoHumano { get; set; }
        public decimal Valor_Insumos
        {
            get
            {
                return (Valor_TalentoHumano*Pct_Insumos)/100;
            }
        }
        public decimal Valor_Administracion
        {
            get
            {
                return (Valor_TalentoHumano * Pct_Administracion) / 100;
            }
        }
        public decimal Valor_MesHoraUrbano
        {
            get
            {
                return Valor_TalentoHumano + Valor_Insumos + Valor_Administracion;
            }
        }
        public decimal Valor_HoraUrbano
        {
            get
            {
                if (HoraMes==0)
                {
                    return 0;
                }
                else
                {
                    return Valor_MesHoraUrbano / HoraMes;
                }
            }
        }
        public decimal Valor_MesHoraRural
        {
            get
            {
                return (Valor_MesHoraUrbano * (100+Pct_Ruralidad))/100;
            }
        }
        public decimal Valor_HoraRural
        {
            get
            {
                if (HoraMes==0)
                {
                    return 0;
                }
                else
                {
                    return Valor_MesHoraRural / HoraMes;
                }
            }
        }
        public decimal Valor_TalentoHumanoHoraUrbano
        {
            get
            {
                if (HoraMes==0)
                {
                    return 0;
                }
                else
                {
                    return Valor_TalentoHumano / HoraMes;
                }
            }
        }
        public decimal Valor_TalentoHumanoHoraRural
        {
            get
            {
                if (HoraMes==0)
                {
                    return 0;
                }
                else
                {
                    return ((Valor_TalentoHumano * (100 + Pct_Ruralidad)) / 100) / HoraMes;
                }
            }
        }
    }
}
