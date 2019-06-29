using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Datas
{
    [Serializable]
    public class DataInfo : Data
    {
        public DataInfo(string value) : base(Own.Author, Own.Room, "", value)
        {
            
        }
    }
}
