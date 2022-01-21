using System;
using System.IO;
using System.Net;

namespace ModGrabber
{
    class Program
    {
        static bool programShouldStop = false;
        static string savePath = "./Mods/";
        static string modList = "modlist.txt";
        static string[] modURLs;
        static void Main(string[] args)
        {
            readModList();
            Console.WriteLine("   _____             .___________            ___.  ___.                 ");
            Console.WriteLine("  /     \\   ____   __| _/  _____/___________ \\_ |__\\_ |__   ___________ ");
            Console.WriteLine(" /  \\ /  \\ /  _ \\ / __ /   \\  __\\_  __ \\__  \\ | __ \\| __ \\_/ __ \\_  __ \\");
            Console.WriteLine("/    Y    (  <_> ) /_/ \\    \\_\\  \\  | \\// __ \\| \\_\\ \\ \\_\\ \\  ___/|  | \\/");
            Console.WriteLine("\\____|__  /\\____/\\____ |\\______  /__|  (____  /___  /___  /\\___  >__|   ");
            Console.WriteLine("        \\/            \\/       \\/           \\/    \\/    \\/     \\/       ");
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Welcome to ModGrabber!");
            Console.WriteLine("There are " + modURLs.Length + " mods ready to grab!");
            Console.WriteLine("");
            Console.WriteLine("Enter the number for your choice:");
            Console.WriteLine("");
            Console.WriteLine("1: Grab the mods! (This will take some time with lots of mods!)");
            Console.WriteLine("2: Exit");
            Console.WriteLine("");

            while (!programShouldStop)
            {
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: // grab the mods!
                        grabMods();
                        break;
                    case 2: // Exit
                        System.Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("You chose poorly! Try again.");
                        break;
                }
            }
        }

        static string getFileName(String url)
        {
            string name = url.Substring(url.IndexOf("#") + 1);
            return name;
        }

        static void readModList()
        {
            string[] lines = File.ReadAllLines(modList);
            modURLs = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(lines[i]))
                    modURLs[i] = lines[i];
            }
        }

        static void grabMods()
        {
            using (WebClient client = new WebClient())
            {
                for (int i = 0; i < modURLs.Length; i++)
                {
                    String link = modURLs[i];
                    String fileName = getFileName(link);
                    Console.WriteLine("[{0}/{1}] - Grabbing {2}", i + 1, modURLs.Length, fileName);
                    client.DownloadFile(link, savePath + fileName);
                }
                Console.WriteLine("Mods have been downloaded!");
                Console.WriteLine("Open the mods folder to view the downloaded mods");
                Console.WriteLine("Edit modlist.txt to download more! (Don't download over 20,000/hr)");
                return;
            }
        }
    }
}
