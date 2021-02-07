using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using SerializationApp.Components;

namespace SerializationApp.Serializers
{
    public class BinarySerializer : Serializer
    {
        public BinarySerializer(string file, string directory)
            :base(file, directory) { }

        public BinarySerializer(string file)
            : base(file) { }


        public override Component Deserialize()
        {
            using (Stream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    return binaryFormatter.Deserialize(fs) as Component;
                }
                catch (SerializationException)
                {
                    throw new SerializationException("Wrong data in the file! Can't deserialize using binary serializer.");
                }
            }
        }

        public override void Serialize()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (Stream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binaryFormatter.Serialize(fs, Component);
            }
        }      
    }
}
