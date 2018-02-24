using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace JsonGenerator
{
    [Serializable]
    class Format
    {
        public List<string> Properties = new List<string>();
        public List<List<string>> Values = new List<List<string>>();
    }

    class Program
    {
        static string saveLocation = "jsonformat";

        static void WriteTitle()
        {
            Console.Clear();
            Console.WriteLine("JSONGenerator 2.0\n");
        }

        static bool WriteFormatToFile(Format format)
        {
            try
            {
                using (Stream stream = File.Create(saveLocation))
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    formatter.Serialize(stream, format);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error writing to file: {e.Message}");
                return false;
            }
        }

        static string CreateJson(Format format)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                int counter = 0;
                foreach (string property in format.Properties)
                {
                    writer.WritePropertyName(property);

                    if (format.Values[counter].Count > 1)
                    {
                        writer.WriteStartArray();

                        foreach (string value in format.Values[counter])
                            writer.WriteValue(value);

                        writer.WriteEnd();
                    }
                    else
                    {
                        foreach (string value in format.Values[counter])
                            writer.WriteValue(value);
                    }

                    counter++;
                }

                writer.WriteEndObject();

                return sb.ToString();
            }
        }

        static void NewFormat()
        {
            WriteTitle();
            Console.WriteLine("  New JSON format\n  Write !end to end\n");

            Format format = new Format();

            while (true)
            {
                Console.Write("  Property name:");
                string input = Console.ReadLine();

                if (!String.IsNullOrEmpty(input) && Char.IsLetter(input[0]))
                    format.Properties.Add(input);
                else if (input == "!end")
                    break;
                else
                    Console.WriteLine("  Property name cannot be empty or start with a number.\n");
            }

            if (format.Properties.Count < 1)
            {
                Console.WriteLine("\n  No properties set, nothing is done.");
                Thread.Sleep(1000);
            }
            else
            {
                if (WriteFormatToFile(format))
                {
                    Console.WriteLine("\n  Saved format.");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("done");
        }

        static Format LoadFormat()
        {
            Format format = new Format();

            try
            {
                using (Stream stream = File.Open(saveLocation, FileMode.Open))
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    format = (Format)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\n  Failed to load format file: {e.Message}");
                Thread.Sleep(1000);
            }

            return format;
        }

        static void NewFile()
        {
            Format format = LoadFormat();

            WriteTitle();
            Console.WriteLine("  New JSON file\n");
            Console.WriteLine("  Enter one or multiple values per property, leave empty to continue\n");

            foreach (string property in format.Properties)
            {
                List<string> valuesOfProperty = new List<string>();
                string input = "";

                while(true)
                {
                    Console.Write($"  {property}:");
                    input = Console.ReadLine();
                    if (input == "")
                        break;
                    else
                        valuesOfProperty.Add(input);
                }

                format.Values.Add(valuesOfProperty);
            }

            File.WriteAllText("configuration.json", CreateJson(format));
            Console.WriteLine("\n  Generated new json file");
            Thread.Sleep(1000);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                WriteTitle();
                Console.WriteLine("  1) Create new json format");
                Console.WriteLine("  2) Create a json file");
                Console.WriteLine("  3) Exit");
                Console.Write("\n> ");
                string input = Console.ReadLine();

                if (input == "1")
                    NewFormat();
                else if (input == "2")
                    NewFile();
                else if (input == "3")
                    break;
            }
        }
    }
}
