using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RiotSharp;
using System.Threading.Tasks;
using tenebot.Services;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace tenebot.Modules.RiotAPI
{
    public static class RIOTAPI
    {

        /// <summary>
        /// API Key, taken from configuration.json
        /// </summary>
        public static RiotApi api = RiotApi.GetDevelopmentInstance("RGAPI-56c5951e-d246-4440-91fe-88e23252b47e");
        /// <summary>
        /// Static API, for champion names. Taken from configuration.json.
        /// </summary>
        public static StaticRiotApi staticapi = StaticRiotApi.GetInstance("RGAPI-56c5951e-d246-4440-91fe-88e23252b47e");

        public static string currentVersion = staticapi.GetVersions(RiotSharp.Misc.Region.euw).First();
        
        


    }
}
