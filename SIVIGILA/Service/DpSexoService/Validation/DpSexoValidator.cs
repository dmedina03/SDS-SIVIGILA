using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.DpSexoService.Validation
{
    public class DpSexoValidator : AbstractValidator<DpSexoDTO>
    {
        private readonly IDpSexoRepository _dpSexoRepository;
        public DpSexoValidator(IDpSexoRepository dpSexoRepository)
        {
            _dpSexoRepository = dpSexoRepository;

            RuleSet("Any", () =>
            {
                RuleFor(x => x.Descripcion)
                .MustAsync(async (c, descripcion, ct) => !await _dpSexoRepository
                .ExistGenericAsync(x => x.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim() && x.Id!=c.ID))
                .WithMessage("Ya existe un Dato Poblacional con el mismo nombre");
            });
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ID)
                .Equal(0)
                .WithMessage("Al crear un Dato poblacional Sexo el ID debe ser igual a 0");
            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.ID)
                .NotEqual(0)
                .WithMessage("Al actualizar un Dato poblacional Sexo el ID no debe ser igual a 0");
            });

        }

    }
}
