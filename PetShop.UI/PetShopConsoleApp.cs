using PetShop.Core.ApplicationService;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PetShop.UI.ConsoleApp
{
    public class PetShopConsoleApp : IPetShopConsoleApp

    {
        private readonly IPetApplicationService _service;
        private readonly String CreateCustomerKeyWord = "NEW";
        private ConsoleColor _curTextColor = ConsoleColor.Blue;

        public PetShopConsoleApp(IPetApplicationService service)
        {
            this._service = service;
        }
        public void StartProgram()
        {
            ShowLoadingGraphics();
            ShowMenu();
        }
        #region Menu

        public void ShowMenu()
        {
            bool closed = false;
            while (!closed)
            {
                String welcome = "Pet Shop 1.2";
                Console.WriteLine(welcome.PadLeft(9 + welcome.Length, ' '));
                Console.WriteLine("------------------------------");
                Console.WriteLine("1. See all pets");
                Console.WriteLine("2. Add new pet");
                Console.WriteLine("3. Delete pet");
                Console.WriteLine("4. Update pet");
                Console.WriteLine("5. Search for pet by name");
                Console.WriteLine("6. Search for pet by type");
                Console.WriteLine("7. Manage owners");
                Console.WriteLine("8. Exit");
                Console.WriteLine("------------------------------");

                int input;
                while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 8)
                {
                    Console.WriteLine("Not a valid input. Try again");
                }
                Console.Clear();

                switch (input)
                {
                    case 1:
                        ShowAllPets();
                        break;
                    case 2:
                        AddPet();
                        break;
                    case 3:
                        DeletePet();
                        break;
                    case 4:
                        UpdatePet();
                        break;
                    case 5:
                        SearchForPetByName();
                        break;
                    case 6:
                        SearchForPetByType();
                        break;
                    case 7:
                        ManageOwners();
                        break;
                    case 8:
                        closed = true;
                        break;
                }
            }
            // Shutting down the application with a slight delay
            Console.WriteLine("Shutting down the application..");
            DateTime twoSecondsInTheFuture = DateTime.Now.AddSeconds(2);
            while (twoSecondsInTheFuture > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
        }

        private void ReturnToMenuMessage()
        {
            Console.WriteLine("Click enter to return to the main menu");
            Console.ReadLine();
            Console.Clear();
        }

        private void ShowLoadingGraphics()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            // Slide effect
            int horizontalCounter = 1;
            for (int i = 0; i < 40; i++)
            {
                for (int k = horizontalCounter; k > 0; k--)
                {
                    if (horizontalCounter - k == 30)
                    {
                        break;
                    }
                    Console.Write("1");
                    SwapTextColor();
                }
                Console.Write("\n");

                for (int n = horizontalCounter - 1; n > 0; n--)
                {
                    if (horizontalCounter - n == 10)
                    {
                        break;
                    }
                    int iterations = 0;
                    for (int y = n; y > 0; y--)
                    {
                        if (!(iterations >= 30))
                        {
                            Console.Write("1");
                            SwapTextColor();
                        }
                        iterations++;
                    }
                    Console.Write("\n");
                }
                horizontalCounter++;
                DateTime future = DateTime.Now.AddMilliseconds(10);
                while (DateTime.Now < future)
                {
                    Thread.Sleep(10);
                }
                Console.Clear();
            }

            // Up slide
            for (int m = 10; m > 0; m--)
            {

                for (int i = m; i > 0; i--)
                {
                    for (int h = 0; h < 30; h++)
                    {
                        Console.Write("1");
                        SwapTextColor();
                    }
                    Console.Write("\n");
                }


                DateTime future = DateTime.Now.AddMilliseconds(10);
                while (DateTime.Now < future)
                {
                    Thread.Sleep(10);
                }
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            //Wait half a second before showing menu
            DateTime oneSec = DateTime.Now.AddMilliseconds(500);
            while (DateTime.Now < oneSec)
            {
                Thread.Sleep(500);
            }

        }

        private void SwapTextColor()
        {
            if (_curTextColor == ConsoleColor.Red)
            {
                _curTextColor = ConsoleColor.Blue;
            }
            else
            {
                _curTextColor = ConsoleColor.Red;
            }
            Console.ForegroundColor = _curTextColor;
        }

        #endregion
        #region Owner Related

        private void ManageOwners()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Manage owners");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Delete owner");
                Console.WriteLine("2. Update owner");
                Console.WriteLine("3. Show all owners");
                Console.WriteLine("4. Go back to main manu");
                int input;
                while (!int.TryParse(Console.ReadLine(), out input) || input > 4 || input < 1)
                {
                    Console.WriteLine("Not a valid input. Try again");
                }
                Console.Clear();
                switch (input)
                {
                    case 1:
                        DeleteOwner();
                        break;
                    case 2:
                        UpdateOwner();
                        break;
                    case 3:
                        ShowAllOwners();
                        break;
                    case 4:
                        done = true;
                        break;
                    default:
                        break;
                }
            }
        }



        private void ShowAllOwners()
        {
            Console.WriteLine("All owners");
            Console.WriteLine("-----------------------------");
            List<Owner> ownerList = _service.GetAllOwners();
            ownerList.Sort();
            foreach (var owner in ownerList)
            {
                Console.WriteLine(owner.FirstName + " " + owner.LastName);
            }
            Console.WriteLine("Press enter to go back to the owner menu");
            Console.ReadLine();
            Console.Clear();
        }

        private void DeleteOwner()
        {
            Console.WriteLine("Delete owner");
            Console.WriteLine("-----------------------------");
            if (_service.GetAllOwners().Count == 0)
            {
                Console.WriteLine("No owners available");
                Console.WriteLine("Press enter to return");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Please type in the ID of the owner");
            int id;
            Owner owner;
            while (!int.TryParse(Console.ReadLine(), out id) || (owner = _service.GetOwnerById(id)) == null)
            {
                Console.WriteLine("Not a valid id. Try again.");
            }

            Console.WriteLine("{0} and all his pets have been deleted", owner.FirstName);
            _service.DeleteOwner(owner);
            Console.WriteLine("Press enter to go back to the Owner menu");
            Console.ReadLine();
            Console.Clear();

        }
        private Owner CreateOwner()
        {
            bool createdOwner = false;
            Owner owner = null;
            while (!createdOwner)
            {
                Console.WriteLine("What is the first name of the owner?");
                String name;
                while (string.IsNullOrEmpty(name = Console.ReadLine()))
                {
                    Console.WriteLine("An owner must have a first name");
                }

                Console.WriteLine("What is the last name of the owner");
                String lastName;
                while (string.IsNullOrEmpty(lastName = Console.ReadLine()))
                {
                    Console.WriteLine("An owner must have a last name");
                }
                int number;
                Console.WriteLine("What is the owners phone number?");
                while (!int.TryParse(Console.ReadLine(), out number) || number.ToString().Length != 8)
                {
                    Console.WriteLine("Not a valid phone number");
                }

                try
                {
                    owner = _service.CreateOwner(name, lastName, number);
                    createdOwner = true;
                }
                catch (InvalidDataException x)
                {
                    // Should not happen due to input check. If it does - a message will be shown
                    Console.WriteLine("An error occured");
                    Console.WriteLine(x.Message);
                    Console.WriteLine("Press enter to try again");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            return owner;

        }
        #endregion
        #region Pet Related


        private void SearchForPetByType()
        {
            Console.WriteLine("Search for pet");
            Console.WriteLine("-----------------------------");

            Console.WriteLine("Type in the type of the pet");
            PetType chosenType;
            String written;
            while (!Enum.TryParse<PetType>(written = Console.ReadLine(), out chosenType) || int.TryParse(written, out int numberOut))
            {
                Console.WriteLine("Not a valid type. Try again");
            }
            List<Pet> results = _service.SearchForPetByType(chosenType);
            ShowSearchResults(results);
            ReturnToMenuMessage();
        }

        private void SearchForPetByName()
        {
            Console.WriteLine("Search for pet");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Type in the name of the pet");
            String name = Console.ReadLine();
            List<Pet> results = _service.SearchForPetByName(name);
            ShowSearchResults(results);
            ReturnToMenuMessage();
        }

        private void ShowSearchResults(List<Pet> searchResults)
        {
            if (searchResults.Count != 0)
            {
                Console.WriteLine("Results:");
                foreach (var pet in searchResults)
                {
                    Console.WriteLine(pet.Name + " the " + pet.PetType);
                }
            }
            else
            {
                Console.WriteLine("No pets found");
            }
        }

        private void AddPet()
        {
            Console.WriteLine("Add new pet");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("What kind of animal is the pet?");

            PetType chosenType;
            String written;
            while (!Enum.TryParse<PetType>(written = Console.ReadLine(), out chosenType) || int.TryParse(written, out int numberOut))
            {
                Console.WriteLine("Not a valid type. Try again");
            }

            Console.WriteLine("What is the name of the {0}?", chosenType.ToString().ToLower());
            String name;
            while ((name = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("You must enter a name. Try again");
            }


            bool validDate = false;
            DateTime birthDate = DateTime.Now; // Temp value
            while (!validDate)
            {
                Console.WriteLine("What year is the {0} born in?", chosenType.ToString().ToLower());
                int year;
                while (!int.TryParse(Console.ReadLine(), out year) || year > DateTime.Now.Year || year < DateTime.Now.AddYears(-150).Year)
                {
                    Console.WriteLine("Not a valid year. Try again");
                }

                int month;
                Console.WriteLine("What month is the {0} born in?", chosenType.ToString().ToLower());
                while (!int.TryParse(Console.ReadLine(), out month) || month > 12 || month <= 0)
                {
                    Console.WriteLine("Not a valid month. Try again");
                }

                birthDate = Utility.CreateBirthDateObject(year, month);
                if (birthDate < DateTime.Now)
                {
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("The pet can't be born in the future. Try again.");
                }
            }

            Console.WriteLine("What is the {0}s color? You can choose from: ", chosenType.ToString().ToLower());
            foreach (var color in Enum.GetValues(typeof(Color)))
            {
                Console.Write(color + "  ");
            }
            Console.Write("\n");
            Color chosenColor;
            String enumString = null;
            while (!Enum.TryParse<Color>(written = Console.ReadLine(), out chosenColor) || int.TryParse(enumString, out int numberOut))
            {
                Console.WriteLine("Not a valid color. Try again");
            }

            Console.WriteLine("What is the owners id? If he/she is a new customer, write NEW to add the owner to the database");
            int id;
            Owner owner = null;
            bool foundValidInput = false;
            while (!foundValidInput)
            {
                String input = Console.ReadLine();
                if (input.Equals(CreateCustomerKeyWord))
                {
                    foundValidInput = true;
                    owner = CreateOwner();
                    continue;
                }

                if (!int.TryParse(input, out id) || (owner = _service.GetOwnerById(id)) == null)
                {
                    Console.WriteLine("Not a valid id. Try again");
                }
                else
                {
                    foundValidInput = true;
                }
            }

            Pet addedPet = null;
            bool hasBeenAdded = false;

            try
            {
                addedPet = _service.CreatePet(name, chosenType, birthDate, chosenColor, owner);
                hasBeenAdded = true;
            }
            catch (InvalidDataException x)
            {
                // Should not happen due to input check - if it somehow happens a message will be shown
                Console.WriteLine("An error has occured");
                Console.WriteLine(x.Message);
            }

            if (hasBeenAdded)
            {
                Console.WriteLine("{1} the {0} has been added to the system", addedPet.PetType, addedPet.Name);
            }
            ReturnToMenuMessage();
        }


        private void PrintList(List<Pet> pets)
        {
            foreach (var pet in pets)
            {
                Console.WriteLine(pet.Name + " the " + pet.Color.ToString().ToLower() +" "+pet.PetType.ToString().ToLower());
                Console.WriteLine("- Price: {0}", pet.Price);
                Console.WriteLine("- Birth date: {0}-{1}", pet.Birthdate.Month, pet.Birthdate.Year);
                Console.WriteLine("- Owner name {0}", pet.CurrentOwner.FirstName);

            }
            Console.WriteLine();
        }

        private void ShowAllPets()
        {
            Console.WriteLine("All pets");
            Console.WriteLine("-----------------------------");
            List<Pet> allPets = _service.GetPets();
            if (allPets.Count == 0)
            {
                Console.WriteLine("No pets found. Press enter to go back");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            PrintList(_service.GetPets());
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Additional options:");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Show 5 cheapest pets");
                Console.WriteLine("2. Sort by price");
                Console.WriteLine("3. Go back to menu");
                int chosenOption;
                while (!int.TryParse(Console.ReadLine(), out chosenOption) || chosenOption < 1 || chosenOption > 3)
                {
                    Console.WriteLine("Not a valid input");
                }

                switch (chosenOption)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("The cheapest pets:");
                        Console.WriteLine("-----------------------------");
                        PrintList(_service.Get5CheapestPets());
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Sorted by price:");
                        Console.WriteLine("-----------------------------");
                        List<Pet> pets = _service.GetPets();
                        pets.Sort();
                        PrintList(pets);
                        break;
                    case 3:
                        done = true;
                        break;
                }
            }
            Console.Clear();
        }

        private void DeletePet()
        {
            Console.WriteLine("Delete pet");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Type in the ID of the pet you want to delete");
            int id;
            Pet foundPet = null;
            while (!int.TryParse(Console.ReadLine(), out id) || (foundPet = _service.GetPetById(id)) == null)
            {
                Console.WriteLine("Not a valid ID. Try again");
            }
            _service.DeletePet(foundPet);
            Console.WriteLine("{0} the {1} has been deleted", foundPet.Name, foundPet.PetType);
            ReturnToMenuMessage();
        }
        #endregion
        #region Update methods
        /*
         * Normally we are using copies of stored data (like objects made from data retrieved from a SQL database),
         * but in this project we have a fake database which consists of a simple List which holds references to objects.
         * Since they aren't copies, but the original data, we could easily update the objects directly here in UI. But since we are pretending
         * I am making copies of the pet/owner objects in the following two methods, which would not be done in a real life application. 
         */

        private void UpdatePet()
        {
            Console.WriteLine("Update pet");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Type in the id of the pet");

            int id = -1; // Temp value
            double price;
            DateTime soldDate = DateTime.Now; // Temp value
            Pet originalPet = null;
            Pet updatedPet = null;
            bool foundValidInput = false;
            while (!foundValidInput)
            {
                String input = Console.ReadLine();

                if (!int.TryParse(input, out id) || (originalPet = _service.GetPetById(id)) == null)
                {
                    Console.WriteLine("Not a valid id. Try again");
                }
                else
                {
                    foundValidInput = true;
                }
            }
            Console.WriteLine("{0} the {1} is located. What do you want to change?", originalPet.Name, originalPet.PetType);
            Console.WriteLine("1. Price");
            Console.WriteLine("2. Sold date");
            int menuChoice;
            String consoleMessage = "";
            while (!int.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 2)
            {
                Console.WriteLine("Not a valid menu choice. Try again");
            }
            if (menuChoice == 1)
            {
                Console.WriteLine("Type in the new price");
                while (!double.TryParse(Console.ReadLine(), out price) || price <= 0)
                {
                    Console.WriteLine("Not a valid price. Try again");
                }
                updatedPet = new Pet(id, originalPet.Name, originalPet.PetType, originalPet.Birthdate, originalPet.Color, originalPet.CurrentOwner);
                updatedPet.Price = price;
                updatedPet.SoldDate = originalPet.SoldDate;
                consoleMessage = "Price has been set to " + price;
            }
            else if (menuChoice == 2)
            {
                Console.WriteLine("What year was {0} sold?", originalPet.Name);

                bool validSoldDate = false;
                while (!validSoldDate)
                {
                    int year;
                    while (!int.TryParse(Console.ReadLine(), out year) || year > DateTime.Now.Year)
                    {
                        Console.WriteLine("Not a valid year. Try again");
                    }

                    int month;
                    Console.WriteLine("What month was {0} sold?", originalPet.Name);
                    while (!int.TryParse(Console.ReadLine(), out month) || month > 12 || month <= 0)
                    {
                        Console.WriteLine("Not a valid month. Try again");
                    }

                    int day;
                    Console.WriteLine("What day was {0} sold?", originalPet.Name);
                    while (!int.TryParse(Console.ReadLine(), out day) || day <= 0 || day > Utility.GetDates(year, month).Count)
                    {
                        Console.WriteLine("Not a valid day. Try again");
                    }
                    soldDate = Utility.MakeSoldDateObject(year, month, day);
                    if (soldDate > DateTime.Now)
                    {
                        Console.WriteLine("A sale can't happen in the future. Try again.");
                    }
                    else
                    {
                        validSoldDate = true;
                    }
                }
                updatedPet = new Pet(id, originalPet.Name, originalPet.PetType, originalPet.Birthdate, originalPet.Color, originalPet.CurrentOwner);
                updatedPet.Price = originalPet.Price;
                updatedPet.SoldDate = soldDate;
                consoleMessage = "The sold date has been set to " + soldDate.Day + "/" + soldDate.Month + "-" + soldDate.Year;
            }

            try
            {
                _service.UpdatePet(updatedPet);
                Console.WriteLine(consoleMessage);
            }
            catch (InvalidDataException x)
            {
                // Should not happen due to input checks. If it happens a message will be shown
                Console.WriteLine("An error has occured. The update has failed with the following error message:");
                Console.WriteLine(x.Message);
            }
            ReturnToMenuMessage();
        }

        private void UpdateOwner()
        {
            Console.WriteLine("Update owner");
            Console.WriteLine("-----------------------------");
            if (_service.GetAllOwners().Count == 0)
            {
                Console.WriteLine("No owners available");
                Console.WriteLine("Press enter to return");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Please type in the ID of the owner");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id) || _service.GetOwnerById(id) == null)
            {
                Console.WriteLine("Not a valid id. Try again.");
            }
            Console.WriteLine("What will the new first name be?");
            string name;
            while ((name = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("The owner must have a name");
            }
            Console.WriteLine("What will the new last name be?");
            string lastName;
            while ((lastName = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("The owner must have a last name");
            }

            int phoneNumber;
            Console.WriteLine("What is the new phone number?");
            while (!int.TryParse(Console.ReadLine(), out phoneNumber) || phoneNumber.ToString().Length != 8)
            {
                Console.WriteLine("Not a valid phone number");
            }

            Console.WriteLine("What is the new address?");
            String address = Console.ReadLine();
            string email;
            Console.WriteLine("What is the owners e-mail - if there the owner doesn't have one - press Enter ");
            while ((email = Console.ReadLine()).Length > 0 && !email.Contains("@"))
            {
                Console.WriteLine("If you type in an e-mail you must use the @ sign");
            }
            try
            {
                Owner owner = new Owner(id, name, lastName, phoneNumber);
                owner.Email = email;
                owner.Address = address;
                _service.UpdateOwner(owner);
                Console.WriteLine("The owner has been updated. Press enter to go back");
            }
            catch (InvalidDataException x)
            {
                Console.WriteLine("An error has occured with the following message");
                Console.WriteLine(x.Message);
                Console.WriteLine("The update could not happen. Press enter to go back");
            }

            Console.ReadLine();
            Console.Clear();
        }
        #endregion
    }

}


