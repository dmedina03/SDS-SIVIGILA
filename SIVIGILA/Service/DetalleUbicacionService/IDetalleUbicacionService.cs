using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Service.BaseInterfaces;

namespace SIVIGILA.Service.DetalleUbicacionService
{ 
    public interface IDetalleUbicacionService : ICreateService<DetalleUbicacionDto>,IUpdateService<DetalleUbicacionDto>,
                                                IGetService<DetalleUbicacionGetDTO, DetalleUbicacionGetDTO>
    {

    }
}
