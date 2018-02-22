using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace JsonGenerator
{
    class Settings
    {
        public string ClientId { get; set; }
        public string BotToken { get; set; }
        public IList<string> OwnerIds { get; set; }
    }

    class Program
    {
        static string GenerateJson(string clientId, string botToken, List<string> ownerIds)
        {
            Settings settings = new Settings
            {
                ClientId = "client_id_here",
                BotToken = "bot_token_here",
                OwnerIds = new List<string> { "owner_id", "or_multiple" }
            };

            if (clientId != "")
                settings.ClientId = clientId;
            if (botToken != "")
                settings.BotToken = botToken;
            if (ownerIds.Capacity != 0)
                settings.OwnerIds = ownerIds;

            return JsonConvert.SerializeObject(settings, Formatting.Indented);
        }

        static void WriteToFile(string s)
        {
            File.WriteAllText("configuration.json", s);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Create a new configuration.json, leave blank for placeholder value");
            Console.Write("\nClient ID: ");
            string clientId = Console.ReadLine();
            Console.Write("\nBot token: ");
            string botToken = Console.ReadLine();

            List<string> ownerIds = new List<string>();

            Console.WriteLine("\nEnter to confirm, leave blank to end");
            int counter = 0;
            while (true)
            {
                counter++;

                Console.Write($"Owner {counter} ID: ");
                string OwnerId = Console.ReadLine();
                if (OwnerId != "")
                    ownerIds.Add(OwnerId);
                else
                    break;
            }

            Console.WriteLine("\nGenerating empty configuration file in executable folder...");
            WriteToFile(GenerateJson(clientId, botToken, ownerIds));
            Console.WriteLine("Done, press enter to exit.");
            Console.ReadKey();
        }
    }
}
