namespace SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs
{
    public record class DpCondiDiscapaGetDTO
    {
        public int ID { get; set; }
        public string? Descripcion { get; set; }
        public bool TalentoHumano { get; set; }
        public bool Ivc { get; set; }
        public bool Vsa { get; set; }
    }
}
