using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyAdmin.MYGUI;
using UnityEngine;

namespace TinyAdmin.Patches
{
    [HarmonyPatch(typeof(QuickMenuManager))]
    internal class QuickMenuManagerPatch
    {

        [HarmonyPatch(nameof(QuickMenuManager.OpenQuickMenu))]
        [HarmonyPrefix]
        public static void openMenu()
        {

            Plugin.Log.LogInfo("opening menu");
            Plugin.Instance.adminMenuOpen = true;
        }

        [HarmonyPatch(nameof(QuickMenuManager.CloseQuickMenu))]
        [HarmonyPrefix]
        public static void closeMenu()
        {
            Plugin.Log.LogInfo("closing menu");
            Plugin.Instance.adminMenuOpen = false;

        }

        [HarmonyPatch(nameof(QuickMenuManager.LeaveGameConfirm))]
        [HarmonyPrefix]
        public static void destroyMenuOnLeave()
        {
            Plugin.Instance.canOpenAdminMenu = false;
            Plugin.Instance.adminMenuOpen = false;
        }
    }
}
