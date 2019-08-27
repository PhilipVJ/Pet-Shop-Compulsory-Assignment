using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entities
{
    public class Owner : IComparable<Owner>
    {
        public int Id { get; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public int PhoneNumber { get; set; }
        public String Email { get; set; }

        public Owner(int id, string firstName, string lastName, int phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }


        public int CompareTo(Owner other)
        {
            if (this.Id == other.Id)
            {
                return 0;
            }
            else if (this.Id > other.Id)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
