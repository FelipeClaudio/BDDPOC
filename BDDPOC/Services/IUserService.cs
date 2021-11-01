using BDDPOC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDDPOC.Services
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
    }
}
