using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition {
    internal class Artifact {
        public string Name { get; set; }
        public string Planet { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public Artifact(string name, string planet, string date, string location, string description) {
            Name = name;
            Planet = planet;
            Date = date;
            Location = location;
            Description = description;
        }

        // This ones nicer to look at
        public override string ToString () {
            return $"{Name} | {Planet} | {Date} | {Location} | {Description}";
        }

        // This ones how the file was formatted
        public string Save() {
            return $"{Name},{Planet},{Date},{Location},{Description}";
        }
    }
}
