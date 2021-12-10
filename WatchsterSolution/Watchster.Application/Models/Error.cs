namespace Watchster.Application.Models
{
    public static class Error
    {
        public const string WrongEmailOrPass = "Wrong email or password";
        public const string EmailNotFound = "Email does not exist in database";
        public const string EmailNotSent = "Error at sending email";
        public const string WrongPassChangeCode = "The given code does not exist";
        public const string InvalidPass = "The given password does not meet the constraints";
    }
}
