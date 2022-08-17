using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAppConsole // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private static string? name;

        static void Main(string[] args)
        {
            string option = "0";

            Console.WriteLine("Hello in student logbook");

            name = FirstNameWrite();
            if (WorkOnMemoryOrFile())
            {
                try
                {
                    var kid_WorkOnFile = new KidSaved(name);
                    while (option != "9")
                    {
                        option = MainMenuView(name);
                        Operation(option, kid_WorkOnFile);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                var kid_WorkInMemory = new KidInMemory(name);
                while (option != "9")
                {
                    option = MainMenuView(name);
                    Operation(option, kid_WorkInMemory);
                }
            }
        }

        private static string FirstNameWrite()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Write your name: ");
                    string? name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name) || !name.All(Char.IsLetter))
                    {
                        throw new ArgumentException("Invalid Name, Can use only letters");
                    }
                    Console.WriteLine($"Your name is: {name}");
                    return name;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static bool WorkOnMemoryOrFile()
        {
            while (true)
            {
                Console.WriteLine("Do you want to work on file or memory?");
                Console.WriteLine("1. File");
                Console.WriteLine("2. Memory");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        return true;

                    case "2":
                        return false;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static string MainMenuView(string name)
        {
            Console.WriteLine($"-------{name}-------");
            Console.WriteLine("1. Add grade");
            Console.WriteLine("2. Delete grade");
            Console.WriteLine("3. Show grades");
            Console.WriteLine("4. Get statistics");
            Console.WriteLine("5. Change name");
            Console.WriteLine("9. Exit");
            Console.WriteLine("What do you want?");
            var option = Console.ReadLine();
            Console.WriteLine("");
            return option;
        }

        private static void Operation(string option, IKidBase kid)
        {
            string? input = null;
            switch (option)
            {
                case "1":
                    Console.WriteLine("Write a grade (1-6, can use +/-): ");
                    input = Console.ReadLine();
                    try
                    {
                        kid.AddGrade(input);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "2":
                    Console.WriteLine("Write index of grade to remove");
                    try
                    {
                        kid.ShowGrades();
                        input = Console.ReadLine();
                        kid.RemoveGrade(input);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "3":
                    Console.WriteLine($"{kid.Name} grades:");
                    try
                    {
                        kid.ShowGrades();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "4":
                    try
                    {
                        var stats = kid.GetStatistics();
                        Console.WriteLine($"Grade in letter: {stats.Letter}");
                        Console.WriteLine($"The lowest grade: {stats.Low}");
                        Console.WriteLine($"The highest grade: {stats.High}");
                        Console.WriteLine($"The average: {string.Format("{0:0.00}", stats.Average)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "5":
                    Console.WriteLine("Write a new name (only letters)");
                    input = Console.ReadLine();
                    try
                    {
                        kid.ChangeName(input);
                        name = input;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case "9":
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
}