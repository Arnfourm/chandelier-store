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
            var password = await _userDbContext.Passwords.SingleOrDefaultAsync(password => password.Id == id);

            if (password == null)
            {
                throw new Exception($"Password with id {id} not found");
            }

            return new Password(password.Id, password.PasswordHash, password.PasswordSaulHash)
        }

        public async Task<Guid> CreatePassword(Password password)
        {
            var passwordEntity = new PasswordEntity
            {
                passwordHash = password.GetPassword;
                passwordSaulHash = password.GetPasswordSaulHash;
            }

            await _userDbContext.Passwords.AddAsync(passwordEntity);
            await _userDbContext.Passwords.SaveChangesAsync();

            return passwordEntity.Id;
        }
    }
}