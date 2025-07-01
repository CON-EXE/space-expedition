using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.IO.Directory;

namespace Space_Expedition {
    internal class Program {
        static void Main (string[] args) {

            if (Exists("Artifacts")) {
                Console.WriteLine("Vault Database found");
            }
            string[] files = GetFiles("Artifacts");
            Console.WriteLine("Number of files found: " + files.Length);
            foreach (string file in files) {
                ReadFile(file);
            }


        }

        static void ReadFile (string file) {
            try {
                using (StreamReader reader = new StreamReader(file)) {
                    string line = "";
                    while ((line = reader.ReadLine()) != null) {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An exception occured while attempting to access {file}: {ex.Message}");
            }
        }
    }
}