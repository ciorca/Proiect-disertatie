using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WLoveAnimals.Pages.Account
{
   
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        public LogoutModel(SignInManager<IdentityUser> signInManager )
        {
            this.signInManager = signInManager;
        }
        //public async Task<IActionResult>OnPostAsync()
        //{
        //   await  HttpContext.SignOutAsync("MyCookieAuth");
        //    return RedirectToPage("/Index");
        //}
        public async Task<IActionResult>OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Animals/Adopta");

        }
        public  IActionResult OnPostDontLogoutAsync()
        {
            
            return RedirectToPage("/Animals/Index");

        }
    }
}
