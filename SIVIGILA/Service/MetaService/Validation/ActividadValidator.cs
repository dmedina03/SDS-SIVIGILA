using FluentValidation;
using SIVIGILA.Commons.DTOs.Actividad;
using SIVIGILA.Commons.DTOs.MetaDTOs;
using SIVIGILA.Repository;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.MetaService.Validation
{
    public class ActividadValidator: AbstractValidator<ActividadDTO>
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IActividadRepository _actividadRepository;

        public ActividadValidator(IMetaRepository metaRepository,IActividadRepository actividadRepository,
                                MetaDTO Dto=null)
        {
            _metaRepository=metaRepository;
            _actividadRepository=actividadRepository;

            When(x => Dto is null, () =>    //La Meta a la cual está asociado no se está creando sino que ya existe
            {
                RuleFor(x => x.MetaID)
                .MustAsync(async (x, id, ct) => await _metaRepository.ExistGenericAsync(c => c.Id == id))
                .WithMessage("La meta asociada al ID suministrado no existe");
            });

            RuleFor(x => x.ActividadID)
                .MustAsync(async (x, id, ct) => await _actividadRepository.
                                                    ExistGenericAsync(c => c.Id == id && c.FkMeta == x.MetaID))
                .WithMessage("La actividad no existe o no se encuentra asociada a la meta suministrada")
                .When(x => x.ActividadID != 0);

            RuleFor(x => x.NombreActividad)
                .NotEmpty()
                .WithMessage("El nombre de la actividad no debe estar vacio")
                .MaximumLength(100)
                .WithMessage("El nombre de la actividad no debe terner mas de 100 caracteres");
            RuleFor(x => x.DetalleActividad)
                .NotEmpty()
                .WithMessage("El detalle de la actividad no puede estar vacio")
                .MaximumLength(150)
                .WithMessage("El detalle de la actividad no puede tener mas de 150 caracteres");

        }
    }
}
