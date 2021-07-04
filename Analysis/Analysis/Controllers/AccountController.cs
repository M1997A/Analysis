using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analysis.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analysis.Controllers
{
    [Authorize(Roles = "Admins")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private IUserValidator<IdentityUser> userValidator;
        private IPasswordValidator<IdentityUser> passwordValidator;
        private IPasswordHasher<IdentityUser> passwordHasher;
        public AccountController(UserManager<IdentityUser> usrMgr,
        IUserValidator<IdentityUser> userValid,
        IPasswordValidator<IdentityUser> passValid,
        IPasswordHasher<IdentityUser> passwordHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
        }
        public async Task <IActionResult> Index()
        {
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            List<IdentityUser> UsersList = new List<IdentityUser>();
            UsersList.Add(user);
            return View(userManager.Users.Except(UsersList));
        }
        public async Task<IActionResult> EditAdmin()
        {
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(string Id, string OldPassword, string NewPassword)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(OldPassword))
                {
                    PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, OldPassword);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        if (!string.IsNullOrEmpty(NewPassword))
                        {
                            IdentityResult result = await passwordValidator.ValidateAsync(userManager, user, NewPassword);
                            if (result.Succeeded)
                            {
                                user.PasswordHash = passwordHasher.HashPassword(user, NewPassword);
                                IdentityResult identityResult = await userManager.UpdateAsync(user);
                                if (result.Succeeded)
                                {
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                            else
                            {
                                AddErrorsFromResult(result);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Enter your new password");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "your password is not correct");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Enter your password");
                }

            }
            return View(user);
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM createUserVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = createUserVM.UserName
                };
                IdentityResult validName  = await userValidator.ValidateAsync(userManager, user);
                if (validName.Succeeded)
                {
                    IdentityResult validPass = await passwordValidator.ValidateAsync(userManager, user, createUserVM.Password);
                    if (validPass.Succeeded)
                    {
                        IdentityResult result = await userManager.CreateAsync(user, createUserVM.Password);
                        if (result.Succeeded)
                        {
                            if(createUserVM.IsAdmin == true)
                            await userManager.AddToRoleAsync(user, "Admins");
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                else
                {
                    AddErrorsFromResult(validName);
                }
            }
            return View(createUserVM);
        }
        public async Task<IActionResult> EditUser(string Id)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                bool IsAdmin = await userManager.IsInRoleAsync(user, "Admins");
                EditUserVM editUserVM = new EditUserVM
                {
                    UserName = user.UserName,
                    Id = user.Id,
                    IsAdmin = IsAdmin
                };
                return View(editUserVM);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM editUserVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByIdAsync(editUserVM.Id);
                if (user != null)
                {
                    user.UserName = editUserVM.UserName;
                    IdentityResult validName =  await userValidator.ValidateAsync(userManager, user);
                    if (validName.Succeeded)
                    {
                        IdentityResult validPass = await passwordValidator.ValidateAsync(userManager, user, editUserVM.Password ?? "");
                        if (!string.IsNullOrEmpty(editUserVM.Password))
                        {
                            
                            if (validPass.Succeeded)
                            {
                                string hashedPassword = passwordHasher.HashPassword(user, editUserVM.Password);
                                user.PasswordHash = hashedPassword;
                            }
                            else
                            {
                                AddErrorsFromResult(validPass);
                            }
                        }
                        if(string.IsNullOrEmpty(editUserVM.Password) || (validPass != null && validPass.Succeeded)) 
                        {
                            if (editUserVM.IsAdmin == true && !await userManager.IsInRoleAsync(user, "Admins"))
                            {
                                await userManager.AddToRoleAsync(user, "Admins");
                            }
                            else if (editUserVM.IsAdmin == false && await userManager.IsInRoleAsync(user, "Admins"))
                            {
                                await userManager.RemoveFromRoleAsync(user, "Admins");
                            }

                            IdentityResult result =  await userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(Index));
                            }
                            else
                            {
                                AddErrorsFromResult(result);
                            }
                        }


                    }
                    else
                    {
                        AddErrorsFromResult(validName);
                    }
                }

            }
            return View(editUserVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            await userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
