using FluentValidation;

namespace TaskManager.Application.Features.Tasks.CreateTask;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("El título es requerido")
            .MinimumLength(3).WithMessage("El título debe tener al menos 3 caracteres")
            .MaximumLength(100).WithMessage("El título no puede exceder 100 caracteres");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres");
    }
}
