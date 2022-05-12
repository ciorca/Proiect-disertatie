using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using WLoveAnimals.Models;

namespace WLoveAnimals.Services
{
   
    public interface IAnimalsRepository
    {
      
        IEnumerable<Animal> Search(string searchTerm);
        IEnumerable<Animal> GetAllAnimals();
        Animal GetAnimal(int id);

        [Authorize]
        Animal Update(Animal updatedAnimal);

        Animal Add(Animal newAnimal);

        [Authorize]
        Animal Delete(int id);
        
    }
}
