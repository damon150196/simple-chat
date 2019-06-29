using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    [Serializable]
    public abstract class Data
    {
        private string author;
        private string room;
        private string type;
        private string value;

        public Data(string author, string room, string type, string value)
        {
            this.author = author;
            this.room = room;
            this.type = type;
            this.value = value;
        }

        public string Type { get => type; set => type = value; }
        public string Value { get => value; set => this.value = value; }
        public string Author { get => author; set => author = value; }
        public string Room { get => room; set => room = value; }
    }
}
