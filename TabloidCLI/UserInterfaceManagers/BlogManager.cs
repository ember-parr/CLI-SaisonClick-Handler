using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.Clear();
            Console.WriteLine("------------------------------");
            Console.WriteLine("|         Blog Menu          |");
            Console.WriteLine("------------------------------");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
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
                    Console.Write("Press any key to go back to Blog Menu");
                    Console.ReadKey();
                    return this;
                case "2":
                    Console.Clear();
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
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
            Console.WriteLine("");
            Console.WriteLine("_____________________________________");
            Console.WriteLine("|___________All Blogs________________|");
            Console.WriteLine("");
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();

        }
        public Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            while (blog.Title == null)
            {
                Console.Write("Title: ");
                string blogTitle = Console.ReadLine();
                if (blogTitle.Length > 55)
                {
                    Console.WriteLine("ERROR: Title must be less than 55 characters.");
                    Console.WriteLine();
                }
                else if (blogTitle.Length <= 0)
                {
                    Console.WriteLine("ERROR: Please enter a title.");
                    Console.WriteLine();
                }
                else
                {
                    blog.Title = blogTitle;
                }
            }
            while (blog.Url == null)
            {
                Console.Write("URL: ");
                string blogUrl = Console.ReadLine();
                if (blogUrl.Length > 2000)
                {
                    Console.WriteLine("ERROR: Seriously why is this URL so long? This is no place to tell a story. Simply include a standard URL.");
                    Console.WriteLine();
                }
                else if (blogUrl.Length <= 0)
                {
                    Console.WriteLine("ERROR: Please enter a valid URL.");
                    Console.WriteLine();
                }
                else
                {
                    blog.Url = blogUrl;
                }
            }

            _blogRepository.Insert(blog);
        }

        private void Edit()
        {
            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }

            _blogRepository.Update(blogToEdit);
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which blog would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }
    }
}
