using FluentValidation;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.MetaService.Validation
{
    public class MetaValidator: AbstractValidator<MetaDTO>
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IVIgenciaRepository _vigenciaRepository;
        private readonly IActividadRepository _actividadRepository;
        public MetaValidator(IMetaRepository metaRepository, IVIgenciaRepository vigenciaRepository,
                            IActividadRepository actividadRepository)
        {
            _metaRepository = metaRepository;
            _vigenciaRepository = vigenciaRepository;
            _actividadRepository = actividadRepository;
            RuleSet("Create", () =>
            {
                RuleFor(x => x.MetaId)
                .Equal(0)
                .WithMessage("El Id de las metas deben ser 0 cuando se están creando");
            });

            RuleSet("Any", () =>
            {
                RuleFor(x => x.MetaId)
                .MustAsync(async (x, id, ct) => await _metaRepository.ExistGenericAsync(c => c.Id == id))
                .WithMessage("La meta no se existe")
                .When(x => x.MetaId != 0);

                RuleFor(x => x.NombreMeta)
                .NotEmpty()
                .WithMessage("El nombre de la meta no puede estar vacio")
                 .MaximumLength(100)
                .WithMessage("El nombre de la meta no debe tener mas de 100 caracteres")
                .MustAsync(async (x, nombre, ct) => !await _metaRepository.
                        ExistGenericAsync(c => c.NombreMeta.ToLower().Trim() == nombre.ToLower().Trim() && c.Id != x.MetaId && c.FkVigencia==x.VigenciaID))
                .WithMessage("La Nombre la meta ya se encuentra registrado en la Vigencia Actual");

                RuleFor(x => x.DetalleMeta)
                .NotEmpty()
                .WithMessage("El detalle de la meta no puede estar vacio")
                .MaximumLength(150)
                .WithMessage("El detalle de la meta no debe tener mas de 150 caracteres");

                RuleFor(x => x.VigenciaID)
                .MustAsync(async (x, id, ct) => await _vigenciaRepository.ExistGenericAsync(x => x.VigenciaID == id))
                .WithMessage("No existe una vigencia asociada al ID suministrado");

                RuleForEach(x => x.Actividades)
                .SetValidator(x => new ActividadValidator(_metaRepository, _actividadRepository, x));
            });
        }
    }
}
