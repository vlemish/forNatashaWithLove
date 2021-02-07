using System;
using System.IO;
using System.Xml.Serialization;

using SerializationApp.Components;

namespace SerializationApp.Serializers
{
    public class XMLSerializer : Serializer
    {
        public XMLSerializer(string file, string directory)
            :base(file, directory) { }

        public XMLSerializer(string file)
            : base(file) { }


        public override Component Deserialize()
        {
            using (Stream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Component));
                    return xmlSerializer.Deserialize(fs) as Component;
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("Wrong data in the file! Can't deserialize using XML serializer.");
                }
            }
        }

        public override void Serialize()
        {
            using (Stream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Component));
                xmlSerializer.Serialize(fs, Component);
            }
        }
    }
}
