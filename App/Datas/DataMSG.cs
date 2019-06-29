using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Datas
{
    [Serializable]
    public class DataMSG : Data
    {
        public DataMSG(string value) : base(Own.Author, Own.Room, "MSG", value)
        {
            
        }
    }
}
