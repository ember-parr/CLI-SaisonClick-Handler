using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine("|        Journal Menu        |");
            Console.WriteLine("------------------------------");
            Console.WriteLine(" 1) List Entries");
            Console.WriteLine(" 2) Entry Details");
            Console.WriteLine(" 3) Add Entry");
            Console.WriteLine(" 4) Edit Entry");
            Console.WriteLine(" 5) Remove Entry");
            Console.WriteLine(" 0) Go Back");
            Console.WriteLine("------------------------------");
            Console.WriteLine();

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    List();
                    Console.Write("Press any key to go back to Journal Menu");
                    Console.ReadKey();
                    return this;
                case "2":
                    Console.Clear();
                    List();
                    return this;
                //case "2":
                //    Journal journal = Choose();
                //    if (journal == null)
                //    {
                //        return this;
                //    }
                //    else
                //    {
                //        return new JournalDetailManager(this, _connectionString, journal.Id);
                //    }
                case "3":
                    Console.Clear();
                    Add();
                    return this;
                case "4":
                    Console.Clear();
                    Edit();
                    return this;
                case "5":
                    Console.Clear();
                    Remove();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }


        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            Console.WriteLine("");
            Console.WriteLine("______________________________________");
            Console.WriteLine("|_______JOURNAL ENTRY LIST____________|");
            Console.WriteLine("");
            foreach (Journal journal in journals)
            {
                Console.WriteLine(journal.Title);
                Console.WriteLine(journal.CreateDateTime);
                Console.WriteLine(journal.Content);
                Console.WriteLine("---------------------------------");

            }
            Console.WriteLine();
            Console.WriteLine();
        }
        private void Add()
        {
            Console.WriteLine("New Entry");
            Journal entry = new Journal();


            while (entry.Title == null)
            {
                Console.Write("Entry Title: ");
                string entryTitle = Console.ReadLine();
                if (entryTitle.Length > 55 || entryTitle.Length <= 0)
                {
                    Console.WriteLine("ERROR: Title must be less than 55 characters and cannot be left blank.");
                    Console.WriteLine();
                }
                else
                {
                    entry.Title = entryTitle;
                }
            }

            Console.Write("Create your entry here: ");
            entry.Content = Console.ReadLine();
             
            entry.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(entry);
        }

        public Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a journal entry:";
            }
            Console.WriteLine(prompt);

            List<Journal> entries = _journalRepository.GetAll();

            for (int i = 0; i < entries.Count; i++)
            {
                Journal entry = entries[i];
                Console.WriteLine($" {i + 1}) {entry.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return entries[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Edit()
        {
            Journal entryToEdit = Choose("Which journal entry would you like to edit?");
            if (entryToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title for journal entry (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                entryToEdit.Title = title;
            }

            Console.Write("New content for journal entry (blank to leave unchanged): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                entryToEdit.Content = content;
            }

            _journalRepository.Update(entryToEdit);

        }
        private void Remove()
        {
            Journal entryToDelete = Choose("Which journal entry would you like to remove?");
            if (entryToDelete != null)
            {
                _journalRepository.Delete(entryToDelete.Id);
            }
        }
    }
}
