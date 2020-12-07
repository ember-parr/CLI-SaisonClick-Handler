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
        private BlogRepository _BlogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _BlogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                //case "1":
                //    List();
                //    return this;
                //case "2":
                //    Blog author = Choose();
                //    if (author == null)
                //    {
                //        return this;
                //    }
                //    else
                //    {
                //        return new BlogDetailManager(this, _connectionString, author.Id);
                //    }
                case "3":
                    Add();
                    return this;
                //case "4":
                //    Edit();
                //    return this;
                //case "5":
                //    Remove();
                //    return this;
                //case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }


        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.Write("Title: ");
            blog.Title = Console.ReadLine();

            Console.Write("URL: ");
            blog.Url = Console.ReadLine();

            _BlogRepository.Insert(blog);
        }


    }
}
