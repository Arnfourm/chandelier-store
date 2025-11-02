using microservices.UserAPI.Domain.Enums;

namespace microservices.UserAPI.Domain.Models
{
    public class User
    {
        private Guid Id;
        private string Email;
        private string Name;
        private string Surname;
        private DateOnly Birthday;
        private DateTime Registration;
        private Guid PasswordId;
        private Guid RefreshTokenId;
        private UserRoleEnum UserRole;

        public User(string email, string name, string surname,
                    DateOnly birthday, DateTime registration, Guid passwordId,
                    Guid refreshTokenId, UserRoleEnum userRole)
        {
            // Валидацию надо сделать
            Email = email;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Registration = registration;
            PasswordId = passwordId;
            RefreshTokenId = refreshTokenId;
            UserRole = userRole;
        }

        public User(Guid id, string email, string name,
                    string surname, DateOnly birthday, DateTime registration,
                    Guid passwordId, Guid refreshTokenId, UserRoleEnum userRole)
                    : this(email, name, surname, birthday, registration, passwordId, 
                    refreshTokenId, userRole)
        {
            // Валидацию надо сделать
            Id = id;
        }

        public string GetEmail() { return Email; }
        public string GetName() { return Name; }
        public string GetSurname() { return Surname; }
        public DateOnly GetBirthday() { return Birthday; }
        public DateTime GetRegistration() { return Registration; }
        public Guid GetPasswordId() { return PasswordId; }
        public Guid GetRefreshTokenId() { return RefreshTokenId; }
        public UserRoleEnum GetUserRole() { return UserRole; }

        public void SetEmail(string email) { Email = email; }
        public void SetName(string name) { Name = name; }
        public void SetSurname(string surname) { Surname = surname; }
        public void SetBirthday(DateOnly birthday) { birthday = Birthday; }
        //Пароль, наверное, стоит обновлять у той же записи, так что метод не нужен (задается вначале и остается статическим всегда)
        //public void SetPasswordId(Guid passwordId) { PasswordId = passwordId; }
        //RefreshToken, наверное, стоит обновлять у той же записи (сам токен, дату создания и дату истечения), так что метод не нужен (задается вначале и остается статическим всегда)
        //public void SetRefreshTokenId(Guid refreshTokenId) { RefreshTokenId = refreshTokenId; }
        public void SetUserRole(UserRoleEnum userRole) { UserRole = userRole; }
    }
}