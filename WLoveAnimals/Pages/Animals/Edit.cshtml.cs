using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WLoveAnimals.Models;
using WLoveAnimals.Services;

namespace WLoveAnimals.Pages.Animals
{
    [Authorize]

    public class EditModel : PageModel
    {
        private readonly IAnimalsRepository animalsRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EditModel(IAnimalsRepository animalsRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.animalsRepository = animalsRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public Animal Animal { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notificare { get; set; }
        public string Mesaj { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Animal = animalsRepository.GetAnimal(id.Value);
            }
            else
            {
                Animal = new Animal();
            }

            if (Animal == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()  //code to update animal data
        {
            if (ModelState.IsValid)
            {


                if (Photo != null)
                {
                    if (Animal.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", Animal.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    Animal.PhotoPath = ProcessUploadedFile();
                }
                if (Animal.Id > 0)
                {
                    Animal = animalsRepository.Update(Animal);
                }
                else
                {
                    Animal = animalsRepository.Add(Animal);
                }

                return RedirectToPage("Index");
            }
            return Page();
        }


        /*public IActionResult OnPostUpdatePreferintaNotificare(int id) //Code to update preferinta notificare
        {
            if (Notificare)
            {
                Mesaj = "Thank you for turning on notifications";
            }
            else
            {
                Mesaj = "You have turned off email notifications";
            }
            TempData["mesaj"] = Mesaj;

            return RedirectToPage("Detalii", new { id });
        }*/

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))

                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

