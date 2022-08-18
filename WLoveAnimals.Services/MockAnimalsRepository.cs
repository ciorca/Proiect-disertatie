using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLoveAnimals.Models;

namespace WLoveAnimals.Services
{
    [Authorize]
    public class MockAnimalsRepository : IAnimalsRepository
    {
        private List<Animal> _animalList;
        public MockAnimalsRepository()
        {
            _animalList = new List<Animal>()
            {
                new Animal() { Id = 1, Name = "Missy", Categorie = Categorie.Cats,Varsta="two months", PhotoPath = "missy.jpg" },
                new Animal() { Id = 2, Name = "Mister", Categorie = Categorie.Cats,Varsta="two months and one week", PhotoPath = "mister.jpg" },
                new Animal() { Id = 3, Name = "Blacky", Categorie = Categorie.Dogs, Varsta="three months",PhotoPath = "blacky.jpg" },
                new Animal() { Id = 4, Name = "Asis", Categorie = Categorie.Dogs,Varsta="two years" },
                };

        }

        public Animal Add(Animal newAnimal)
        {
            newAnimal.Id = _animalList.Max(e => e.Id) + 1;
            _animalList.Add(newAnimal);
            return newAnimal;

        }
        public Animal Delete(int id)
        {
            Animal animalToDelete = _animalList.FirstOrDefault(e => e.Id == id);
            if (animalToDelete != null)
            {
                _animalList.Remove(animalToDelete);
            }
            return animalToDelete;

        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return _animalList;
        }
        public Animal GetAnimal(int id)  //returns a certain id.
        {
            return _animalList.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Animal> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _animalList;
            }
            return _animalList.Where(e => e.Name.Contains(searchTerm) ||
                                            e.Varsta.Contains(searchTerm));
        }

        public Animal Update(Animal updatedAnimal)
        {
            Animal animal = _animalList.FirstOrDefault(e => e.Id == updatedAnimal.Id);
            if (animal != null)
            {
                animal.Name = updatedAnimal.Name;
                animal.Varsta = updatedAnimal.Varsta;
                animal.Categorie = updatedAnimal.Categorie;
                animal.PhotoPath = updatedAnimal.PhotoPath;
            }
            return animal;
        }
    }
}

