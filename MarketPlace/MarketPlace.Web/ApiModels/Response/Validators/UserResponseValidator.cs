using FluentValidation;

namespace MarketPlace.Web.ApiModels.Response.Validators;

public class UserResponseValidator:AbstractValidator<UserResponse>
{
	public UserResponseValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("Id is required");

		RuleFor(x => x.FirstName)
			.NotEmpty()
			.WithMessage("FirstName Is Required");

		RuleFor(x => x.LastName)
			.NotEmpty()
			.WithMessage("LastName is Required");

		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress()
			.WithMessage("Email is Required");

		RuleFor(x => x.Role)
			.NotEmpty()
			.WithMessage("Role is Required");
	}
}
