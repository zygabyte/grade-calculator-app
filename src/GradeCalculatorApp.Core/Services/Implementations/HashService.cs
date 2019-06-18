namespace GradeCalculatorApp.Core.Services.Implementations
{
    public interface IHashService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }

    public class HashService : IHashService
    {
        private static string GetRandomSalt() => BCrypt.Net.BCrypt.GenerateSalt(12);

        public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());

        public bool ValidatePassword(string password, string correctHash) => BCrypt.Net.BCrypt.Verify(password, correctHash);
    }
}