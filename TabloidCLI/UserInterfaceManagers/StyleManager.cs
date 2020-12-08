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
            { Black, 1 }
            { Blue , 2 }

Cyan, 3

DarkBlue, 4

DarkCyan, 5

DarkGray,6

DarkGreen, 7

DarkMagenta, 8

DarkRed, 9

DarkYellow, 10

Gray, 11

Green, 12

Magenta, 13

Red, 12

White, 15

Yellow, 16
        }; 

        public StyleManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }
        public IUserInterfaceManager Execute()
        {



        }
}
