using System;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchBlogs();
                    return this;
                case "2":
                    SearchAuthors();
                    return this;
                case "3":
                    SearchPosts();
                    return this;
                case "4":
                    SearchAll();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            //searches authors through the tagRepo class's search method and returns a searchResult object, which includes a list of Author objects 
            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            //search results obj's have a prop, .NoResultsFound, that returns true when their are no results found in the search
            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            //if there was a result use the display method of the search results object
            else
            {
                results.Display();
            }
        }

        private void SearchBlogs()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Blog> results = _tagRepository.SearchBlog(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else 
            {
                results.Display();
            }

        }

        private void SearchPosts()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }

        }

        //runs 3 search methods and get 3 types of SearchResults objects
        private void SearchAll()
        {
            Console.Write("Tag> ");
            string tagName = Console.ReadLine();

            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);
            SearchResults<Blog> bResults = _tagRepository.SearchBlog(tagName);
            SearchResults<Author> aResults = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound && aResults.NoResultsFound && bResults.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                if (!aResults.NoResultsFound) 
                {
                    Console.WriteLine($"Authors:");
                   aResults.Display();
                }
                if (!bResults.NoResultsFound)
                {
                    Console.WriteLine($"Blogs:");
                    bResults.Display();
                        }
                if (!results.NoResultsFound)
                {
                    Console.WriteLine($"Posts:");
                    results.Display();
                }
            }
            
        }
    }
}