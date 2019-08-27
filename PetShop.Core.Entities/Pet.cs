using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public class Pet : IComparable<Pet>
    {
        public int Id { get; }
        public String Name { get; set; }
        public PetType PetType { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime SoldDate { get; set; }
        public Color Color { get; set; }
        public Owner CurrentOwner { get; set; }
        public double Price { get; set; }

        public Pet(int id, string name, PetType petType, DateTime birthdate, Color color, Owner currentOwner)
        {
            Id = id;
            Name = name;
            PetType = petType;
            Birthdate = birthdate;
            Color = color;
            CurrentOwner = currentOwner;
        }

        public int CompareTo(Pet other)
        {
            if (this.Price > other.Price)
            {
                return 1;
            }
            else if (this.Price == other.Price)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
