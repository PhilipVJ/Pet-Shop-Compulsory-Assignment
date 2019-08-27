using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        Owner DeleteOwner(Owner toDelete);
        Owner UpdateOwner(Owner updatedOwner);
        Owner CreateOwner(Owner toCreate);
        IEnumerable<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
    }
}
