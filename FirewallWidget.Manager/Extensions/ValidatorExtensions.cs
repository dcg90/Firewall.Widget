using FluentValidation.Results;

using System.Linq;

namespace FirewallWidget.Manager.Extensions
{
    internal static class ValidatorExtensions
    {
        public static string[] ExtractErrors(this ValidationResult result)
        {
            return result?.Errors
                .Select(e => e.ErrorMessage)
                .ToArray();
        }
    }
}
