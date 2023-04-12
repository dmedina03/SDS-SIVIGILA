namespace SIVIGILA.Commons.ErrorHandling.CustomExceptions
{
    public class ValidationDataException: Exception
    {
        public IDictionary<string, string[]>? Errores { get; set; }
        public ValidationDataException(string message): base(message)
        {
            
        }
        public ValidationDataException(string message, IDictionary<string, string[]> Errores): base(message) 
        {
            this.Errores=Errores;
        }
    }
}
