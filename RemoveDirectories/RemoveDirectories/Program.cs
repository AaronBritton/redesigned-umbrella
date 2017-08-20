using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RemoveDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Remove Directories within the current folder struture.");
            Console.WriteLine("Note: this should be ran from the root directory");
            Console.WriteLine("e.g. C:\\F8VB\\Modules\\");
            Console.WriteLine();
            Console.WriteLine("Location of the file which contains forms to keep:");
            string fileName = Convert.ToString(Console.ReadLine());
            Console.WriteLine();

            List<string> formsToKeep = new List<string>();

            if (File.Exists(fileName))
            {
                using(var reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        formsToKeep.Add(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("Cannot read " + fileName);
                Console.ReadKey();
                Environment.Exit(0);
            }

            // Initialize
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] subFolders = Directory.GetDirectories(currentDirectory);


            // Loop through folders within the current directory to find all of their children folders
            foreach(string subFolder in subFolders)
            {
                string[] subSubFolders = Directory.GetDirectories(subFolder);
                

                // Loop through subfolders and check whether it exists within the file read
                foreach(string subSubFolder in subSubFolders)
                {
                    var count = subSubFolder.Count(x => x == '\\');
                    string[] splitDirectory = subSubFolder.Split('\\');
                    string formName = splitDirectory[count];

                    // If the current folder name does not exist within the read file, delete it
                    if (!formsToKeep.Contains(formName))
                    {
                        //Delete the current directory
                        DeleteDirectory(subSubFolder);
                    }
                }
            }


            Console.ReadLine();
        }

        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine("Deleting " + path);
                // Directory.Delete(path, true);
            }
        }
    }
}
