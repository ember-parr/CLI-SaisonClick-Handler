using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    class StyleManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;
        private Dictionary<int, string> colors = new Dictionary<int, string>
        {
            { 1, "Black" },
            { 2, "Blue"  },
            { 3, "Cyan" },
            { 4, "DarkBlue" },
            { 5, "DarkCyan" },
            { 6, "DarkGray" },
            { 7, "DarkGreen" },
            { 8, "DarkMagenta" },
            { 9, "DarkRed"},
            { 10, "DarkYellow" },
            { 11, "Gray"},
            { 12, "Green" },
            { 13, "Magenta" },
            { 14, "Red" },
            { 15, "White" },
            { 16, "Yellow" },
        };

        public StyleManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Pick a new Background Color");
            foreach (KeyValuePair<int, string> pair in colors)
            {
                Console.BackgroundColor =  (ConsoleColor)Enum.Parse(typeof(ConsoleColor), pair.Value);
                Console.WriteLine($"{pair.Key}) {pair.Value}");
                Console.ResetColor();
            }
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            int choice;
            string stringChoice = Console.ReadLine();
            try
            {
                choice = int.Parse(stringChoice);
            }
            catch 
            {
                Console.WriteLine("Invalid Selection");
                return this;
            }

            if (choice != 0)
            {
                try
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colors.GetValueOrDefault(choice));
                    return _parentUI;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid Selection");
                    return this;
                }
            }
            return _parentUI;


            //switch (choice)
            //{
            //    case "1":
            //        List();
            //        return this;

            //}
        }
    }
}
