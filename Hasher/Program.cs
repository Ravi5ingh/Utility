using System;
using TextCopy;

namespace Hasher
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("At least one command line arg is required. (eg. Haser.exe abc) will put the hash for 'abc' in your copy buffer");
                Environment.Exit(1);
            }
            ClipboardService.SetText(BCrypt.Net.BCrypt.HashPassword(args[0], BCrypt.Net.BCrypt.GenerateSalt(12)));
            Console.WriteLine($"Hash for {args[0]} is now in copy buffer");
        }
    }
}
