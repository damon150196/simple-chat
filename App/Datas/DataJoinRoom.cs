using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Datas
{
    [Serializable]
    public class DataJoinRoom : Data
    {
        public DataJoinRoom() : base(Own.Author, Own.Room, "JOIN", "")
        {
            
        }
    }
}
