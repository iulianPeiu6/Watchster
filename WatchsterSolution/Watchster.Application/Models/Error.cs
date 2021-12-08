namespace Watchster.Application.Models
{
    public static class Error
    {
        public static string WrongEmailOrPassword = "Wrong email or password";
        public static string EmailNotFound = "Email does not exist in database";
        public static string EmailNotSent = "Error at sending email";
        public static string WrongPasswordChangeCode = "The given code does not exist";
        public static string InvalidPassword = "The given password does not meet the constraints";

    }
}
