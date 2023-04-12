using System.Text.Json.Serialization;

namespace SIVIGILA.Commons.Utils.Pagging
{
    //Clase a enviar al cliente
    public class DataCollection<T>
    {
        public bool HasItems
        {
            get
            {
                if (Items == null)
                {
                    return false;
                }
                return Items.Any();
            }
        }
        public IEnumerable<T> Items { get; set; }
        public List<Calculado> Calculados { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }

    public class Calculado
    {
        public string Detalle { get; set; }
        public decimal Valor { get; set; }
    }
}
