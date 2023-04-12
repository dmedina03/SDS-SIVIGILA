using System.Text.Json;

namespace SIVIGILA.Commons.Utils.Pagging
{
    public static class DTOMapperextenssion
    {
        public static T MapTo<T>(this object value)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
        }
    }
}
