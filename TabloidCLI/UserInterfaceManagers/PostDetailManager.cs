﻿using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    View();
                    return this;
                case "2":
                    Console.Clear();
                    AddTag();
                    return this;
                case "3":
                    Console.Clear();
                    Edit();
                    return this;
                case "4":
                    Console.Clear();
                    RemoveTag();
                    return this;
                case "0":
                    Console.Clear();
                    return _parentUI;
                default:
                    Console.WriteLine("Invlid Selection");
                    return this;
            }
        }


        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Post Title: {post.Title}");
            Console.WriteLine($"Author: {post.Author.FirstName} {post.Author.LastName}");
            Console.WriteLine($"Blog: {post.Blog.Title}");
            Console.WriteLine($"Published: {post.PublishDateTime}");
            Console.WriteLine("Tags comming in V2:");
            


            Console.WriteLine();
            Console.Write("Press any key to go back to Post Menu");
            Console.ReadKey();
        }
    }
}
