using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Space_Expedition {
    internal class Program {
        static void Main (string[] args) {
            GalacticVault vault = new GalacticVault();

            //string path = "Artifacts"; // File Location

            vault.StartUp();
        }
    }
}