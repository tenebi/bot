using System;

namespace tenebot.Services
{
    public static class ImageHandler
    {
        private static string imageBaseUrl = Settings.BaseHostUrl;

        /// <summary>
        /// Returns a random image from the server with a proper url path, also checks if its a jpg, png or gif.
        /// </summary>
        /// <param name="folder">Folder on the server containing number named images.</param>
        /// <param name="max">Maximum random number result.</param>
        /// <param name="min">Minimum random number result.</param>
        /// <returns>Verified image url.</returns>
        public static string RandomImageUrl(string folder, int max, int min = 1)
        {
            Random rnd = new Random();
            int selected = rnd.Next(min, max);

            string url = imageBaseUrl + folder + selected.ToString();

            if (Debugging.CheckHttpReachable(url + ".jpg"))
                return url + ".jpg";
            else if (Debugging.CheckHttpReachable(url + ".png"))
                return url + ".png";
            else if (Debugging.CheckHttpReachable(url + ".gif"))
                return url + ".gif";
            else
            {
                Debugging.Log("RandomImageUrl", "Failed to find image url");
                return "Failed to get url";
            }
        }
    }
}
