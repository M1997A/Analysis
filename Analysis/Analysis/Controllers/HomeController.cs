using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Analysis.Models.Repositories;
using Rotativa.AspNetCore;
using Analysis.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Analysis.Infrastructure;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analysis.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private IResultsRepository resultsRepository;
        public HomeController(IResultsRepository results, UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            
            resultsRepository = results;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View("AboutUs");
        }
        public IActionResult AnalyticalWarnings()
        {
            return View();
        }
        public IActionResult AnalysisLibrary()
        {
            return View();
        }
        public IActionResult ClientResult()
        {
            return View();
        }
        public IActionResult ShowResult(long ClientCode)
        {
            bool checkResult = resultsRepository.CheckClientResult(ClientCode);
            if (!checkResult)
            {
                TempData["CodeMessage"] = "Sorry deer customer your Analysis not finished yet";
                return RedirectToAction(nameof(ClientResult));
            }

            var result = resultsRepository.GetClientResult(ClientCode);
            if(result.Count() == 0)
            {
                TempData["CodeMessage"] = "please enter a valid code";
                return RedirectToAction(nameof(ClientResult));
            }
            return new ViewAsPdf("ShowResult",result);
        }
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginVM.UserName);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =  await signInManager.PasswordSignInAsync
                        (user,loginVM.Password ,false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
