using bentran1vn.question.repository.Database;
using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Extensions;
using Microsoft.EntityFrameworkCore;


namespace bentran1vn.question.src.Repositories.User
{
    public class AccountRepository : IAccountRepository
    {
        private DbSet<Users> users;
        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var users = await context.Users.ToListAsync();

                    if (users == null || !users.Any())
                    {
                        return Enumerable.Empty<Users>();
                    }

                    return users;

                } catch (Exception ex)
                {
                    throw new Exception($"Error retrieving users: {ex.Message}");
                }
            }
        }

        public async Task<Users> GetUsersAsync(string email)
        {
            using (var context = new AppDbContext())
            {
                try
                {
                    var user = await context.Users.FirstOrDefaultAsync(user => user.Email == email);
                    if(user == null)
                    {
                        throw new Exception("Can not find user !");
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving user: {ex.Message}");
                }
            }
        }
    }
}
