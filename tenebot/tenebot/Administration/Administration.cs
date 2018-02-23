using System;
using tenebot.Services;

namespace tenebot.Administration
{
    public static class Administration
    {
        /// <summary>
        /// Checks if the id is an OwnerId in the configuration.json.
        /// </summary>
        /// <param name="id">Discord user id.</param>
        /// <returns>If the user is an owner.</returns>
        public static bool CheckIsOwner(string id)
        {
            foreach (string ownerid in Settings.OwnerIds)
            {
                if (id == ownerid)
                    return true;
            }

            return false;
        }
    }
}
