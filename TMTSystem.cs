using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using TooManyTabs.UI;


namespace TooManyTabs
{
    class TMTSystem : ModSystem
    {
        public static UserInterface menuUserInterface;
        public static SearchMenu searchMenu;
        GameTime _lastUpdatedGameTime;




        public override void OnWorldLoad()
        {
            if (!Main.dedServ)
            {
               


            }
        }

        

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdatedGameTime = gameTime;
    
            if (SearchMenu.Visible)
            {
         
                menuUserInterface?.Update(gameTime);
            }
            
  
        }

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"Too Many Tabs: Item Search",
					delegate {
						if (SearchMenu.Visible)
						{
                       
                            menuUserInterface.Draw(Main.spriteBatch, _lastUpdatedGameTime);
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
			}

		}
	}
}

