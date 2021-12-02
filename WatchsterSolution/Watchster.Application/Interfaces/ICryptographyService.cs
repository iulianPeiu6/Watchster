namespace Watchster.Application.Interfaces
{
    public interface ICryptographyService
    {
        string GetPasswordSHA3Hash(string password);
    }
}