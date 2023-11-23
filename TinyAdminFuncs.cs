using GameNetcodeStuff;
using System.Collections.Generic;

namespace TinyAdmin
{
    public class AdminTools
    {
        public static Dictionary<int, string> GetAllPlayers()
        {

            int player_id = 0;
            Dictionary<int, string> pList = new();

            foreach (PlayerControllerB player in StartOfRound.Instance.allPlayerScripts)
            {
                //This was for debugging, hitting the log this much in one frame might be too much
                //Plugin.Log.LogInfo($"[{player_id}] {player.playerUsername}");

                
                pList.Add(player_id, player.playerUsername);
                player_id++;
            }

            return pList;
        }

        public static void KickBanByID(int ID)
        {
            Plugin.Log.LogInfo($"Kicking player: {ID}");

            /*
                Its that shrimple, dude
                There is another function that runs on top of this one in the base game
                but it checks if player ID is between 1 and 3 (which is all players but the host)
                So its possible for the host to ban themselves if I don't check again here
            */

            if (ID == 0)
                return;

            StartOfRound.Instance.KickPlayer(ID);
        }

        public static void KickBanByName(string Name)
        {
            //This is untested and requires the username to match exactly
            //Not really ready for production
            int player_id = 0;

            foreach (PlayerControllerB player in StartOfRound.Instance.allPlayerScripts)
            {
                if (player.playerUsername == Name)
                {
                    KickBanByID(player_id);
                }
                player_id++;
            }
        }
    }
}