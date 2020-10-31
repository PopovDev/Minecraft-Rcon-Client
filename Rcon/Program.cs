using CoreRCON;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rcon
{
    class Program
    {
        static IniFile INI = new IniFile("config.ini");
        static async Task Main(string[] args)
        {
            if (!INI.KeyExists("IP", "Connect")) INI.Write("Connect", "IP", "");
            if (!INI.KeyExists("PORT", "Connect")) INI.Write("Connect", "PORT", "");
            if (!INI.KeyExists("PASS", "Connect")) INI.Write("Connect", "PASS", "");
            
            Console.Write($"Enter IP ({INI.ReadINI("Connect", "IP")}):");
            var ReadIP = Console.ReadLine();
            Console.Write($"Enter Port ({INI.ReadINI("Connect", "PORT")}):");
            var ReadPort = Console.ReadLine();
            Console.Write($"Enter Password ({INI.ReadINI("Connect", "PASS")}):");
            var ReadPASS = Console.ReadLine();

            if (!string.IsNullOrEmpty(ReadIP)) INI.Write("Connect", "IP", ReadIP);      
            if (!string.IsNullOrEmpty(ReadPort)) INI.Write("Connect", "PORT", ReadPort);      
            if (!string.IsNullOrEmpty(ReadPASS)) INI.Write("Connect", "PASS", ReadPASS);

            var c = new RCON(IPAddress.Parse(INI.ReadINI("Connect", "IP")), ushort.Parse(INI.ReadINI("Connect", "PORT")), INI.ReadINI("Connect", "PASS"));
            try
            {
                await c.ConnectAsync();
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection Error \n Press key to exit");
                Console.ReadLine();
                Environment.Exit(1);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Connected");
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var text = (await c.SendCommandAsync(Console.ReadLine())).Split("§");
                if (text.Count() <= 1) Console.Write(text.First());
                else
                    foreach (var t in text)
                    {
                        if (t.Length < 1) continue;
                        Console.ForegroundColor = (ConsoleColor)int.Parse(t[0].ToString(), NumberStyles.HexNumber);
                        Console.Write(t.Substring(1));
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                Console.WriteLine();
            }

        }
    }
}
