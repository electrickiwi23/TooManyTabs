using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using System.Collections.Generic;

namespace TooManyTabs.UI
{
    class SearchMenu : UIState
    {
        public TestButton playButton;
        public DragableUIPanel panel;
		private UISearchBar searchBar;
		private UIPanel searchBox;
		private UIScrollbar scrollbar;
		private bool clickedSearch = false;
		private bool clickedAnything = false;
		private List<UIItemIcon> itemIcons;
		
		private UIDynamicItemCollection grid;

		TMTItemSlot slot = null;

		public static bool Visible = false;

		public override void OnInitialize()
		{

			// Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
			panel = new DragableUIPanel();
			panel.SetPadding(8);
			// We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(panel);`. 
			// This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
			panel.Left.Set(400f, 0f);
			panel.Top.Set(100f, 0f);
			panel.Width.Set(358f, 0f);
			panel.Height.Set(540f, 0f);
			panel.BackgroundColor = new Color(0, 75, 156,160);
			Append(panel);

			searchBox = new UIPanel();
			searchBox.SetPadding(2);
			searchBox.Left.Set(0, 0f);
			searchBox.Top.Set(0, 0f);
			searchBox.Height.Set(28, 0f);
			searchBox.Width.Set(200, 0f);

			panel.Append(searchBox);

			searchBar = new UISearchBar(Language.GetText("SearchItems"), 1);
			searchBar.Height.Set(28, 0f);
			searchBar.Width.Set(268, 0f);
			searchBar.HAlign = 0f;
			searchBar.VAlign = 0.5f;
			searchBox.OnClick += OnSearchClicked;
			searchBar.OnContentsChanged += OnSearchContentsChanged;
			searchBox.Append(searchBar);
			

			searchBar.OnStartTakingInput += OnStartTakingInput;
			searchBar.OnEndTakingInput += OnEndTakingInput;



			UIPanel resultsPanel = new UIPanel();
			resultsPanel.SetPadding(2);
			resultsPanel.Top.Set(32, 0f);
			resultsPanel.Width.Set(280,0f);
			resultsPanel.Height.Set(392,0f);
			resultsPanel.BackgroundColor = new Color(1,1,1,0);
			
			panel.Append(resultsPanel);


			scrollbar = new UIScrollbar();
			scrollbar.Top.Set(36, 0f);
			scrollbar.HAlign = 1f;
			scrollbar.Height.Set(384,0f);
			

			panel.Append(scrollbar);
			
			Asset<Texture2D> clearButtonTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/SearchCancel");
			UIImageButton clearButton = new UIImageButton(clearButtonTexture);
			clearButton.HAlign = 1f;
			clearButton.VAlign = 0f;
			clearButton.Height.Set(24, 0f);
			clearButton.Width.Set(24, 0f);
			clearButton.OnClick += OnClearClicked;

			searchBox.Append(clearButton);

			grid = new UIDynamicItemCollection();
			grid.Width.Set(268,0f);
			grid.Height.Set(392,0f);
		


			UIList list = new UIList();
			list.Add(grid);
			list.SetScrollbar(scrollbar);
			list.Width.Set(0,1f);
			list.Height.Set(0,1f);
			

			resultsPanel.Append(list);









			searchBar.SetContents("");

			base.OnInitialize(); 

		}

		private List<int> UpdateSearchCriteria(string name){
			List<Item> allItems = TooManyTabs.GetAllItems();
			bool empty = name.Equals("");
		
			List<int> filteredItems = new List<int>();
			foreach (Item item in allItems)
            {
				if (empty||item.Name.ToLower().Contains(name.ToLower())) {
					filteredItems.Add(item.type);
					
                }
            }
			
		
	
			return filteredItems;

        }

		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{ 
			SoundEngine.PlaySound(SoundID.MenuClose);
			Visible = false;
		}

		private void OnSearchClicked(UIMouseEvent evt,UIElement listeningElement)
        {
			if (evt.Target.Parent != searchBox)
			{
				searchBar.ToggleTakingText();
				clickedSearch = true;
			}

		}

		private void OnClearClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			searchBar.SetContents("");
		}

		private void OnStartTakingInput()
		{
			searchBox.BorderColor = Main.OurFavoriteColor;
		}

		private void OnEndTakingInput()
		{
			searchBox.BorderColor = new Color(35, 40, 83);
		}

		public void AttemptStoppingUsingSearchbar(UIMouseEvent evt)
        {
			clickedAnything = true;
        }

		private void OnSearchContentsChanged(string contents)
		{

			List<int> items = UpdateSearchCriteria(contents);
			grid.SetContentsToShow(items);
			

		}

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			AttemptStoppingUsingSearchbar(evt);
		}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
			if (clickedAnything && !clickedSearch && searchBar.IsWritingText)
            {
				searchBar.ToggleTakingText();
            }
			clickedAnything = false;
			clickedSearch = false;

        }

   

    }
}
