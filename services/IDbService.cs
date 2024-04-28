using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD4.models;

namespace APBD4.services
{
    public interface IDbService
    {
        public List<Animal> GetAnimal(string orderBy);
        void UpdateAnimal(string idAnimal, Animal animal);
        void DeleteAnimal(string idAnimal);
        void AddAnimal(Animal animal);     
    }
}
