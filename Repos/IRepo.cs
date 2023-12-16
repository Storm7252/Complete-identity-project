using Microsoft.AspNetCore.Identity;
using soft.Models;

namespace soft.Repos
{
    public interface IRepo
    {
        Task<IdentityResult> Register(Register reg);
        Task<SignInResult> Login(Login UserData);
        void Logout();
    }
}
