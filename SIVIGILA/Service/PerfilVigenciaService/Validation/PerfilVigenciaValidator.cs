using FluentValidation;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Repository;
using SIVIGILA.Repository.Interface;
using SIVIGILA.Service.MetaService.Validation;
using SIVIGILA.Service.VigenciaService;

namespace SIVIGILA.Service.PerfilVigenciaService.Validation
{
    public class PerfilVigenciaValidator : AbstractValidator<PerfilVigenciaDto>
    {
        private readonly IPerfilVigenciaRepository _perfilVigenciaRepository;
        private readonly IPostgradoRepository _postgradoRepository;
        private readonly IVIgenciaRepository _vigenciaRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IProfesionRepository _profesionRepository;
        public PerfilVigenciaValidator(IPerfilVigenciaRepository perfilVigenciaRepository, IVIgenciaRepository vigenciaRepository,
            IPerfilRepository perfilRepository, IProfesionRepository profesionRepository, IPostgradoRepository postgradoRepository)
        {
            _perfilVigenciaRepository = perfilVigenciaRepository;
            _vigenciaRepository = vigenciaRepository;
            _perfilRepository = perfilRepository;
            _profesionRepository = profesionRepository;
            _postgradoRepository = postgradoRepository;

            RuleSet("Create", () =>
            {
                RuleFor(x => x.PerfilVigenciaID)
                .Equal(0)
                .WithMessage("Al crear un Perfil Vigencia el ID debe ser igual a 0");
            });
            RuleSet("Update", () =>
            {
                RuleFor(x => x.PerfilVigenciaID)
                .NotEqual(0)
                .WithMessage("Al editar un Perfil Vigencia el ID no debe ser igual a 0");
            });

            RuleSet("Any", () =>
            {
                RuleFor(x => x.VigenciaID)
                .MustAsync(async (x, Id, ct) => await _vigenciaRepository.ExistGenericAsync(x => x.VigenciaID == Id))
                .WithMessage("No existe una vigencia con el ID suministrado");

                RuleFor(x => x.PerfilID)
                .MustAsync(async (x, Id, ct) => await _perfilRepository.ExistGenericAsync(x => x.Id == Id))
                .WithMessage("No existe un perfil con el ID suministrado");

                //RuleForEach(x => x.ProfesionVigencia)
                //.SetValidator(x => new ProfesionVigenciaValidator(_profesionRepository, _postgradoRepository, x));

                RuleForEach(x => x.ProfesionVigencia)
                .ChildRules(profesion =>
                {
                    profesion.RuleFor(x => x.ListPostgrado)
                    .MustAsync(async (x, value, ct) => !(value == false && x.PostgradoVigencia.Count() > 0))
                    .WithMessage("No es posible agregar los valores de postgrado");

                    profesion.RuleFor(x => x.ProfesionID)
                    .MustAsync(async (x, value, ct) => await _profesionRepository.ExistGenericAsync(x =>x.Id == value))
                    .WithMessage("No existe una Profesion con el ID suministrado");

                    profesion.RuleForEach(p => p.PostgradoVigencia)
                    .ChildRules(postgrado =>
                    {
                        postgrado.RuleFor(x => x.PostgradoID)
                        .MustAsync(async (x, value, ct) => await _postgradoRepository.ExistGenericAsync(x => x.Id == value))
                        .WithMessage("No existe un Postgrado con el ID suministrado");
                    });
                });
                    
            });
        }

    }
}
