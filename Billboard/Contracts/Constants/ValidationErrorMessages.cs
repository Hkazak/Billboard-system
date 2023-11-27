namespace Contracts.Constants;

public static class ValidationErrorMessages
{
    public const string InvalidPasswordFormat =
        "Password must be at least 8 characters long, contain at least one digit, uppercase letter and lowercase letter";

    public const string InvalidEmailFormat = "Invalid email format";
    public const string EmailAlreadyUsed = "Email is already used";
    public const string NameIsEmpty = "Name must not be empty";
    public const string PhoneAlreadyUsed = "Phone is already used";
    public const string InvalidPhoneNumber = "Invalid Phone Number";
    public const string ConfirmPasswordShouldBeSameWithPassword = "Your confirm password must be same with password";
    public const string DescriptionIsEmpty = "Description must not be empty";
    public const string AddressIsEmpty = "Address must not be empty";
    public const string WidthIsEmpty = "Width must not be empty";
    public const string HeightIsEmpty = "Heigh must not be empty";
    public const string BillboardTypeIsInvalid = "Billboard type should be  SingleSide, DoubleSide or TripleSide";
    public const string BillboardSurfaceIsInvalid = "";
    public const string SurfaceIsEmpty = "Surface must not be empty";
    public const string TitleIsEmpty = "Title must not be empty";
    public const string PriceIsEmpty = "Price must not be empty";
    public const string EndTimeIsEmpty = "End time must not be empty";
    public const string StartTimeIsEmpty = "Start time must not be empty";
    public const string CombinationOfSurfaceAndTypeMustBeUnique =
        "Billboard surface id and billboard type should be unique for each price rule";
}