using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        FileController fileController = new FileController();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nVelg en handling:");
            Console.WriteLine("1. Vis alle personer");
            Console.WriteLine("2. Søk etter en person (via ID)");
            Console.WriteLine("3. Legg til en ny person");
            Console.WriteLine("4. Slett en person (via ID)");
            Console.WriteLine("5. Vis JSON-data");
            Console.WriteLine("6. Avslutt");
            Console.Write("Ditt valg: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowAllPeople(fileController);
                    break;
                case "2":
                    SearchPerson(fileController);
                    break;
                case "3":
                    AddPerson(fileController);
                    break;
                case "4":
                    DeletePerson(fileController);
                    break;
                case "5":
                    fileController.DisplayJsonData();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Avslutter programmet...");
                    break;
                default:
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                    break;
            }
        }
    }

    static void ShowAllPeople(FileController fileController)
    {
        List<Person> people = fileController.GetPeople();
        if (people.Count == 0)
        {
            Console.WriteLine("Ingen personer funnet.");
        }
        else
        {
            Console.WriteLine("\nPersonliste:");
            foreach (var person in people)
            {
                Console.WriteLine($"ID: {person.Id}, Navn: {person.Name}, Alder: {person.Age}");
            }
        }
    }

    static void SearchPerson(FileController fileController)
    {
        Console.Write("Skriv inn ID på personen du søker: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            List<Person> people = fileController.GetPeople();
            Person foundPerson = people.FirstOrDefault(p => p.Id == id);

            if (foundPerson != null)
            {
                Console.WriteLine($"Person funnet: ID: {foundPerson.Id}, Navn: {foundPerson.Name}, Alder: {foundPerson.Age}");
            }
            else
            {
                Console.WriteLine("Ingen person funnet med den ID-en.");
            }
        }
        else
        {
            Console.WriteLine("Ugyldig ID-format.");
        }
    }

    static void AddPerson(FileController fileController)
    {
        List<Person> people = fileController.GetPeople();

        Console.Write("Skriv inn navn: ");
        string name = Console.ReadLine();

        Console.Write("Skriv inn alder: ");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            int newId = people.Any() ? people.Max(p => p.Id) + 1 : 1;
            people.Add(new Person { Id = newId, Name = name, Age = age });

            fileController.SavePeople(people);
            Console.WriteLine("Person lagt til.");
        }
        else
        {
            Console.WriteLine("Ugyldig alder.");
        }
    }

    static void DeletePerson(FileController fileController)
    {
        List<Person> people = fileController.GetPeople();

        Console.Write("Skriv inn ID på personen du vil slette: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Person personToRemove = people.FirstOrDefault(p => p.Id == id);
            if (personToRemove != null)
            {
                people.Remove(personToRemove);
                fileController.SavePeople(people);
                Console.WriteLine("Person slettet.");
            }
            else
            {
                Console.WriteLine("Ingen person funnet med den ID-en.");
            }
        }
        else
        {
            Console.WriteLine("Ugyldig ID-format.");
        }
    }
}
