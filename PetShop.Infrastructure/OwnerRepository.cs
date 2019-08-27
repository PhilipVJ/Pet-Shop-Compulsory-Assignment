using PetShop.Core.DomainService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        public Owner CreateOwner(Owner toCreate)
        {
            List<Owner> ownerList = FakeDatabase.allOwners.ToList();
            ownerList.Add(toCreate);
            FakeDatabase.allOwners = ownerList;
            return toCreate;
        }

        public Owner DeleteOwner(Owner toDelete)
        {
            List<Owner> ownerList = FakeDatabase.allOwners.ToList();
            ownerList.Remove(toDelete);
            FakeDatabase.allOwners = ownerList;
            return toDelete;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return FakeDatabase.allOwners;
        }
        public Owner GetOwnerById(int id)
        {
            foreach (var owner in FakeDatabase.allOwners)
            {
                if(owner.Id == id)
                {
                    return owner;
                }
            }
            return null;
        }

        public Owner UpdateOwner(Owner updatedOwner)
        {
            List<Owner> allOwners = FakeDatabase.allOwners.ToList();
            Owner listOwner = null;
            foreach (var owner in allOwners)
            {
                if (owner.Id == updatedOwner.Id)
                {
                    listOwner = owner;
                    // Override all old properties
                    owner.Address = updatedOwner.Address;
                    owner.Email = updatedOwner.Email;
                    owner.FirstName = updatedOwner.FirstName;
                    owner.LastName = updatedOwner.LastName;
                    owner.PhoneNumber = updatedOwner.PhoneNumber;
                    break;
                }
            }

            return listOwner;
        }
    }
}
