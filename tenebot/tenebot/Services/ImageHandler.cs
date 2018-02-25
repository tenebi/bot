using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tenebot.Services
{
    public static class ImageHandler
    {
        private static string imageBaseUrl = @"http://jovanzlatanovic.com/tenebot/";

        public static string RandomImageUrl(string folder)
        {
            Random rnd = new Random();
            int selected = rnd.Next(0, 10);
            return imageBaseUrl + selected.ToString() + ".jpg";
        }
    }
}
