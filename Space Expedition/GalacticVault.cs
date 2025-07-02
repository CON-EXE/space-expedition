namespace Space_Expedition {
    internal class GalacticVault {
        public Artifact[] Inventory { get; set; } 

        //Decoding Arrays
        public char[] Original { get; }
        public char[] Mapped { get; }

        public GalacticVault () {
            Inventory = [];
            Original = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
            Mapped = ['H', 'Z', 'A', 'U', 'Y', 'E', 'K', 'G', 'O', 'T', 'I', 'R', 'J', 'V', 'W', 'N', 'M', 'F', 'Q', 'S', 'D', 'B', 'X', 'L', 'C', 'P'];
        }

        public void StartUp (string database) {

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

        public void menu() {

            string input = "";
            bool loggedIn = true;

            Console.WriteLine("Welcome to the Galactic Vault manager. Please make a selection:");
            Console.WriteLine("1. Add Artifact.");
            Console.WriteLine("2. View artifact inventroy.");
            Console.WriteLine("3. Exit");
            input = Console.ReadLine();
            
            while(loggedIn) {
                switch (input) {
                    case "1":
                        // AddArtifact();
                        break;
                    case "2":
                        // ViewInventory();
                        break;
                    case "3":
                        loggedIn = false;
                        break;
                }
            }
        }

        public void ProcessFile (string file) {
            try {
                using (StreamReader reader = new StreamReader(file)) {
                    string line = "";
                    while ((line = reader.ReadLine()) != null) {
                        string[] parameters = line.Split(","); 
                        parameters[0] = DecodeName(parameters[0]);
                        Console.WriteLine(parameters[0]);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An exception occured while attempting to access {file}: {ex.Message}");
            }
        }

        public string DecodeName(string name) {
            string[] splitName = name.Split(' ');

            for(int i = 0; i < splitName.Length; i++) {
                string[] splitWord = splitName[i].Split('|');

                for(int j = 0; j < splitWord.Length; j++) {
                    char character = splitWord[j][0];
                    int level = int.Parse(splitWord[j].Substring(1));
                    splitWord[j] = Decode(character, level).ToString(); 
                }
                splitName[i] = String.Join("", splitWord);
            }
            name = String.Join(" ", splitName);
            return name;
        }

        private char Decode(char character, int level) {
            int index = 0;
            if (level == 1) {
                index = Array.IndexOf(Original, character);
                character = Original[Original.Length - 1 - index];
            } else {
                index = Array.IndexOf(Original, character);
                character = Mapped[index];
                level -= 1;
                character = Decode(character, level);
            }
            return character;
        }
    }
}