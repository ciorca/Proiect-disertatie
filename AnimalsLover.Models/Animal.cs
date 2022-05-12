using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace WLoveAnimals.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"),
            MinLength(3, ErrorMessage = "Name should contain at least 3 characters")]
        public string Name { get; set; }

        public string PhotoPath { get; set; }

    
        public Categorie? Categorie { get; set; }
        [Required]

        public string Varsta { get; set; }

    }

}
