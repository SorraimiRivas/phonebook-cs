using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;



class Program
{

    static int tableWidth = 73;
    static void Main(string[] args)
    {

        List<Contact> contacts = [];

        contacts.Add(new Contact { Name = "John", LastName = "Doe", Number = "1234567890", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Jane", LastName = "Doe", Number = "1234567123", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Michael", LastName = "Jordan", Number = "1245567890", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Hiram", LastName = "Marim", Number = "1234560099", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "George", LastName = "Bush", Number = "1234560099", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Tai", LastName = "Lopez", Number = "1234560099", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Karim", LastName = "Abad", Number = "1234560099", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Nolan", LastName = "Freeman", Number = "1234560099", Id = contacts.Count + 1 });
        contacts.Add(new Contact { Name = "Morgan", LastName = "Ferguson", Number = "1234560099", Id = contacts.Count + 1 });

        while (true)
        {

            Console.WriteLine();
            Console.WriteLine("Phonebook");
            Console.WriteLine();
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Show Contacts");
            Console.WriteLine("3. Search Contact");
            Console.WriteLine("4. Edit Contact");
            Console.WriteLine("5. Delete Contact");
            Console.WriteLine("6. Exit");
            Console.WriteLine("");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? string.Empty;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddContact(contacts);
                    break;
                case "2":
                    ShowContacts(contacts);
                    break;
                case "3":
                    SearchContact(contacts);
                    break;
                case "4":
                    EditContact(contacts);
                    break;
                case "5":
                    DeleteContact(contacts);
                    break;
                case "6":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please enter a valid option.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AddContact(List<Contact> contacts)
    {
        Console.Write("Enter the contact name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter the contact last name: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Enter contact number (10): ");
        string number = Console.ReadLine() ?? "";

        //Checks if the phone number is valid
        if (number == null || number.Length != 10)
        {
            Console.WriteLine();
            Console.WriteLine("Invalid phone number! Please enter a valid 10 digit phone number.");
            return;
        }

        if (contacts.Exists(c => c.Number == number))
        {
            Console.WriteLine("Contact with the same number already exists!");
            return;
        }

        contacts.Add(new Contact { Name = name, LastName = lastName, Number = number, Id = contacts.Count + 1 });


        Console.WriteLine("Contact added successfully!");
    }

    static void ShowContacts(List<Contact> contacts)
    {
        // convert to string formatting
        Console.Clear();
        Console.WriteLine(new string('-', tableWidth));
        PrintRow("ID", "Name", "Last name", "Phone number");
        Console.WriteLine(new string('-', tableWidth));
        Console.WriteLine(new string('-', tableWidth));

        foreach (Contact contact in contacts)
        {

            //Console.WriteLine("{0,-20} {1,5}\n", "Name", "Hours");
            PrintRow(contact.Id.ToString(), contact.Name, contact.LastName, contact.Number);

        }
        PrintLine();

    }

    static void PrintLine()
    {
        Console.WriteLine(new string('-', tableWidth));
    }
    static void PrintRow(params string[] columns)
    {
        int width = (tableWidth - columns.Length) / columns.Length;
        string row = "|";

        foreach (string column in columns)
        {
            row += AlignCenter(column, width) + "|";
        }

        Console.WriteLine(row);
    }

    static string AlignCenter(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
    static void DeleteContact(List<Contact> contacts)
    {
        ShowContacts(contacts);
        Console.WriteLine("");
        Console.Write("Enter the id of the contact you want to delete: ");
        string id = Console.ReadLine() ?? "";

        var contactToDelete = contacts.Find(c => c.Id.ToString() == id);

        if (contactToDelete is null)
        {
            Console.WriteLine("Invalid ID! Please enter a valid ID.");
            return;
        }

        contacts.Remove(contactToDelete);

        Console.WriteLine("Contact has been deleted succesfully!");
    }

    static void SearchContact(List<Contact> contacts)
    {

        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found!");
            return;
        }

        Console.Write("Enter contact name to search: ");
        string name = Console.ReadLine() ?? "";

        var searchResults = contacts.FindAll(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));

        if (searchResults.Count == 0)
        {
            Console.WriteLine("No contacts found!");
            return;
        }

        Console.WriteLine("Search results:");
        foreach (Contact contact in searchResults)
        {
            Console.Clear();
            PrintLine();
            PrintRow("ID", "Name", "Last name", "Phone number");
            PrintLine();

            PrintRow(contact.Id.ToString(), contact.Name, contact.LastName, contact.Number);
        }
        PrintLine();
    }

    static void EditContact(List<Contact> contacts)
    {
        while (true)
        {
            Console.WriteLine("1. Edit contact name");
            Console.WriteLine("2. Edit contact last name");
            Console.WriteLine("3. Edit contact number");
            Console.WriteLine("4. Go back");
            Console.WriteLine("");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    EditName(contacts);
                    break;

                case "2":
                    EditLastName(contacts);
                    break;
                case "3":
                    EditNumber(contacts);
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice! Please enter a valid option.");
                    break;
            }

            static void EditName(List<Contact> contacts)
            {
                ShowContacts(contacts);
                Console.WriteLine("");
                Console.Write("Enter the id of the contact you want to edit: ");
                string id = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new name: ");
                string newName = Console.ReadLine() ?? "";
                Console.WriteLine();

                var nameToChange = contacts.Find(c => c.Id.ToString() == id);

                if (nameToChange is null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                var prevValue = nameToChange.Number;

                nameToChange.Number = newName;

                System.Console.WriteLine($"{prevValue} is now {newName}");
                PrintRow("");
            };

            static void EditLastName(List<Contact> contacts)
            {
                ShowContacts(contacts);
                Console.WriteLine("");
                Console.Write("Enter the id of the contact you want to edit: ");
                string id = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new last name: ");
                string newLastName = Console.ReadLine() ?? "";

                var lastNameToChange = contacts.Find(c => c.Id.ToString() == id);

                if (lastNameToChange is null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                var prevValue = lastNameToChange.LastName;

                lastNameToChange.LastName = newLastName;

                Console.WriteLine($"{prevValue} is now {newLastName}");
            };

            static void EditNumber(List<Contact> contacts)
            {
                ShowContacts(contacts);
                Console.WriteLine("");
                Console.Write("Enter the id of the contact you want to edit: ");
                string id = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new number: ");
                string newNumber = Console.ReadLine() ?? "";

                var numberToChange = contacts.Find(c => c.Id.ToString() == id);

                if (numberToChange is null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                var prevValue = numberToChange.Number;

                numberToChange.Number = newNumber;

                System.Console.WriteLine($"{prevValue} is now {newNumber}");
            };
        }
    }
}