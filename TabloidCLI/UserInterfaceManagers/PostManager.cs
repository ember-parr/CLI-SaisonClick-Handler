using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;
        private AuthorManager _authorRepo;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _authorRepo = new AuthorManager(parentUI, connectionString);
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            //_authorRepo = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }
        //AuthorManager authorManage = new AuthorManager(this, _connectionString);

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) New Post");
            Console.WriteLine(" 2) Nothing here yet");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Add();
                    return this;
                case "2":
                    Console.WriteLine("nothing yet....");
                    return this;
                default:
                    Console.WriteLine("Invlid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Author: ");

            Author chosenAuthor = _authorRepo.Choose("Select an author: ");


            post.Author = chosenAuthor;
            post.PublishDateTime = DateTime.Now;



            // author and blog go here

            _postRepository.Insert(post);
        }

    }
}
