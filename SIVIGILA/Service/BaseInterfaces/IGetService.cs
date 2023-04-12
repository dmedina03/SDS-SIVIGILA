namespace SIVIGILA.Service.BaseInterfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">DTO a responder en el Metodo GetByIdAsync</typeparam>
    /// <typeparam name="U">LDTo a responder en el Método GetAllAsync</typeparam>
    public interface IGetService<T,U>
    {
        /// <summary>
        /// Método para obtener la entidad asociado al Dto <typeparamref name="T"/> asociada al ID suministrado
        /// </summary>
        /// <exception cref="NotFoundException">Se arroja la excepción si no se encuentra el elemento</exception>
        /// <param name="Id">ID de la entidad a consultar</param>
        /// <returns>Objeto del tipo <typeparamref name="T"/></returns>
        public Task<T> GetByIdAsync(int Id);

        /// <summary>
        /// Método para traer todas las entidades asociadas al DTO <typeparamref name="U"/> que se encuentran activas
        /// </summary>
        /// <remarks>Trae únicamente las Entidades que se encuentran activas (SIEMPRE Y CUANDO LA ENTIDAD TENGA COLUMNA ESTADO
        /// . sI NO TIENE COLUMNA ESTADO  POR EJEMPLO: <see cref="MetaDTO"/> TRAE TODOS LOS DATOS SIN EXCEPCIÓN</remarks>
        /// <returns></returns>
        public Task<IEnumerable<U>> GetAllAsync();
    }
}
