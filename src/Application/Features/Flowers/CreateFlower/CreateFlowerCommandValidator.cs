namespace Application.Features.Flowers.CreateFlower;

public class CreateFlowerCommandValidator : AbstractValidator<CreateFlowerCommand>
{
	public CreateFlowerCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Flower name is required");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Flower type is required");
    }
}
