namespace Space_Expedition {
    internal class GalacticVault {
        public Artifact[] Inventory { get; set; }

        public GalacticVault () {
            Inventory = new Artifact[0];
        }

        public void StartUp (string database) {

            string input = "";

            if (Directory.Exists(database)) {
                Console.WriteLine("Vault Database found");
                string[] files = Directory.GetFiles(database);
                Console.WriteLine("Number of files found: " + files.Length);

                if (files.Length > 0) {
                    Console.WriteLine("Accessing Artifact Data...");

                    foreach (string file in files) {
                        ProcessFile(file);
                    }
                    Console.WriteLine("Artifact data processed successfully!");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();

                } else {
                    Console.WriteLine("No artifact data found.");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                }

            } else {
                Console.Write("Unable to locate Vault Database.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }

            Console.Clear();
        }

        public void ProcessFile (string file) {
            try {
                using (StreamReader reader = new StreamReader(file)) {
                    string line = "";
                    while ((line = reader.ReadLine()) != null) {
                        string[] parameters = line.Split(",");

                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An exception occured while attempting to access {file}: {ex.Message}");
            }
        }
    }
}