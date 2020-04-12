using FirewallWidget.Manager.DTO;

using FluentValidation;

namespace FirewallWidget.Manager.Validators
{
    public class RuleValidator : AbstractValidator<RuleDto>
    {
        public RuleValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty();
        }
    }
}
