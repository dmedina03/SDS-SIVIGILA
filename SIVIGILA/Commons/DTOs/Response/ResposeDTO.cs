namespace SIVIGILA.Commons.DTOs.Response
{
    public class ResponseDTO<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
