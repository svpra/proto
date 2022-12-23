using System;

namespace TCPServer
{
    internal class Program
    {
        private static void Main()
        {
            Console.Title = "QtServer";
            Console.ForegroundColor = ConsoleColor.White;

            var server = new QtServer();
            server.Start();
            server.AcceptClients();
        }
    }
}
