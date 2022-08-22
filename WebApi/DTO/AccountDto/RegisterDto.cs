using FluentValidation;

namespace WebApi.DTO.AccountDto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string CheckPassword { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username can not be empty")
                .MinimumLength(8).WithMessage("Minimum length for username is 8")
                .MaximumLength(16).WithMessage("Maximum length for username is 16");
            RuleFor(x=>x.Fullname).NotEmpty().WithMessage("Name can not be empty")
                .MinimumLength(8).WithMessage("Minimum length for Name is 8")
                .MaximumLength(16).WithMessage("Maximum length for Name is 16");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("Write a password")
                .MinimumLength(8).WithMessage("Minimum length for password is 8")
                .MaximumLength(16).WithMessage("Maximum length for password is 16");
            RuleFor(x=>x.CheckPassword).NotEmpty().WithMessage("Password incorrect")
                .MinimumLength(8).WithMessage("Minimum length for password is 8")
                .MaximumLength(16).WithMessage("Maximum length for password is 16");
            RuleFor(p => p).Custom((r, context) =>
            {
                if (r.Password!=r.CheckPassword)
                {
                    context.AddFailure("CheckPassword", "Passwords do not match");
                }
            });
        }
    }
}
