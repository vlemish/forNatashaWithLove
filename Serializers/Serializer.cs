using System.IO;
using SerializationApp.Components;

namespace SerializationApp.Serializers
{
    public abstract class Serializer
    {
        public string FilePath { get; set; }

        public string DirectoryPath { get; set; }

        public Component Component { get; protected set; }


        public Serializer() { }

        public Serializer(string filePath, string directoryPath)
        {
            FilePath = filePath;
            DirectoryPath = directoryPath;

            Component = new DirectoryComponent(new DirectoryInfo(DirectoryPath));
        }

        public Serializer(string filePath)
        {
            FilePath = filePath;
        }

        public static bool IsValidFilePath(string filePath)
        {
            var isExist = File.Exists(filePath);
            bool canCreate = false;

            if (!isExist)
            {
                try
                {
                    using (File.Create(filePath))
                    {                        
                        canCreate = true;
                    }

                    File.Delete(filePath);
                }

                catch { }
            }
            return isExist || canCreate
                ? true
                : false;
        }

        public abstract Component Deserialize();

        public abstract void Serialize();

    }
}
