using System;
using System.IO;

using SerializationApp.Serializers;

namespace SerializationApp
{
    class Program
    {       
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 3 || args.Length > 4)
                {
                    var msg = args.Length < 3
                        ? "Not enough arguments!"
                        : "Too much arguments!";
                    throw new ArgumentOutOfRangeException(msg);
                }

                Serializer serializer;
                var type = ParseArg(args[0].ToLower(),
                    arg => arg.Equals("binary") || arg.Equals("xml"),
                    "Wrong value for a type!");
                var operation = ParseArg(args[1].ToLower(),
                    arg => arg.Equals("ser") || arg.Equals("des"),
                    "Wrong value for an operation");
                var filePath = ParseArg(args[2].ToLower(),
                    arg => Serializer.IsValidFilePath(arg), 
                    "Wrong file path!");

                switch (operation)
                {
                    case "ser":
                        {
                            if (args.Length < 4)
                            {
                                throw new ArgumentNullException("Can't serialize a structure without given path to the directory!");
                            }
                            var directoryPath = ParseArg(args[3],
                                arg => Directory.Exists(arg),
                                "Wrong directory path!");
                            serializer = SerializerStaticFactory.CreateSerializer(type, filePath, directoryPath);
                            serializer.Serialize();
                            Console.WriteLine("Structure was successfully serialized!");
                            break;
                        }
                    case "des":
                        {
                            serializer = SerializerStaticFactory.CreateSerializer(type, filePath);
                            var structure = serializer.Deserialize();
                            Console.WriteLine("Your stucture:\n" + structure.ToString());
                            Console.WriteLine("Structure was successfully deserialized!");
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);          
                Console.WriteLine("\nTo serialize a directory, you should specify a type of serializer, an operation (ser - to serialize)," +
                    "a path to file where the structure of the directory will be saved, and the path to the directory itself." +
                    "\n\nType can be xml or binary. Operation can be ser(serialize) or des(deserialize).\n" +
                    "\nExample of use: binary ser F:\\Data\\source.txt F:\\Data\n" +
                    "\nTo deserialize a structure, you can drop out the path to the directory and specify the type of serializer (the one that was used to serialize), " +
                    "an operation (des), and a path to the file in which the structure of the file stored.\n" +
                    "\nExample of use: binary des F:\\Data\\source.txt\n" +
                    "\nThere is a strict order of arguments in the program. The order of args should be next: type-> operation-> file path-> directory path (if deserializing).\n");
                Console.ResetColor();
            }
        }   
        
        static string ParseArg(string arg, Func<string, bool> condition, string erorMsg)
        {            
            return condition(arg)
                ? arg
                : throw new ArgumentException(erorMsg);
        }
    }
}
