
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using TooManyTabs.UI;


namespace TooManyTabs
{
     class TMTKeybindPlayer : ModPlayer
    {
        public override void  ProcessTriggers(TriggersSet triggersSet) { 

            if (TooManyTabs.OpenKeybind.JustPressed)
            {
                Main.NewText($"ExampleMod's ModKeybind was just pressed.");

                SearchMenu.Visible = !SearchMenu.Visible;

                Main.NewText(SearchMenu.Visible);


            }
        }

        




    }
}
