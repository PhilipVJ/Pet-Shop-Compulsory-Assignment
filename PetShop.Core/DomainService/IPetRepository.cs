using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        Pet DeletePet(Pet toDelete);
        Pet UpdatePet(Pet updatedPet);
        Pet CreatePet(Pet toCreate);
        IEnumerable<Pet> GetAllPets();
        Pet GetPetById(int id);
    }
}
