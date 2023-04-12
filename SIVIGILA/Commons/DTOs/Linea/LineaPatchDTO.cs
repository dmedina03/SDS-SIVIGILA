using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.DTOs.Linea
{
    public record LineaPatchDTO
    {
        public int id { get; set; }
        public bool estado { get; set; }
        public Guid ResponsableID { get; set; }
    }
}
