using FluentValidation;
using SIVIGILA.Commons.DTOs.DetalleUbicacionDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.DetalleUbicacionService.Validation
{
    public class DetalleUbicacionValidator : AbstractValidator<DetalleUbicacionDto>
    {
        private readonly IDetalleUbicacionRepository _detalleUbicacionRepository;
        public DetalleUbicacionValidator(IDetalleUbicacionRepository detalleUbicacionRepository)
        {
            _detalleUbicacionRepository = detalleUbicacionRepository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.Id)
                .Equal(0)
                .WithMessage("Al crear un valor de la lista, el ID debe ser 0");
                
            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Al actualizar un valor de la lista, el ID no puede ser 0");
                
            });
            RuleSet("Any", () =>
            {
                RuleFor(x => x.Detalle)
                .MustAsync(async (c, detalle, ct) => !await _detalleUbicacionRepository
                .ExistGenericAsync(x => x.Detalle.ToLower().Trim() == detalle.ToLower().Trim() &&
                                   x.FkTipoUbicacion == c.FkTipoUbicacion))
                .WithMessage("No puede haber un mismo valor de la lista igual, la descripción debe ser diferente");
            });

        }
    }
}
