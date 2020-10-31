using CoreRCON;
using System;
using System.Linq;
using System.Net;

namespace Rcon
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var c = new RCON(IPAddress.Parse("37.57.26.218"), 25575, "2281337");
            await c.ConnectAsync();
            while (true)
            {
                var text = (await c.SendCommandAsync(Console.ReadLine())).Split("§");
                if (text.Count() <= 1) Console.Write(text.First());
                else
                    foreach (var t in text)
                    {
                        if (t.Length < 1) continue;
                        if (int.TryParse(t[0].ToString(), out int csas))
                        {
                            Console.ForegroundColor = (ConsoleColor)csas;
                        }
                        else
                        {
                            switch (t[0].ToString())
                            {
                                case "a":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case "b":
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    break;
                                case "c":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case "d":
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case "e":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case "f":
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                            }
                        }
                        Console.Write(t.Substring(1));
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                Console.WriteLine();
            }

        }
    }
}
