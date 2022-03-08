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
        internal UserInterface menuUserInterface;
        internal SearchMenu searchMenu;

        public override void OnWorldLoad()
        {
            if (!Main.dedServ)
            {


                searchMenu = new SearchMenu();
                searchMenu.Activate();
                menuUserInterface = new UserInterface();
                menuUserInterface.SetState(searchMenu);



            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (SearchMenu.Visible)
            {
                Main.NewText("egg");
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
							menuUserInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
			}

		}
	}
}

