using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class DataLoader
    {
        public static Data Load(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (Data)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
