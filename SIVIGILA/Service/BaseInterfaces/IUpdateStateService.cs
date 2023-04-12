namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IUpdateStateService
    {
        /// <summary>
        /// MNétodo para actualizar el estado de la entidad asociada al DTO
        /// </summary>
        /// <param name="Dto">DTO con la información necesaria para realizar la actualización</param>
        /// <returns></returns>
        public Task<bool> UpdateStateAsync(int ID, bool estado);
    }
}
