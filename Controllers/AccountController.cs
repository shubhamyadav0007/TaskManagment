using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Models;
using TaskBackend.Repository;

namespace TaskBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepo accountRepo;
        public AccountController(IAccountRepo accountRepo)
        {
            this.accountRepo = accountRepo;
        }
        [HttpPost("signupAdmin")]
        public IActionResult SignUpAdmin(RegisterAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = accountRepo.SignUpAdmin(model);
                if (result.Result != null)
                {
                    if (result.Result.IsSuccess) { return Ok(); }
                    return NotFound();
                }
            }
            return NotFound();
        }
        [HttpPost("signupUser")]
        public IActionResult SignUpUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = accountRepo.SignUpUser(model);
                if (result.Result != null)
                {
                    if (result.Result.IsSuccess) { return Ok(); }
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await accountRepo.Login(model);
            if (result != null)
            {
                if (result.IsSuccess) { return Ok(result); }
                return Unauthorized();
            }
            return Unauthorized();
        }

    }
}
