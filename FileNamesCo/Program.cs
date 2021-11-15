using System;
using System.IO;

namespace FileNamesCo
{
    class Program
    {
        static string sourcePath = "";
        static DirectoryInfo di;
        static void Main(string[] args)
        {
            //nápověda pro uživatele
            //nápověda na youtube
            //autoclearing možnost
            //clear command
            bool opk = false;
            Console.Write("Vítejte v programu! - zajdete příkaz \"help\" pro nápovědu\nzadejte příkaz \"start\" nebo dejte ENTER pro start");
            while (opk)
            {
                Commands(ref opk);
            }
            Console.ReadKey();
        }

        private static void Commands(ref bool opk, string command = "")
        {
            switch (command)
            {
                case "start":
                default:
                    {                  
                        if (sourcePath == string.Empty) //new source path
                        {
                            startPath:
                            Console.Write("Zadej cestu k složce se soubory: ");
                            string input = Console.ReadLine();
                            sourcePath = Directory.Exists(input) ? input : string.Empty;
                            if (sourcePath != string.Empty)
                            { di = new DirectoryInfo(sourcePath); }
                            else { Console.Write($"nexistující cesta ke složce - {input} .\n"); goto startPath; }
                        }
                        else { di = new DirectoryInfo(sourcePath); }
                        FileInfo[] files = di.GetFiles(); //get all files from directory

                        Console.Write("Zadej klíčové slovo k nahrazení v souboru: ");
                        string searchName = Console.ReadLine(); //filename for replace

                        Console.Write("Zadej název nahrazení klíčového slova v souboru: ");
                        string replaceName = Console.ReadLine(); //filename to replace

                        foreach (FileInfo file in files)
                        {
                            if (file.Name.Contains(searchName))
                            {
                                string newFileName = file.Name.Replace(searchName, replaceName);
                                string basePath = di.FullName;
                                string fileNamePath = Path.Combine(basePath, file.Name);
                                string newFileNamePath = Path.Combine(basePath, newFileName);
                                File.Move(fileNamePath, newFileNamePath);
                            }
                        }
                        Console.Write("Opakovat zadání? - y / n - ");
                        opk = Console.ReadLine().ToLower() == "y" ? true : false;
                        Console.Write("Stejná cesta k složce k souboru? - y / n - ");
                        sourcePath = Console.ReadLine().ToLower() == "y" ? sourcePath : string.Empty;
                        Console.Clear();
                        break;
                    }
            }
        }
    }
}