using System;
using System.Collections.Generic;
using System.IO;

namespace RpMan.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * "Ctrl + F5" in Visual Studio to run your program,
             * this will add a pause with "Press any key to continue..."
             * automatically without any Console.Readline() or ReadKey() functions.
             * 
             */

            GenerateEntityConfigFiles.process();

            Console.WriteLine("Hello World!");
        }


    }
}
