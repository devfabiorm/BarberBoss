using BarberBoss.Exception.Messages;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace BarberBoss.Application.UseCases.Users;
public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";
    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ErrorMessageKey}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        if (!UpperCase().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        if (!LowerCase().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        if (!Numbers().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        if (!Characters().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_CREDENTIALS);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCase();

    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCase();

    [GeneratedRegex(@"[0-9]")]
    private static partial Regex Numbers();

    [GeneratedRegex(@"[\!\?\*\.]+")]
    private static partial Regex Characters();
}