using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.DataAnnotations.DAO
{
    public class UserDAO : IUserDAO
    {
        private readonly UserDbContext _userDbContext;

        public UserDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _userDbContext.Users
                .Select(userEntity => new User
                (
                    userEntity.Id,
                    userEntity.Email,
                    userEntity.Name,
                    userEntity.Surname,
                    userEntity.Birthday,
                    userEntity.Registration,
                    userEntity.PasswordId,
                    userEntity.RefreshTokenId,
                    userEntity.UserRole
                )).ToListAsync();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var userEntity = await _userDbContext.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception($"User with id {id} not found");
            }

            return new User(
                userEntity.Id,
                userEntity.Email,
                userEntity.Name,
                userEntity.Surname,
                userEntity.Birthday,
                userEntity.Registration,
                userEntity.PasswordId,
                userEntity.RefreshTokenId,
                userEntity.UserRole
            );
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var userEntity = await _userDbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

            if (userEntity == null)
            {
                throw new Exception($"User with email {email} not found");
            }

            return new User(
                userEntity.Id,
                userEntity.Email,
                userEntity.Name,
                userEntity.Surname,
                userEntity.Birthday,
                userEntity.Registration,
                userEntity.PasswordId,
                userEntity.RefreshTokenId,
                userEntity.UserRole
            );
        }

        public async Task<Guid> CreateUser(User user)
        {
            // Object password might needs to be
            var userEntity = new UserEntity
            {
                Email = user.GetEmail(),
                Name = user.GetName(),
                Surname = user.GetSurname(),
                Birthday = user.GetBirthday(),
                Registration = user.GetRegistration(),
                PasswordId = user.GetPasswordId(),
                RefreshTokenId = user.GetRefreshTokenId(),
                UserRole = user.GetUserRole()
            };

            await _userDbContext.Users.AddAsync(userEntity);
            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new user {ex.Message}", ex);
            }

            return userEntity.Id;
        }

        public async Task UpdateUser(User user)
        {
            await _userDbContext.Users
                .Where(userEntity => userEntity.Id == user.GetId())
                .ExecuteUpdateAsync(userSetters => userSetters
                .SetProperty(userEntity => userEntity.Email, user.GetEmail())
                .SetProperty(userEntity => userEntity.Name, user.GetName())
                .SetProperty(userEntity => userEntity.Surname, user.GetSurname())
                .SetProperty(userEntity => userEntity.Birthday, user.GetBirthday())
                .SetProperty(userEntity => userEntity.RefreshTokenId, user.GetRefreshTokenId())
                .SetProperty(userEntity => userEntity.UserRole, user.GetUserRole()));

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to update user's info. Error message:\n{ex.Message}", ex);
            }
        }

        public async Task DeleteUser(Guid id)
        {
            await _userDbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete user: {ex.Message}", ex);
            }
        }
    }
}