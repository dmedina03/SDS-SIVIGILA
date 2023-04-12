namespace SIVIGILA.Service.BaseInterfaces
{
    public interface IExistService
    {
        /// <summary>
        /// Método para validar la existencia de una entidad asociada al ID suministrado
        /// </summary>
        /// <param name="ID">ID de la entidad</param>
        /// <returns>True si la entidad existe, False si no</returns>
        public Task<bool> ExistByIDAsync(int ID);
    }
}
