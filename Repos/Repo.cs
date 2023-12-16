using Microsoft.AspNetCore.Identity;
using soft.Models;

namespace soft.Repos
{
    public class Repo:IRepo
    {
        private readonly UserManager<IdentityUser>_usermanager;
        private readonly SignInManager<IdentityUser> _signinmanager;

        public Repo(UserManager<IdentityUser> usermanager,SignInManager<IdentityUser> signinmanager)
        {
            this._usermanager = usermanager;
            this._signinmanager = signinmanager;
        }
        public async Task<IdentityResult> Register(Register reg)
        {
            var user = new IdentityUser()
            {
                Email = reg.Email,
                UserName = reg.Email
            };
            var res=await _usermanager.CreateAsync(user, reg.Password);
            if (!string.IsNullOrEmpty(reg.Roles.ToString()))
            {
                await _usermanager.AddToRoleAsync(user, reg.Roles.ToString());
            }
            else
            {
                await _usermanager.AddToRoleAsync(user, Roles.Student);
            }
            return res;
        }

        public async Task<SignInResult> Login(Login UserData)
        {
            var res=await _signinmanager.PasswordSignInAsync(UserData.Email, UserData.Password,false,false);
            return res;
        }

        public async void Logout()
        {
            await _signinmanager.SignOutAsync();
        }
    }
}
