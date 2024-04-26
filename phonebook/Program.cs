using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, string> contacts = new Dictionary<string, string>();

        while (true)
        {
            Console.WriteLine("1. Add Contact");
            if (contacts.Count > 0)
            {
                Console.WriteLine("2. Search Contact");
            }
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddContact(contacts);
                    break;

                case "2":
                    SearchContact(contacts);
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice! Please enter a valid option.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AddContact(Dictionary<string, string> contacts)
    {
        Console.Write("Enter the contact name: ");
        string name = Console.ReadLine() ?? "";

        //Checks if the contact already exists
        if (contacts.ContainsKey(name))
        {
            Console.WriteLine("Contact already exists!");
            return;
        }

        Console.Write("Enter contact number (10): ");
        string number = Console.ReadLine() ?? "";

        //Checks if the phone number is valid
        if (number == null || number.Length != 10)
        {
          Console.WriteLine("Invalid phone number! Please enter a valid 10 digit phone number.");
          return;
        }

        if (contacts.ContainsValue(number))
        {
          Console.WriteLine("Phone number already belongs to { }!", contacts.FirstOrDefault(x => x.Value == number).Key);
          return;
        }

        contacts[name] = number;
        Console.WriteLine("Contact added successfully!");
    }

    static void SearchContact(Dictionary<string, string> contacts)
    {

        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found!");
            return;
        }
        
        Console.Write("Enter contact name to search: ");
        string? searchName = Console.ReadLine();
        if (contacts.ContainsKey(searchName!))
        {
          int namePadding = Math.Max(15, searchName!.Length);
          int numberPadding = Math.Max(16, contacts[searchName].Length);

          Console.WriteLine(namePadding);

          Console.WriteLine("+" + new string('-', namePadding + 2) + "+" + new string('-', numberPadding + 2) + "+");
          Console.WriteLine("|" + "Name".PadRight(namePadding) + "|" + "Phone".PadRight(numberPadding) + "|");
          Console.WriteLine("+" + new string('-', namePadding + 2) + "+" + new string('-', numberPadding + 2) + "+");
          Console.WriteLine(String.Format("| {0,-" + namePadding + "} | {1,-" + numberPadding + "} |", searchName, contacts[searchName]));
          Console.WriteLine("+" + new string('-', namePadding + 2) + "+" + new string('-', numberPadding + 2) + "+");
        }
        else
        {
          Console.WriteLine("Contact not found!");
        }
    }
}
