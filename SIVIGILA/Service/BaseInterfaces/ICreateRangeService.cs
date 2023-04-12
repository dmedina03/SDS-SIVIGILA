namespace SIVIGILA.Service.BaseInterfaces
{
    public interface ICreateRangeService<T>
    {
        public Task<bool> AddRangeAsync(List<T> data);
    }
}
