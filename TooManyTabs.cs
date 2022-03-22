using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using TooManyTabs.UI;
using System.Collections.Generic;


namespace TooManyTabs
{
    public class TooManyTabs : Mod
    {
        

        public static ModKeybind OpenKeybind;
        public static bool openedMenu;
  

        public override void Load()
        {


            OpenKeybind = KeybindLoader.RegisterKeybind(this, "Open Item Search", Microsoft.Xna.Framework.Input.Keys.P);
            ModTranslation translation = LocalizationLoader.CreateTranslation("SearchItems");
            translation.AddTranslation(1, "Item Name:");
            LocalizationLoader.AddTranslation(translation);



        }

        public static void OpenMenu()
        {
            SearchMenu.Visible = true;





        }

        public static List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                Item item = new Item(i);
                if(!item.Name.Equals(""))
                items.Add(item);
                
            }

            return items;
                    

        }
    }
}


        