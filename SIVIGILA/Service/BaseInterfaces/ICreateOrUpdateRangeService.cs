namespace SIVIGILA.Service.BaseInterfaces
{
    public interface ICreateOrUpdateRangeService<T>
    {
        public Task<bool> CreateOrUpdateRangeAsync(IEnumerable<T> Dtos);
    }
}
