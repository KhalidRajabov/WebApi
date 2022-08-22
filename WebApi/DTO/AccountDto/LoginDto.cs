using FluentValidation;

namespace WebApi.DTO.AccountDto
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty")
                .MinimumLength(8).WithMessage("Minimum length for username is 8")
                .MaximumLength(16).WithMessage("Maximum length for username is 16");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Write a password")
                .MinimumLength(8).WithMessage("Minimum length for password is 8")
                .MaximumLength(16).WithMessage("Maximum length for password is 16");
        }
    }
}
