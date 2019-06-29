using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Datas
{
    [Serializable]
    public class DataNick : Data
    {
        public DataNick(string value) : base(Own.Author, Own.Room, "NICK", value)
        {
            
        }
    }
}
