using FluentValidation;
using SIVIGILA.Commons.DTOs.DatosPoblacionalesDTOs;
using SIVIGILA.Repository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.DpPresenEtnicaServices.Validation
{
    public class DpPresenEtnicaValidator : AbstractValidator<DpPresenEtnicaDTO>
    {
        private readonly IDpPresenEtnicaRepository _dpPresenEtnicaRepository;
        public DpPresenEtnicaValidator(IDpPresenEtnicaRepository dpPresenEtnicaRepository)
        {
            _dpPresenEtnicaRepository = dpPresenEtnicaRepository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.ID)
                .Equal(0)
                .WithMessage("Al crear un nuevo dato poblacional presencia etnica el ID debe ser igual a 0");
            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.ID)
                .NotEqual(0)
                .WithMessage("Al actualizar un nuevo dato poblacional presencia etnica el ID no debe ser igual a 0");
            });
            RuleSet("Any", () =>
            {
                RuleFor(x => x.Descripcion)
                .MustAsync(async (c,descripcion,ct) => !await _dpPresenEtnicaRepository
                .ExistGenericAsync(x => x.Descripcion.ToLower().Trim() == descripcion.ToLower().Trim() && x.Id != c.ID))
                .WithMessage("Ya existe un Dato Poblacional con el mismo nombre");
            });
        }
    }
}
