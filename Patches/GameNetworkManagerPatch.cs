using HarmonyLib;
using Steamworks;
using Steamworks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyAdmin.MYGUI;
using UnityEngine;

namespace TinyAdmin.Patches
{
    [HarmonyPatch(typeof(GameNetworkManager))]
    internal class GameNetworkManagerPatch
    {

        [HarmonyPatch("SteamMatchmaking_OnLobbyCreated")]
        [HarmonyPrefix]
        public static void lobbyCreatedPatch()
        {
            GameObject currentGUI = new UnityEngine.GameObject("AdminGUI");
            UnityEngine.Object.DontDestroyOnLoad(currentGUI);
            currentGUI.hideFlags = HideFlags.HideAndDontSave;
            currentGUI.AddComponent<AdminMenu>();
            Plugin.myGUI = (AdminMenu)currentGUI.GetComponent("AdminGUI");

            Plugin.Log.LogInfo("GUI Created");

        }

        [HarmonyPatch(nameof(GameNetworkManager.LeaveCurrentSteamLobby))]
        [HarmonyPostfix]
        public static void lobbyDestroyPatch()
        {
            // couldn't find anything good to patch in the game network manager //

            // this does not work here and will prevent the user from quitting

            //Plugin.Log.LogInfo("Disconnecting, GUI Destroyed");
            
            /*
            if(Plugin.myGUI != null)
            {
                GameObject.Destroy(Plugin.myGUI.gameObject);
            }
            */


        }
    }

}
