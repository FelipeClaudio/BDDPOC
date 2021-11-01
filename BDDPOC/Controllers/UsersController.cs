using BDDPOC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BDDPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await this.userService.GetUsers().ConfigureAwait(false));
        }
    }
}
