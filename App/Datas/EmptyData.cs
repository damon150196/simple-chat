using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Datas
{
    [Serializable]
    public class EmptyData : Data
    {
        public EmptyData() : base(Own.Author, Own.Room, "", "")
        {
            
        }
    }
}
