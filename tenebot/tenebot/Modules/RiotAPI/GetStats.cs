using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiotSharp;
using RiotSharp.Misc;
using System.Threading.Tasks;
using Discord;
using RiotSharp.Interfaces;

namespace tenebot.Modules.RiotAPI
{
    public class GetStats : ModuleBase<SocketCommandContext>
    {
        Region Region = new Region();
        
        EmbedBuilder helpEmbed = new EmbedBuilder()
            .WithTitle("Hmm, that region doesn't seem to exist!")
            .WithColor(Color.DarkRed)
            .WithDescription("Here, try these for regions:")
            .AddField("`EUW` `EUNE` `TR` `RU` `OCE` `NA` `KR` `JP` `LAN` `BR` `LAS`", "\n");
        EmbedBuilder errorEmbed = new EmbedBuilder()
            .WithTitle("Uh oh!")
            .WithColor(Color.DarkRed);
        EmbedBuilder statEmbed = new EmbedBuilder();
            


        [Command("lolstats")]
        public async Task getStats(string region, [Remainder] string summonerName)
        {
            switch (region.ToUpper())
            {
                case ("EUW"):
                    Region = Region.euw;
                    break;
                case ("EUNE"):
                    Region = Region.eune;
                    break;
                case ("TR"):
                    Region = Region.tr;
                    break;
                case ("RU"):
                    Region = Region.ru;
                    break;
                case ("OCE"):
                    Region = Region.oce;
                    break;
                case ("NA"):
                    Region = Region.na;
                    break;
                case ("KR"):
                    Region = Region.kr;
                    break;
                case ("JP"):
                    Region = Region.jp;
                    break;
                case ("LAN"):
                    Region = Region.lan;
                    break;
                case ("BR"):
                    Region = Region.br;
                    break;
                case ("LAS"):
                    Region = Region.las;
                    break;
                default:
                    await ReplyAsync("", false, helpEmbed.Build());
                    break;
            }
           


            try
            {

                var summoner = RIOTAPI.api.GetSummonerByName(Region, summonerName);
                var top3masteries = RIOTAPI.api.GetChampionMasteries(Region, summoner.Id).Take(3);

                
                
                foreach(var mastery in top3masteries)
                {
                    int champid = (int)mastery.ChampionId;
                    var champ = RIOTAPI.staticapi.GetChampion(Region, champid, RiotSharp.StaticDataEndpoint.ChampionData.All);
                    statEmbed.AddInlineField(champ.Name, mastery.ChampionPoints.ToString() + "mastery points");
                }

                statEmbed.WithTitle($"{summoner.Name}'s LoL statistics")
                    .AddInlineField("Total mastery score:", RIOTAPI.api.GetTotalChampionMasteryScore(Region, summoner.Id))
                    .WithThumbnailUrl("http://ddragon.leagueoflegends.com/cdn/"+ RIOTAPI.currentVersion +"/img/profileicon/" + summoner.ProfileIconId + ".png");



                await ReplyAsync("", false, statEmbed.Build());

            }
            catch(RiotSharpException e)
            {
                errorEmbed.Description = e.Message;
                await ReplyAsync("", false, errorEmbed.Build());

            }
        }

    }
}
