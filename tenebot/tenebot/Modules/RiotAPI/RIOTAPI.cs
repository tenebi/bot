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
        public static RiotApi api = RiotApi.GetDevelopmentInstance("RGAPI-dd771ff5-5492-407b-b4d5-214937e6ccb0");
        /// <summary>
        /// Static API, for champion names. Taken from configuration.json.
        /// </summary>
        public static StaticRiotApi staticapi = StaticRiotApi.GetInstance("RGAPI-dd771ff5-5492-407b-b4d5-214937e6ccb0");


    }
}
