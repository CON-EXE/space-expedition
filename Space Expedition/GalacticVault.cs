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

        public void StartUp () {

            if (File.Exists("galactic_vault.txt")) {
                Console.WriteLine("Vault Database found");
                Console.WriteLine("Accessing Artifact Data...");

                CreateInventory("galactic_vault.txt");

                Console.WriteLine("Artifact data processed successfully!");
                Console.Write("Press any key to continue...");
                Console.ReadKey();

            } else {
                Console.Write("Unable to locate Vault Database.");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }

            Console.Clear();
            menu();
        }

        public void menu() {

            string input = "";
            bool loggedIn = true;

            while(loggedIn) {

                Console.WriteLine("Welcome to the Galactic Vault manager.");
                Console.WriteLine("1. Add Artifact.");
                Console.WriteLine("2. View artifact inventroy.");
                Console.WriteLine("3. Exit");
                Console.Write("Make a selection: ");
                input = Console.ReadLine();
            
                switch (input) {
                    case "1":
                        AddArtifact();
                        break;
                    case "2":
                         ViewInventory();
                        break;
                    case "3":
                        loggedIn = false;
                        break;
                }
            }
            Console.Clear();
        }

        // Creates initial inventory from vault file.
        public void CreateInventory(string vaultFile) {
            try {
                using (StreamReader reader = new StreamReader(vaultFile)) {
                    string line = "";
                    while ((line = reader.ReadLine()) != null) {
                        string[] parameters = line.Split(",");
                        parameters[0] = DecodeName(parameters[0]);
                        Artifact newArtifact = new Artifact(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
                        Artifact[] newInvt = new Artifact[Inventory.Length + 1];
                        newInvt[newInvt.Length - 1] = newArtifact;

                        for(int i = 0; i < Inventory.Length; i++) {
                            newInvt[i] = Inventory[i];
                        }
                        Inventory = newInvt;
                    }
                    Inventory = SortInventory();
                }
            } catch (Exception ex) {
                Console.WriteLine($"An exception occured while attempting to access {vaultFile}: {ex.Message}");
            }
        }

        public void AddArtifact() {
            string input = "";

            try {
                Console.WriteLine("Note: Artifact names are case sensetive.");
                Console.Write("Enter artifact name: ");
                input = (Console.ReadLine()) + ".txt";
                if (File.Exists(input)) {
                    Console.WriteLine("Artifact file found.");
                    using (StreamReader reader = new StreamReader(input)) {
                        string line = "";
                        line = reader.ReadLine();
                        string[] parameters = line.Split(",");
                        parameters[0] = DecodeName(parameters[0]);
                        Artifact newArtifact = new Artifact(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
                        Inventory = InsertArtifact(newArtifact);

                    }
                } else {
                    Console.WriteLine("Artifact file not found.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"An exception occured: {ex.Message}");
            }
        }

        public void ViewInventory () {
            string input = "";
            bool viewing = true;
            try {
                while (viewing) {
                    Console.WriteLine("1. View artifact list");
                    Console.WriteLine("2. Search artifact by name");
                    Console.WriteLine("3. Exit");
                    input = Console.ReadLine();

                    switch(input) {
                        case "1":
                            foreach(Artifact a in Inventory) {
                                Console.WriteLine(a.Name);
                            }
                            break;
                        case "2":
                            Console.WriteLine("Enter artifact name: ");
                            input = Console.ReadLine();
                            SearchInventory(input);
                            break;
                        case "3":
                            viewing = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"An exception occured: {ex.Message}");
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

        public Artifact[] InsertArtifact (Artifact newArtifact) {

            Artifact[] newInventory = new Artifact[Inventory.Length + 1];
            int i = Inventory.Length - 1;

            while(i >= 0 && string.Compare(Inventory[i].Name, newArtifact.Name) > 0) {
                newInventory[i + 1] = Inventory[i];
                i--;
            }

            newInventory[i + 1] = newArtifact;

            for (int j = 0; j <= i; j++) {
                newInventory[j] = Inventory[j];
            }
            
            return newInventory;
        }

        public Artifact[] SortInventory () {
            for ( int i = 1; i < Inventory.Length; i++) {
                Artifact key = Inventory[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(Inventory[j].Name, key.Name) > 0) {
                    Inventory[j + 1] = Inventory[j];
                    j--;
                }
                Inventory[j + 1] = key;
            }
            return Inventory;
        }

        public void SearchInventory (string search) {
            int low = 0;
            int high = Inventory.Length - 1;

            while (low <= high) {
                int mid = low + high / 2;
                int comp = string.Compare(Inventory[mid].Name, search);

                if (comp == 0) {
                    Console.WriteLine(Inventory[mid]);
                    break;
                } else if (comp < 0) {
                    low = mid + 1;
                } else {
                    high = mid - 1;
                }
            }
            Console.WriteLine("Artifact not found.");
        }
    }
}