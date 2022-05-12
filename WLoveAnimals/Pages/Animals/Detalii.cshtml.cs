using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WLoveAnimals.Models;
using WLoveAnimals.Services;

namespace WLoveAnimals.Pages.Animals
{
    public class DetaliiModel : PageModel
    {
        private readonly IAnimalsRepository animalsRepository;
        public DetaliiModel(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }
        [TempData]
        public string Mesaj { get; set; }

        public Animal Animal { get; private set; }

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
            
            return RedirectToPage("Index");

        }
    }
}
