using thewall9.data.Models;
using thewall9.bll.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.bll
{
    public class UserBLL
    {
        public UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        public Task<IdentityResult> CreateAsync(RegisterBindingModel model)
        {
            var _AU = UserManager.FindByEmail(model.Email);
            if (_AU == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                };
                return UserManager.CreateAsync(user, model.Password);
            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var _AU = UserManager.FindByEmail(user.Email);
            if (_AU == null)
            {
                return UserManager.CreateAsync(user);
            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public ApplicationUser Create(string Name,string Email, string Password)
        {
            var _AU = UserManager.FindByEmail(Email);
            if (_AU == null)
            {
                _AU = new ApplicationUser
                {
                    Email = Email,
                    UserName = Email,
                    Name=Name
                };
                var _U = UserManager.Create(_AU, Password);
                if (_U.Succeeded)
                    return _AU;
                throw new RuleException("Error creando usuarios");

            }
            throw new RuleException("Ya existe un usuario con ese email");
        }
        public ApplicationUser Find(string Email)
        {
            return UserManager.FindByEmail(Email);
        }
        public List<string> GetRoles(string UserID)
        {
            return UserManager.GetRoles(UserID).ToList();
        }
    }
}
