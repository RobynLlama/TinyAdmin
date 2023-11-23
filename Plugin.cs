using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using TinyAdmin.MYGUI;
using TinyAdmin.Patches;
using UnityEngine;

namespace TinyAdmin
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        public static ManualLogSource Log;

        internal static GameObject myGUIObject;

        internal static Plugin Instance;

        internal bool adminMenuOpen = false;
        internal bool canOpenAdminMenu = false;
        internal bool menuExists = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Log = Logger;
            harmony.PatchAll(typeof(GameNetworkManagerPatch));
            harmony.PatchAll(typeof(QuickMenuManagerPatch));

        }

    }
}
