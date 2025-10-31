namespace microservices.UserAPI.Domain.Models;
{
    public class User
    {
        private Guid Id;
        private string Email;
        private string Name;
        private string Surname;
        private DateOnly Birthday;
        private DataTime Registration;
        private Guid PasswordId;
        private Guid RefreshTokenId;
        private UserRoleEnum UserRole;

        // public User(string email, string name, string surname, )
        // {

        // }
    }
}