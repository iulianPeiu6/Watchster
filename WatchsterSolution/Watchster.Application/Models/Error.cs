namespace Watchster.Application.Models
{
    public static class Error
    {
        public const string WrongEmailOrPass = "Wrong email or password";
        public const string EmailNotFound = "Email does not exist in database";
        public const string EmailNotSent = "Error at sending email";
        public const string WrongPassChangeCode = "The given code does not exist";
        public const string InvalidPass = "The given password does not meet the constraints";
        public const string UserNotFound = "User does not exist in database";
        public const string MovieNotFound = "Movie does not exist in database";
        public const string RatingNotInRange = "Rating value is not between 0 and 10";
        public const string MovieAlreadyRated = "Current Movie was already rated by the user";
    }
}
