using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShop.UI.ConsoleApp
{
    public class Utility
    {
        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month)) 
                             .Select(day => new DateTime(year, month, day))
                             .ToList();
        }
        public static DateTime MakeSoldDateObject(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        public static DateTime CreateBirthDateObject(int year, int month)
        {
            return new DateTime(year, month, 1);
        }
        
    }
}
