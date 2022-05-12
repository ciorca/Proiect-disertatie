using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WLoveAnimals.Models;
using WLoveAnimals.Services;

namespace WLoveAnimals.Pages.Animals
{
    public class AdoptaModel : PageModel
    {
    
        private readonly IAnimalsRepository animalsRepository;
        public IEnumerable<Animal> Animals { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public string Mesaj { get; set; }
        public AdoptaModel(IAnimalsRepository animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }
        public void OnGet()
        {
            Animals = animalsRepository.Search(SearchTerm);
        }
    }
}