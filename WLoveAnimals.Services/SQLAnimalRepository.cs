using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WLoveAnimals.Models;

namespace WLoveAnimals.Services
{
   
    public class SQLAnimalRepository : IAnimalsRepository
    {
      
        private readonly AppDbContext context;
        public SQLAnimalRepository(AppDbContext context)
        {
            this.context = context;
        }

       
        public Animal Add(Animal newAnimal)
        {
            context.Database.ExecuteSqlRaw("spInsertAnimal {0}, {1}, {2}, {3} , {4}",
                                   newAnimal.Name,
                                   newAnimal.PhotoPath,
                                   newAnimal.Categorie,
                                   newAnimal.Varsta,
                                   newAnimal.Oras
                                   );
            return newAnimal;
        }

        public Animal Delete(int id)
        {
            Animal animal = context.Animals.Find(id);
            if (animal != null)
            {
                context.Animals.Remove(animal);
                context.SaveChanges();

            }
            return animal;
        }

        public IEnumerable<Animal> GetAllAnimals()
        {
            return context.Animals
                       .FromSqlRaw<Animal>("SELECT * FROM Animals")
                       .ToList();
                        
        }

        public Animal GetAnimal(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);
            return context.Animals
                    .FromSqlRaw<Animal>("spGetAnimalById @Id", parameter)
                    .ToList()
                    .FirstOrDefault();
        }

        public IEnumerable<Animal> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Animals;
            }
            return context.Animals.Where(e => e.Name.Contains(searchTerm) ||
                                            e.Varsta.Contains(searchTerm) || e.Oras.Contains(searchTerm));
        }

        public Animal Update(Animal updatedAnimal)
        {
            var animal = context.Animals.Attach(updatedAnimal);
            animal.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedAnimal;
        }
    }
}
