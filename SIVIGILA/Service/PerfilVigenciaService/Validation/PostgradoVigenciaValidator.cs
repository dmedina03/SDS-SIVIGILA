using FluentValidation;
using SIVIGILA.Commons.DTOs.PerfilVigenciaDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.PerfilVigenciaService.Validation
{
    public class PostgradoVigenciaValidator : AbstractValidator<PerfilPostgradoVigenciaDto>
    {
        private readonly IPostgradoRepository _postgradoRepository;
        public PostgradoVigenciaValidator(IPostgradoRepository postgradoRepository, PerfilProfesionVigenciaDto Dto =null)
        {
            _postgradoRepository = postgradoRepository;

            RuleFor(x => x.PostgradoID)
                .MustAsync(async (x, Id, ct) => await _postgradoRepository.ExistGenericAsync(x => x.Id == Id))
                .WithMessage("No existe Postgrado con el ID ingresado");
        }
    }
}
