using B3.Server;
using System;

namespace B3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hell, B3 World!");
            B3Server server = new B3Server();
            server.Start();


        }
    }
}
