namespace Application.Features.Flowers.GetFlower;

public class GetFlowerQueryValidator : AbstractValidator<GetFlowerQuery>
{
	public GetFlowerQueryValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty();
	}
}
