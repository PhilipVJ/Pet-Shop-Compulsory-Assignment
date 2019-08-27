using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetApplicationService
    {
        Pet CreatePet(string name, PetType petType, DateTime birthdate, Color color, Owner currentOwner);
        List<Pet> GetPets();
        Pet UpdatePet(Pet updatedPet);
        Pet DeletePet(Pet toDelete);
        Pet GetPetById(int id);
        int GetNextAvailablePetId();
        int GetNextAvailableOwnerId();
        Owner DeleteOwner(Owner toDelete);
        Owner UpdateOwner(Owner updatedOwner);
        Owner CreateOwner(string firstName, string lastName, int phoneNumber);
        List<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        List<Pet> SearchForPetByName(String name);
        List<Pet> SearchForPetByType(PetType type);
        List<Pet> Get5CheapestPets();
        void SetAndCalculatePrice(Pet pet);
    }
}
