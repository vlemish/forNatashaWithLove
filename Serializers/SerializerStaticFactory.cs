using System;

namespace SerializationApp.Serializers
{
    public static class SerializerStaticFactory
    {
        public static Serializer CreateSerializer(string type, string filePath, string directoryPath = null)
        {
            switch (type)
            {
                case "binary":
                    {
                        if (directoryPath != null)
                        {
                            return new BinarySerializer(filePath, directoryPath);
                        }
                        else
                        {
                            return new BinarySerializer(filePath);
                        }
                    }
                case "xml":
                    {
                        if (directoryPath != null)
                        {
                            return new XMLSerializer(filePath, directoryPath);
                        }
                        else
                        {
                            return new XMLSerializer(filePath);
                        }
                    }

                default:
                    {
                        throw new ArgumentException("Wrong type!");
                    }
            }
        }
    }
}
