using System;
using ObjectIO;

namespace ObjectIO.Runtime;
internal class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "[Loading]";
        Interpreter interpreter = new Interpreter();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Title = "[ObjectIO]";
        if (args.Length > 0)
        {
            if (args.Contains("-run"))
            {
                interpreter.Execute(File.ReadAllText(args[0]));
            }
        }
        else
        {
        again:
            Console.Write("File name: ");
            var fileName = Console.ReadLine().Replace("$this", Environment.CurrentDirectory);
            if (File.Exists(fileName) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid file name");
                Console.ForegroundColor = ConsoleColor.White;
                goto again;
            }
            Console.Write("Options: (run)");
            var options = Console.ReadLine();
            if (options.ToLower() == "run")
            {
                interpreter.Execute(File.ReadAllText(fileName));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option");
                Console.ForegroundColor = ConsoleColor.White;
                goto again;
            }
        }
    }
}