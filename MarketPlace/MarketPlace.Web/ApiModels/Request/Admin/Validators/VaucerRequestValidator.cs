using FluentValidation;

namespace MarketPlace.Web.ApiModels.Request.Admin.Validators;

public class VaucerRequestValidator:AbstractValidator<VaucerRequest>
{
	public VaucerRequestValidator()
	{
		RuleFor(x => x.UserId)
			.NotEmpty()
			.WithMessage("UserId is Required");

		RuleFor(x=>x.ProductId)
			.NotEmpty()
            .WithMessage("ProductId is Required");

		RuleFor(x => x.ExpireTime)
			.NotEmpty()
			.WithMessage("ExpireTime is Required");
    }
}
