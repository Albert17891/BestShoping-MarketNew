using FluentValidation;

namespace MarketPlace.Web.ApiModels.Response.Validators;

public class VaucerResponseValidator : AbstractValidator<VaucerResponse>
{
	public VaucerResponseValidator()
	{
		RuleFor(x => x.VaucerName)
			.NotEmpty()
			.WithMessage("Vaucer Name is Required");

		RuleFor(x => x.Price)
			.NotEmpty()
			.WithMessage("Price is not empty");

		RuleFor(x => x.ProductId)
			.NotEmpty()
			.WithMessage("ProductId is not empty");
	}
}
