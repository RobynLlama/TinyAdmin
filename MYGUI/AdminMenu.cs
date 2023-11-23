using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TinyAdmin.MYGUI
{
    internal class AdminMenu : MonoBehaviour
    {
        private bool isMenuOpen;
        private bool wasKeyUp;
        private KeyboardShortcut openCloseMenu;

        private float MENUWIDTH = 800;
        private float MENUHEIGHT = 400;
        private float MENUX;
        private float MENUY;
        private float ITEMWIDTH = 300;
        private float CENTERX;
        private float scrollStart;

        private GUIStyle menuStyle;
        private GUIStyle kickButtonStyle;
        private GUIStyle labelStyle;
        private GUIStyle hScrollStyle;
        private GUIStyle vScrollStyle;

        private Vector2 scrollPosition;

        private void Awake()
        {
            isMenuOpen = false;
            wasKeyUp = true;
            openCloseMenu = new KeyboardShortcut(KeyCode.Escape);
            MENUWIDTH = Screen.width / 6;
            MENUHEIGHT = Screen.width / 4;
            ITEMWIDTH = MENUWIDTH / 1.2f;

            // this is center at center of menu
            //MENUX = (Screen.width / 2) - (MENUWIDTH / 2);

            // this is center at left side of menu
            //MENUX = (Screen.width / 2);

            // this is right off the edge of the screen on the right side
            MENUX = Screen.width - MENUWIDTH;
            MENUY = (Screen.height / 2) - (MENUHEIGHT / 2);
            CENTERX = MENUX + ((MENUWIDTH / 2) - (ITEMWIDTH / 2));
            scrollStart = MENUY + 30;


        }

        private void Update()
        {
            if (openCloseMenu.IsUp())
            {
                if (!wasKeyUp)
                {
                    wasKeyUp = true;
                }

            }
            if (openCloseMenu.IsDown())
            {
                if (wasKeyUp)
                {
                    wasKeyUp = false;
                    isMenuOpen = !isMenuOpen;
                }
            }
           
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        private void IntitializeMenu()
        {
            if (menuStyle == null)
            {
                menuStyle = new GUIStyle(GUI.skin.box);
                kickButtonStyle = new GUIStyle(GUI.skin.button);
                labelStyle = new GUIStyle(GUI.skin.label);
                hScrollStyle = new GUIStyle(GUI.skin.horizontalScrollbar);
                vScrollStyle = new GUIStyle(GUI.skin.verticalScrollbar);

                menuStyle.normal.textColor = Color.white;
                menuStyle.normal.background = MakeTex(2, 2, new Color(0.01f, 0.01f, 0.1f, .9f));
                menuStyle.fontSize = 18;
                menuStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;

                kickButtonStyle.normal.textColor = Color.white;
                kickButtonStyle.normal.background = MakeTex(2, 2, new Color(0.3f, 0.01f, 0.1f, .9f));
                kickButtonStyle.hover.background = MakeTex(2, 2, new Color(0.4f, 0.01f, 0.1f, .9f));
                kickButtonStyle.fontSize = 22;
                kickButtonStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;

                labelStyle.normal.textColor = Color.white;
                labelStyle.normal.background = MakeTex(2, 2, new Color(0.01f, 0.01f, 0.1f, 0f));
                labelStyle.fontSize = 24;
                labelStyle.alignment = TextAnchor.MiddleCenter;
                labelStyle.normal.background.hideFlags = HideFlags.HideAndDontSave;

                hScrollStyle.normal.background = MakeTex(2, 2, new Color(0.01f, 0.01f, 0.1f, 0f)); ;


            }
        }

        private void OnGUI()
        {
            if(menuStyle == null) { IntitializeMenu(); }
            
            if(isMenuOpen)
            {
                GUI.Box(new Rect(MENUX, MENUY, MENUWIDTH, MENUHEIGHT), "TinyAdmin Kick Menu", menuStyle);
                scrollPosition = GUI.BeginScrollView(new Rect(MENUX, MENUY + 30, MENUWIDTH, MENUHEIGHT - 50), scrollPosition, new Rect(MENUX, scrollStart, ITEMWIDTH, AdminTools.GetAllPlayers().Count * 30), false, true, hScrollStyle, vScrollStyle);
                try
                {

                    foreach (var player in AdminTools.GetAllPlayers())
                    {
                        if (GUI.Button(new Rect(CENTERX, MENUY + 30 + (player.Key * 30), ITEMWIDTH, 30), player.Value))
                        {
                            AdminTools.KickBanByID(player.Key);
                        }
                    }
                }
                catch (Exception e)
                {
                    Plugin.Log.LogInfo(e.Message);
                }

                // End the scroll view that we began above.
                GUI.EndScrollView();
            }

            

        }


    }
}
