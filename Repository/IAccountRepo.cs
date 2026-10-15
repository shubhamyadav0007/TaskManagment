using TaskBackend.Models;

namespace TaskBackend.Repository
{
    public interface IAccountRepo
    {
        Task<AuthResponseModel> SignUpAdmin(RegisterAdminViewModel model);
        Task<AuthResponseModel> SignUpUser(RegisterUserViewModel model);
        Task<AuthResponseModel> Login(LoginViewModel model);
    }
}
