using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using TooManyTabs.UI;


namespace TooManyTabs
{
    public class TooManyTabs : Mod
    {


        public static ModKeybind OpenKeybind;


        public override void Load()
        {
            OpenKeybind = KeybindLoader.RegisterKeybind(this, "Open Item Search", Microsoft.Xna.Framework.Input.Keys.P);

       
        }

        public static void OpenMenu()
        { 
            SearchMenu.Visible = true;
           




        }

  
		}
    }


        