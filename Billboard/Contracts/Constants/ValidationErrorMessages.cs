namespace Contracts.Constants;

public static class ValidationErrorMessages
{
    public const string InvalidPasswordFormat =
        "Password must be at least 8 characters long, contain at least one digit, uppercase letter and lowercase letter";

    public const string InvalidEmailFormat = "Invalid email format";
    public const string EmailAlreadyUsed = "Email is already used";
    public const string NameIsEmpty = "Name must not be empty";
}