using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using TooManyTabs.UI;
using Terraria.UI;
namespace TooManyTabs
{
     class TMTKeybindPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet) { 

            if (TooManyTabs.OpenKeybind.JustPressed)
            {
                if (!TooManyTabs.openedMenu)
                {
                    TMTSystem.searchMenu = new SearchMenu();

                    TMTSystem.menuUserInterface = new UserInterface();
                    TMTSystem.menuUserInterface.SetState(TMTSystem.searchMenu);

                    TMTSystem.searchMenu.Activate();
                    TooManyTabs.openedMenu = true;

                    Item empty = new Item(0);
                    Main.NewText(empty.Name);
                }

                SearchMenu.Visible = !SearchMenu.Visible;


                
            }
        }

        




    }
}
