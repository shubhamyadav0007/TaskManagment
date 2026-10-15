using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskBackend.Models;

namespace TaskBackend.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;

        public AccountRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
            this.roleManager = roleManager;
        }



        public async Task<AuthResponseModel> Login(LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return new AuthResponseModel()
                {
                    IsSuccess = false
                };
            }
            var PasswordChecker = await userManager.CheckPasswordAsync(user, model.Password);
            if (!PasswordChecker)
            {
                return new AuthResponseModel()
                {
                    IsSuccess = false
                };
            }
            var userRoles = await userManager.GetRolesAsync(user);
            var tokenHendeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.config.GetValue<string>("JWT:Key"));

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = System.DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHendeler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHendeler.WriteToken(token);
            return new AuthResponseModel()
            {
                IsSuccess = true,
                Token = encryptedToken,
                Username = user.UserName,
                Role = userRoles[0]
            };
        }
        public async Task<AuthResponseModel> SignUpAdmin(RegisterAdminViewModel model)
        {
            var user = new ApplicationUser 
            {   UserName = model.Email,
                Email = model.Email,
                Name = model.Name ,
                PhoneNumber = model.PhoneNumber,
                OrganizationName = model.OrganizationName,

            };
            var result = await userManager.CreateAsync(user, model.Password);
            var response = new AuthResponseModel();
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                response.IsSuccess = true;
                return response;
            }

            response.IsSuccess = false;
            return response;
        }

        public async Task<AuthResponseModel> SignUpUser(RegisterUserViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber=model.PhoneNumber

            };
            var result = await userManager.CreateAsync(user, model.Password);
            var response = new AuthResponseModel();
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                response.IsSuccess = true;
                return response;
            }

            response.IsSuccess = false;
            return response;
        }


    }
}
