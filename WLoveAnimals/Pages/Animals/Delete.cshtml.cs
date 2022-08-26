using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WLoveAnimals.Models;
using WLoveAnimals.Services;

namespace WLoveAnimals.Pages.Animals
{
    //[Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAnimalsRepository animalsRepository;

        public DeleteModel(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }
        [BindProperty]
        public Animal Animal { get; set; }
        public IActionResult OnGet(int id)
        {
            Animal = animalsRepository.GetAnimal(id);
            if (Animal == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            Animal deletedAnimal = animalsRepository.Delete(Animal.Id);
            if (deletedAnimal == null)
            {
                return RedirectToPage("/NotFound");
            }
            return RedirectToPage("Index");

        }
    }
}

