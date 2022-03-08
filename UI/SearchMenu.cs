using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;

namespace TooManyTabs.UI
{
    class SearchMenu : UIState
    {
        public TestButton playButton;
        public DragableUIPanel panel;
		public static bool Visible = false;

		public override void OnInitialize()
		{
			// Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
			panel = new DragableUIPanel();
			panel.SetPadding(0);
			// We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(panel);`. 
			// This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
			panel.Left.Set(400f, 0f);
			panel.Top.Set(100f, 0f);
			panel.Width.Set(170f, 0f);
			panel.Height.Set(70f, 0f);
			panel.BackgroundColor = new Color(73, 94, 171);

			Asset<Texture2D> test = ModContent.Request<Texture2D>("TooManyTabs/Assets/testButton");

			UIImageButton playButton = new UIImageButton(test);

			playButton.Left.Set(140, 0f);
            playButton.Top.Set(10, 0f);
			playButton.Width.Set(22, 0f);
			playButton.Height.Set(22, 0f);
			playButton.OnClick += new MouseEvent(CloseButtonClicked);
			playButton.Append(panel);
			// As a recap, ExampleUI is a UIState, meaning it covers the whole screen. We attach panel to ExampleUI some distance from the top left corner.AssetRequestMode.ImmediateLoad
			// We then place playButton, closeButton, and moneyDiplay onto panel so we can easily place these UIElements relative to panel.ss
			// Since panel will move, this proper organization will move playButton, closeButton, and moneyDiplay properly when panel moves.
		}

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{ 
			SoundEngine.PlaySound(SoundID.MenuClose);
			Visible = false;
		}
		
		
    }
}
