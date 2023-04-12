namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IGetAllNamesService
    {
        public Task<IEnumerable<string>> GetAllNamesAsync(string ? search);
    }
}
