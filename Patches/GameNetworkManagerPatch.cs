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
            Plugin.myGUIObject = new UnityEngine.GameObject("AdminGUI");
            UnityEngine.Object.DontDestroyOnLoad(Plugin.myGUIObject);
            Plugin.myGUIObject.hideFlags = HideFlags.HideAndDontSave;
            Plugin.myGUIObject.AddComponent<AdminMenu>();
            Plugin.myGUI = (AdminMenu)Plugin.myGUIObject.GetComponent("AdminGUI");
            Plugin.Instance.canOpenAdminMenu = true;
            

            Plugin.Log.LogInfo("GUI Created");

        }
    }

}
