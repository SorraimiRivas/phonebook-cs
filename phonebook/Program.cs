using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{

    class Contact
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public int Id { get; set; }

        public Contact(string name, string lastName, string number, int id)
        {
            Name = name;
            LastName = lastName;
            Number = number;
            Id = id;
        }
    }

    static void Main(string[] args)
    {

        List<Contact> contacts = [];

        while (true)
        {
            contacts.Add(new Contact("John", "Doe", "1234567890", contacts.Count + 1));
            contacts.Add(new Contact("Jane", "Doe", "1234567123", contacts.Count + 1));
            contacts.Add(new Contact("Michael", "Jordan", "1245567890", contacts.Count + 1));
            contacts.Add(new Contact("Hiram", "Marim", "1234560099", contacts.Count + 1));
            contacts.Add(new Contact("George", "Bush", "1234560099", contacts.Count + 1));
            contacts.Add(new Contact("Tai", "Lopez", "1234560099", contacts.Count + 1));
            contacts.Add(new Contact("Karim", "Abad", "1234560099", contacts.Count + 1));
            contacts.Add(new Contact("Nolan", "Freeman", "1234560099", contacts.Count + 1));

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
            string choice = Console.ReadLine() ?? "";
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

        contacts.Add(new Contact(name, lastName, number, contacts.Count + 1));

        Console.WriteLine("Contact added successfully!");
    }

    static void ShowContacts(List<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts found!");
            return;
        }

        Console.WriteLine("Name | Last Name | Number | ID");
        Console.WriteLine();
        foreach (Contact contact in contacts)
        {
            Console.WriteLine($"{contact.Name} | {contact.LastName} | {contact.Number} | {contact.Id}");
        }
    }

    static void DeleteContact(List<Contact> contacts)
    {
        ShowContacts(contacts);
        Console.WriteLine("");
        Console.Write("Enter the id of the contact you want to delete: ");
        string id = Console.ReadLine() ?? "";


        foreach (Contact contact in contacts)
        {

            if (id == "" || id == null)
            {
                Console.WriteLine("Invalid ID! Please enter a valid ID.");
                break;
            }

            if (contacts.Find(c => c.Id.ToString() == id) == null)
            {
                Console.WriteLine($"Contact with ID {id} does not exist!");
                break;
            }

            if (id == contact.Id.ToString())
            {
                Console.WriteLine("");
                string deletedContact = $"{contact.Name} {contact.LastName}";
                contacts.Remove(contact);
                Console.WriteLine($"{deletedContact} deleted successfully!");
                break;
            }
        }
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

        List<Contact> searchResults = contacts.FindAll(c => c.Name.ToLower().Contains(name.ToLower()));

        if (searchResults.Count == 0)
        {
            Console.WriteLine("No contacts found!");
            return;
        }

        Console.WriteLine("Search results:");
        foreach (Contact contact in searchResults)
        {
            Console.WriteLine($"Name: {contact.Name} {contact.LastName}, Number: {contact.Number}");
        }
    }

    static void EditContact(List<Contact> contacts)
    {
        var loopOn = true;

        while (loopOn)
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

                if (id == "" || id == null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new name: ");
                string newName = Console.ReadLine() ?? "";
                Console.WriteLine();

                foreach (Contact contact in contacts)
                {
                    if (id == contact.Id.ToString())
                    {
                        string oldName = contact.Name;

                        contact.Name = newName;
                        Console.WriteLine($"{oldName} is now {newName}!");
                        Console.WriteLine();

                        return;
                    }
                }
            };

            static void EditLastName(List<Contact> contacts)
            {
                ShowContacts(contacts);
                Console.WriteLine("");
                Console.Write("Enter the id of the contact you want to edit: ");
                string id = Console.ReadLine() ?? "";

                if (id == "" || id == null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new last name: ");
                string newLastName = Console.ReadLine() ?? "";

                foreach (Contact contact in contacts)
                {
                    if (id == contact.Id.ToString())
                    {
                        string oldLastName = contact.LastName;

                        contact.LastName = newLastName;
                        Console.WriteLine($"{oldLastName} is now {newLastName}!");
                        return;
                    }
                }
            };

            static void EditNumber(List<Contact> contacts)
            {
                ShowContacts(contacts);
                Console.WriteLine("");
                Console.Write("Enter the id of the contact you want to edit: ");
                string id = Console.ReadLine() ?? "";

                if (id == "" || id == null)
                {
                    Console.WriteLine("Invalid ID! Please enter a valid ID.");
                    return;
                }

                Console.Write("Enter the new number: ");
                string newNumber = Console.ReadLine() ?? "";

                foreach (Contact contact in contacts)
                {
                    if (id == contact.Id.ToString())
                    {
                        string oldNumber = contact.Number;

                        contact.Number = newNumber;
                        Console.WriteLine($"{oldNumber} is now {newNumber}!");
                        return;
                    }
                }
            };
        }
    }

}