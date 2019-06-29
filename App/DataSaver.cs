using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static class DataSaver
    {
        public static byte[] Save(Data data)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
                return stream.ToArray();
            }
        }
    }
}
