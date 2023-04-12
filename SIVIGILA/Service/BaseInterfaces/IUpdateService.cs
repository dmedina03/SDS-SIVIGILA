namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IUpdateService<T>
    {
        public Task<bool> UpdateAsync(T Dto);
    }
}
