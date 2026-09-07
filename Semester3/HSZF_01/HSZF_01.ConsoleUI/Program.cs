using HSZF_01.ConsoleUI.BusinessLogic;
using HSZF_01.ConsoleUI.Models;

namespace HSZF_01.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student s1 = new("Biki", "A1S2D3", 30, 20, false);
            Student s2 = new("Csunáj", "G4H5J6", 26, 29, true);

            StudentChat chat = new(s1, s2);
            chat.SendMessage(s1, "Hello Csunáj!");
            chat.SendMessage(s2, "Hi Biki!");

            chat.DisplayChat();
        }
    }
}
