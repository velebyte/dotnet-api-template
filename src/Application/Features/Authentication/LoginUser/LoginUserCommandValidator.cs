using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authentication.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
	public LoginUserCommandValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.WithMessage("Email is required")
			.EmailAddress()
			.WithMessage("Email format is incorrect");

		RuleFor(x => x.Password)
			.NotEmpty()
			.WithMessage("Password is required");
	}
}
