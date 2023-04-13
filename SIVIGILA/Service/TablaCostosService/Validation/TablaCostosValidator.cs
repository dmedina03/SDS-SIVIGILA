using FluentValidation;
using SIVIGILA.Commons.DTOs.TablaCostosDTOs;
using SIVIGILA.Repository.Interface;

namespace SIVIGILA.Service.TablaCostosService.Validation
{
    public class TablaCostosValidator : AbstractValidator<TablaCostosDTO>
    {
        private readonly ITablaCostosRepository _repository;
        private readonly IPerfilVigenciaRepository _repositoryPerfilVigencia;
        public TablaCostosValidator(ITablaCostosRepository repository, IPerfilVigenciaRepository repositoryPerfilVigencia)
        {
            _repository = repository;
            _repositoryPerfilVigencia = repositoryPerfilVigencia;
            RuleSet("Create", () =>
            {
                RuleFor(x => x.ID)
                .Equal(0)
                .WithMessage("El ID debe ser igual a 0");

                RuleFor(x => x.PerfilVigencia_ID)
                .NotEqual(0)
                .WithMessage("El valor PerfilVigencia_ID no puede ser igual a 0");

                RuleFor(x => x.PerfilVigencia_ID)
                .MustAsync(async (c, Id, ct) => await _repositoryPerfilVigencia
                .ExistGenericAsync(x => x.PerfilVigenciaID == Id))
                .WithMessage("El PerfilVigencia_ID no existe");


            });
            RuleSet("Any", () =>
            {
                RuleFor(x => x.Valor_TalentoHumano)
                .GreaterThan(0)
                .WithMessage("El valor base de talento humano debe ser mayor que 0");
            });
            
        }
    }
}
