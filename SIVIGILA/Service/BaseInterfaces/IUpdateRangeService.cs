namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IUpdateRangeService<T>  
    {
        public Task<bool> UpdateRangeAsync(List<T> data);
    }
}
