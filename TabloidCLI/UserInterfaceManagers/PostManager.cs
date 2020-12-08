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
        private BlogManager _blogRepo;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _authorRepo = new AuthorManager(parentUI, connectionString);
            _blogRepo = new BlogManager(parentUI, connectionString);
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            //_authorRepo = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }
        //AuthorManager authorManage = new AuthorManager(this, _connectionString);

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove A Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invlid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            Console.WriteLine("");
            Console.WriteLine("_____________________________________");
            Console.WriteLine("|___________All Posts________________|");
            Console.WriteLine("");
            foreach (Post post in posts)
            {
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Written By: {post.Author.FirstName} {post.Author.LastName}");
                Console.WriteLine($"URL: {post.Url}");
                Console.WriteLine($"Blog: {post.Blog.Title}");
                Console.WriteLine("---------------------------------");
            }
            Console.WriteLine();
            Console.WriteLine();
        }


        public Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a post: ";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");


            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

        }







        private void Add()
        {
            Console.WriteLine("Add Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();


            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Author: ");

            Author chosenAuthor = _authorRepo.Choose("Select an author: ");
            Blog chosenBlog = _blogRepo.Choose("Select a blog: ");

            Console.Write("Publish Date (input as mm/dd/yyyy): ");
            DateTime inputDate = DateTime.Parse(Console.ReadLine());

            post.Blog = chosenBlog;
            post.Author = chosenAuthor;
            post.PublishDateTime = inputDate;



            _postRepository.Insert(post);
        }


        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit? ");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New post title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New post URL (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New publish date (input as mm/dd/yyyy or blank to leave unchaged) ");
            string publish = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(publish))
            {
                postToEdit.PublishDateTime = DateTime.Parse(publish);
            }
            Console.Write("Would You Like To Change The Posts Author? (y/n) ");
            string authorQuestion = Console.ReadLine();
            if (authorQuestion == "y" || authorQuestion == "Y")
            {
                Author chosenAuthor = _authorRepo.Choose("Select an author: ");
                postToEdit.Author = chosenAuthor;
            }

            Console.Write("Would you like to change the post's blog? (y/n) ");
            string blogQuestion = Console.ReadLine();
            if (blogQuestion == "y" || blogQuestion == "Y")
            {
                Blog chosenBlog = _blogRepo.Choose("Select a blog: ");
                postToEdit.Blog = chosenBlog;
            }
            Console.WriteLine();
            Console.WriteLine();

            _postRepository.Update(postToEdit);

        }



        private void Remove()
        {
            Post postToDelete = Choose("Which Post Would You Like To Delete? ");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}







