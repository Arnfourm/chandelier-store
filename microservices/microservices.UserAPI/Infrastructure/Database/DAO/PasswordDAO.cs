using microservices.UserAPI.Domain.Interfaces.DAO;
using microservices.UserAPI.Domain.Models;
using microservices.UserAPI.Infrastructure.Database.Contexts;
using microservices.UserAPI.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace microservices.UserAPI.Infrastructure.Database.DAO
{
    public class PasswordDAO : IPasswordDAO
    {
        private readonly UserDbContext _userDbContext;

        public PasswordDAO(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task<Password> GetPasswordById(Guid id)
        {
            var passwordEntity = await _userDbContext.Passwords.FindAsync(id);

            if (passwordEntity == null)
            {
                throw new Exception($"Password with id {id} not found");
            }

            return new Password(passwordEntity.Id, passwordEntity.PasswordHash, passwordEntity.PasswordSaulHash);
        }

        public async Task<Guid> CreatePassword(Password password)
        {
            var passwordEntity = new PasswordEntity
            {
                PasswordHash = password.GetPasswordHash(),
                PasswordSaulHash = password.GetPasswordSaulHash()
            };

            await _userDbContext.Passwords.AddAsync(passwordEntity);
            try
            {
                await _userDbContext.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to add new password {ex.Message}", ex);
            }

            return passwordEntity.Id;
        }

        public async Task DeletePassword(Guid id)
        {
            await _userDbContext.Users.Where(password => password.Id == id).ExecuteDeleteAsync();

            try
            {
                await _userDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while trying to delete password: {ex.Message}", ex);
            }
        }
    }
}