using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetApplicationService : IPetApplicationService
    {
        private readonly IPetRepository _petRep;
        private readonly IOwnerRepository _ownerRep;
        public PetApplicationService(IPetRepository rep, IOwnerRepository ownerRep)
        {
            this._petRep = rep;
            this._ownerRep = ownerRep;
        }
        #region PetRelated


        public List<Pet> SearchForPetByName(string name)
        {
            List<Pet> searchResults = new List<Pet>();
            foreach (var pet in _petRep.GetAllPets())
            {
                if (pet.Name.ToLower().Contains(name.ToLower()))
                {
                    searchResults.Add(pet);
                }
            }
            return searchResults;
        }

        public void SetAndCalculatePrice(Pet pet)
        {
            if (pet.PetType == PetType.Elephant)
            {
                pet.Price = 1500;
            }
            else if(pet.PetType == PetType.Mouse)
            {
                pet.Price = 50;
            }
            else
            {
                pet.Price = 300;
            }
        }

        public List<Pet> SearchForPetByType(PetType type)
        {
            List<Pet> searchResults = new List<Pet>();
            foreach (var pet in _petRep.GetAllPets())
            {
                if (pet.PetType == type)
                {
                    searchResults.Add(pet);
                }
            }
            return searchResults;
        }

        public List<Pet> Get5CheapestPets()
        {
            List<Pet> cheapestPets = new List<Pet>();
            List<Pet> allPets = GetPets();
            allPets.Sort();
            if (allPets.Count <= 5)
            {
                return allPets;
            }
            for (int i = 0; i < 5; i++)
            {
                cheapestPets.Add(allPets[i]);
            }
            return cheapestPets;
        }
        public Pet CreatePet(string name, PetType petType, DateTime birthdate, Color color, Owner currentOwner)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidDataException("The pet needs a name");
            }
            if (birthdate > DateTime.Now || birthdate < DateTime.Now.AddYears(-150)) // No pets can be more than 150 years old
            {
                throw new InvalidDataException("Invalid birth date");
            }

            if (currentOwner == null)
            {
                throw new InvalidDataException("Owner cannot be null");
            }

            int id = GetNextAvailablePetId();
            Pet newPet = new Pet(id, name, petType, birthdate, color, currentOwner);
            SetAndCalculatePrice(newPet);
            return _petRep.CreatePet(newPet);
        }

        public int GetNextAvailablePetId()
        {
            int highestId = 0;
            foreach (var pet in _petRep.GetAllPets())
            {
                if (pet.Id > highestId)
                {
                    highestId = pet.Id;
                }
            }
            return ++highestId;
        }

        public Pet DeletePet(Pet toDelete)
        {
            return _petRep.DeletePet(toDelete);
        }

        public Pet GetPetById(int id)
        {
            return _petRep.GetPetById(id);
        }

        public List<Pet> GetPets()
        {
            return _petRep.GetAllPets().ToList();
        }

        public Pet UpdatePet(Pet updatedPet)
        {
            if (string.IsNullOrEmpty(updatedPet.Name))
            {
                throw new InvalidDataException("The pet needs a name");
            }
            if (updatedPet.Birthdate > DateTime.Now || updatedPet.Birthdate < DateTime.Now.AddYears(-150)) // No pets can be more than 150 years old
            {
                throw new InvalidDataException("Invalid birth date");
            }

            if (updatedPet.CurrentOwner == null)
            {
                throw new InvalidDataException("Owner cannot be null");
            }
            if(updatedPet.SoldDate>DateTime.Now)
            {
                throw new InvalidDataException("A sold date can't be in the future");
            }
            if(updatedPet.Price<0)
            {
                throw new InvalidDataException("A price can't be less than 0");
            }

            return _petRep.UpdatePet(updatedPet);
        }
        #endregion
        #region OwnerRelated
        public int GetNextAvailableOwnerId()
        {
            int highestId = 0;
            foreach (var owner in _ownerRep.GetAllOwners())
            {
                if (owner.Id > highestId)
                {
                    highestId = owner.Id;
                }
            }
            return ++highestId;
        }

        public Owner DeleteOwner(Owner toDelete)
        {
            // Delete all pets of the owner
            List<Pet> allPets = _petRep.GetAllPets().ToList();
            foreach (var pet in allPets)
            {
                if (pet.CurrentOwner.Id == toDelete.Id)
                {
                    _petRep.DeletePet(pet);
                }
            }
            // Delete the owner
            return _ownerRep.DeleteOwner(toDelete);
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            if (string.IsNullOrEmpty(updatedOwner.FirstName))
            {
                throw new InvalidDataException("An owner must have a first name");
            }
            if (string.IsNullOrEmpty(updatedOwner.LastName))
            {
                throw new InvalidDataException("An owner must have a last name");
            }
            if (updatedOwner.PhoneNumber.ToString().Length != 8)
            {
                throw new InvalidDataException("The number must have 8 digits");
            }
            if(!string.IsNullOrEmpty(updatedOwner.Email) && !updatedOwner.Email.Contains("@"))
            {
                throw new InvalidDataException("If you add an e-mail it needs the @ sign"); 
            }
            return _ownerRep.UpdateOwner(updatedOwner);
        }

        public Owner CreateOwner(string firstName, string lastName, int phoneNumber)
        {
            if(string.IsNullOrEmpty(firstName))
            {
                throw new InvalidDataException("An owner must have a first name");
            }
            if(string.IsNullOrEmpty(lastName))
            {
                throw new InvalidDataException("An owner must have a last name");
            }
            if(phoneNumber.ToString().Length!=8)
            {
                throw new InvalidDataException("The number must have 8 digits");
            }

            Owner toAdd = new Owner(GetNextAvailableOwnerId(), firstName, lastName, phoneNumber);
            return _ownerRep.CreateOwner(toAdd);
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRep.GetAllOwners().ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _ownerRep.GetOwnerById(id);
        }

        #endregion
    }
}
