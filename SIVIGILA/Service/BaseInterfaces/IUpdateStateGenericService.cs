namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IUpdateStateGenericService<T>
    {
        public Task<bool> UpdateStateAsync(T Dto);
    }
}
