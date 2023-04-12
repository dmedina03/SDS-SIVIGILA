using FluentValidation;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.ProfesionVigenciaService.Validation
{
    public class ProfesionVigenciaValidation : AbstractValidator<PerfilProfesionVigenciaDto>
    {

        private readonly IProfesionVigenciaRepository _profesionVigenciaRepository;
        private readonly IProfesionRepository _profesionRepository;
        public ProfesionVigenciaValidation(IProfesionVigenciaRepository profesionVigenciaRepository,
            IProfesionRepository profesionRepository)
        {
            _profesionVigenciaRepository = profesionVigenciaRepository;
            _profesionRepository = profesionRepository;

            RuleSet("Any", () =>
            {
                RuleFor(x => x.ProfesionID)
                .MustAsync(async (x, profesionId, ct) => await _profesionRepository.ExistGenericAsync(x => x.Id == profesionId))
                .WithMessage("No existe una profesion con ese ID");
            });

        }

    }
}
