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
            ValidateUserData(email, name, surname, birthday, registration, passwordId, refreshTokenId, userRole);
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
            ValidateUserId(id);
            Id = id;
        }


        public Guid GetId() { return Id; }
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
        public void SetBirthday(DateOnly birthday) { Birthday = birthday; }
        public void SetPasswordId(Guid passwordId) { PasswordId = passwordId; }
        public void SetRefreshTokenId(Guid refreshTokenId) { RefreshTokenId = refreshTokenId; }
        public void SetUserRole(UserRoleEnum userRole) { UserRole = userRole; }


        private void ValidateUserId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty", nameof(id));
        }

        private void ValidateUserData(string email, string name, string surname, DateOnly birthday, DateTime registration,
            Guid passwordId, Guid refreshTokenId, UserRoleEnum userRole)
        {
            
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be null or empty", nameof(email));
            if (!IsValidEmail(email))
                throw new ArgumentException("Email format is invalid", nameof(email));

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be null or empty", nameof(name));
            if (string.IsNullOrWhiteSpace(surname)) throw new ArgumentException("Surname cannot be null or empty", nameof(surname));

            if (birthday == DateOnly.MinValue)
                throw new ArgumentOutOfRangeException("Birthday cannot be minimum date", nameof(birthday));

            if (birthday > DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentOutOfRangeException("Birthday cannot be in the future", nameof(birthday));

            if (DateOnly.FromDateTime(DateTime.Today).AddYears(-100) > birthday)
                throw new ArgumentOutOfRangeException("Birthday seems unrealistic", nameof(birthday));

          
            if (registration == DateTime.MinValue)
                throw new ArgumentException("Registration date cannot be minimum date", nameof(registration));

            if (registration > DateTime.UtcNow.AddHours(1))
                throw new ArgumentException("Registration date cannot be in the future", nameof(registration));

            if (passwordId == Guid.Empty) throw new ArgumentException("Password ID cannot be empty", nameof(passwordId));
            if (refreshTokenId == Guid.Empty) throw new ArgumentException("Refresh token ID cannot be empty", nameof(refreshTokenId));

            if (!Enum.IsDefined(typeof(UserRoleEnum), userRole)) throw new ArgumentException("Invalid user role", nameof(userRole));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}