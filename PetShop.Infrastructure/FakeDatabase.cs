using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class FakeDatabase
    {
        public static IEnumerable<Pet> allPets;
        public static IEnumerable<Owner> allOwners;

        public static void InitDatabase()
        {
            List<Owner> ownerList = new List<Owner>();
            Owner karl = new Owner(1, "Karl", "Karlsen", 23324543);
            Owner jens = new Owner(2, "Jens", "Jensen", 60492817);
            ownerList.Add(karl);
            ownerList.Add(jens);
            allOwners = ownerList;

            List<Pet> petList = new List<Pet>();
            // Add cat
            Pet cat = new Pet(1, "Charles", PetType.Cat, DateTime.Now.AddDays(-300), Color.Black, karl)
            {
                Price = 900,
                SoldDate = DateTime.Now.AddDays(-3)
            };
            petList.Add(cat);
            // Add dog
            Pet dog = new Pet(2, "Brian", PetType.Dog, DateTime.Now.AddDays(-900), Color.Red, jens)
            {
                Price = 300,
                SoldDate = DateTime.Now.AddDays(-6)
            };
            petList.Add(dog);
            // Add snake
            Pet snake = new Pet(2, "Jason", PetType.Snake, DateTime.Now.AddDays(-800), Color.Blue, jens)
            {
                Price = 500
            };
            petList.Add(snake);
            allPets = petList;
        }


    }
}
