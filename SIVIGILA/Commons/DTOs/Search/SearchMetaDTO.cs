using Microsoft.AspNetCore.Mvc;

namespace SIVIGILA.Commons.DTOs.Search
{
    public class SearchMetaDTO: SearchBaseDTO
    {
        [FromRoute]
        public int VigenciaID { get; set; }
    }
}
