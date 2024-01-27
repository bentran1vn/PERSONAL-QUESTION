using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;
using bentran1vn.question.src.Requests.UserRequests;
using Microsoft.AspNetCore.Identity;

namespace bentran1vn.question.src.Repositories.User
{
    public interface IAccountRepository
    {
        public Task<IEnumerable<Users>> GetAllUsersAsync();
        public Task<Users> GetUsersAsync(string email);
    }
}
