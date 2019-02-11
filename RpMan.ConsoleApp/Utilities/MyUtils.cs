using System;
using System.Collections.Generic;
using System.Text;

namespace RpMan.ConsoleApp.Utilities
{
    public class MyUtils
    {
        public static void ShowPauseMsg(string msg = null)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Console.WriteLine(msg);
            }
            Console.WriteLine("Paused...Press ENTER key to continue.");
            Console.ReadLine();
        }
    }
}
