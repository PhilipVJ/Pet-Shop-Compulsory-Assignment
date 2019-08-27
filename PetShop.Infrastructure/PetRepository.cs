using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        public Pet CreatePet(Pet toCreate)
        {
            List<Pet> allPets = FakeDatabase.allPets.ToList();
            allPets.Add(toCreate);
            FakeDatabase.allPets = allPets;
            return toCreate;
        }

        public Pet DeletePet(Pet toDelete)
        {
            List<Pet> allPets = FakeDatabase.allPets.ToList();
            allPets.Remove(toDelete);
            FakeDatabase.allPets = allPets;
            return toDelete;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return FakeDatabase.allPets;
        }

        public Pet GetPetById(int id)
        {
            foreach (var item in FakeDatabase.allPets)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            List<Pet> allPets = FakeDatabase.allPets.ToList();
            Pet oldPet = null;
            foreach (var pet in allPets)
            {
                if (pet.Id == updatedPet.Id)
                {
                    oldPet = pet;
                    break;
                }
            }
            allPets.Remove(oldPet);
            allPets.Add(updatedPet);
            FakeDatabase.allPets = allPets; 
            return updatedPet;
        }
    }
}
