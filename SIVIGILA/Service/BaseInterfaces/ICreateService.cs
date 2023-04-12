namespace SIVIGILA.Service.BaseInterfaces
{
    public interface ICreateService<T> 
    {
        public Task<int> AddAsync(T Dto);
    }
}
