using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WLoveAnimals.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }  //creeam modelul paginii
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page(); 
           
            if(Credential.UserName == "admin" && Credential.Password == "password")  //verificam credentialul si stabilm userul si parola
            {
                
                var claims = new List<Claim>   //instantiem o lista de claims
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    //new Claim(ClaimTypes.Email, "admin@wloveanimals.com"),
                    new Claim("Admin","true"),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth"); //declaram si instantiem identitatea
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

               await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal); //legatura cu AddAuthentication services din Startup
                
                return RedirectToPage("/Index");
            }
            return Page();

        }
    }
    public class Credential
    {
        [Required]  //adaugam validare
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
}
