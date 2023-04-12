using FluentValidation;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.PerfilVigenciaService.Validation
{
    public class ProfesionVigenciaValidator : AbstractValidator<PerfilProfesionVigenciaDto>
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IPostgradoRepository _postgradoRepository;
        public ProfesionVigenciaValidator(IProfesionRepository profesionRepository,
            IPostgradoRepository postgradoRepository,PerfilVigenciaDto Dto=null)
        {
            _profesionRepository = profesionRepository;
            _postgradoRepository = postgradoRepository;

            RuleFor(x => x.ProfesionID)
                .MustAsync(async (x, id, ct) => await _profesionRepository.ExistGenericAsync(x => x.Id==id))
                .WithMessage("No existe una Profesion con el ID ingresado");

            //RuleFor(x => x.ListPostgrado)
            //    .Equals(false)
            //    .

            //RuleForEach(x => x.PostgradoVigencia)
            //    .SetValidator(x => new PostgradoVigenciaValidator(_postgradoRepository, x));
        }

        private bool ValidateListaPostgrado(bool value)
        {
            if (!value)
                return false;
            else
                return true;
        }
    }
}
