using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using System.Collections.Generic;
using TooManyTabs.UI;
using Terraria.GameContent.ItemDropRules;
using System;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;


namespace TooManyTabs.UI
{
    class RecipeMenu : UIState
    {

        bool usage;
        Item item;
        DragableUIPanel panel;
        UIPanel resultsPanel;
        UIList list;
        UISearchBar searchBar;
        UIPanel searchBox;

        private bool clickedSearch = false;
		private bool clickedAnything = false;
        public RecipeMenu(Item item,bool usage, Vector2 location){
            this.usage  = usage;
            this.item = item;
            
            panel = new DragableUIPanel();
            panel.Left.Set(location.X, 0f);
			panel.Top.Set(location.Y, 0f);

            searchBar = new UISearchBar(Language.GetText("SearchItems"), 1);
            searchBar.SetContents(SearchMenu.currentSearch);
            
     
	

        }

        	public override void OnInitialize()
		{
            panel.SetPadding(8);
			// We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(panel);`. 
			// This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
	
			panel.Width.Set(366f, 0f);
			panel.Height.Set(540f, 0f);
			panel.BackgroundColor = new Color(0, 75, 156,160);
			panel.BackgroundColor = new Color(0, 75, 156,160);
			Append(panel);

			searchBox = new UIPanel();
			searchBox.SetPadding(2);
			searchBox.Left.Set(0, 0f);
			searchBox.Top.Set(0, 0f);
			searchBox.Height.Set(28, 0f);
			searchBox.Width.Set(200, 0f);

			panel.Append(searchBox);

			searchBar.Height.Set(28, 0f);
			searchBar.Width.Set(268, 0f);
			searchBar.HAlign = 0f;
			searchBar.VAlign = 0.5f;
			searchBox.OnClick += OnSearchClicked;
			searchBar.OnContentsChanged += OnSearchContentsChanged;
			searchBox.Append(searchBar);
            
			searchBar.OnStartTakingInput += OnStartTakingInput;
			searchBar.OnEndTakingInput += OnEndTakingInput;

            resultsPanel = new UIPanel();
			resultsPanel.SetPadding(8);
			resultsPanel.Top.Set(32, 0f);
			resultsPanel.Width.Set(324,0f);
			resultsPanel.Height.Set(492,0f);
			resultsPanel.BackgroundColor = new Color(1,1,1,0);

            panel.Append(resultsPanel);

            UIScrollbar scrollbar = new UIScrollbar();
            scrollbar.Top.Set(36, 0f);
			scrollbar.HAlign = 1f;
			scrollbar.Height.Set(484,0f);
            panel.Append(scrollbar);

            list = new UIList();
            list.SetScrollbar(scrollbar);
            list.Width.Set(0,1f);
			list.Height.Set(0,1f);
            
            resultsPanel.Append(list);
            
                  
            for (int i = 0; i < Main.recipe.Length; i++) {
                Recipe recipe = Main.recipe[i];
                Item result;
                if (usage){
                    recipe.TryGetIngredient(item.type,out result);
                    if (item.type==ItemID.LeadBar&&recipe.acceptedGroups.Contains(RecipeGroupID.IronBar)){
                        recipe.TryGetIngredient(ItemID.IronBar,out result);
                    }
                }
                else
                recipe.TryGetResult(item.type,out result);
            
                if (result!=null){

                    UIPanel recipePanel = new UIPanel();
                    recipePanel.Width.Set(0,1f);
                    recipePanel.HAlign = .5f;
                    recipePanel.Height.Set(8+48*(MathF.Ceiling((recipe.requiredItem.Count)/3.0f)),0f);
                    recipePanel.SetPadding(6);
                    int j = 0;
                   
                    foreach(Item ingredient in recipe.requiredItem){
                        
                        TMTItemSlot slot;
                       
                        if(ingredient.type==ItemID.IronBar&&recipe.acceptedGroups.Contains(RecipeGroupID.IronBar)){
                             slot = new TMTItemSlot(new List<Item>{ingredient,new Item(ItemID.LeadBar,ingredient.stack)},44);
                        }else slot = new TMTItemSlot(ingredient,44);
                        slot.Top.Set(48*(j/3),0f);
                        slot.Left.Set(48*(j%3),0f);
                        slot.OnClick += OnItemClicked;
                        slot.OnRightClick += OnItemRightClicked;
                        
                        j++;
                        recipePanel.Append(slot);
                    }
                    TMTItemSlot finalSlot = new TMTItemSlot(recipe.createItem,44);
                    finalSlot.HAlign = 1f;
                    finalSlot.VAlign = .5f;
                    finalSlot.OnClick += OnItemClicked;
                    finalSlot.OnRightClick += OnItemRightClicked;
                    recipePanel.Append(finalSlot);
                    
                    
                    
                    if (recipe.requiredTile!=null){
                        if (recipe.requiredTile.Count!=0){
                            
                
                /*
                        UITileIcon tileSlot = new UITileIcon(recipe.requiredTile);
                        tileSlot.VAlign = .5f;
                        tileSlot.HAlign = 1f;
                        tileSlot.Left.Set(-58,0f);
                        recipePanel.Append(tileSlot);
                        */
                        
                        
                        }
                    }
                    
                    
    
             
                    
                    list.Add(recipePanel);
                }
            
                  
            }
            
            if (!usage){
            ItemDropDatabase database = Main.ItemDropsDB;
            IDictionary<UINpcIcon,DropRateInfo> added = new Dictionary<UINpcIcon,DropRateInfo>();
            foreach (KeyValuePair<int, NPC> pair in ContentSamples.NpcsByNetId){

                List<IItemDropRule> drops = database.GetRulesForNPCID(pair.Key,false);
                DropRateInfoChainFeed ratesInfo = new DropRateInfoChainFeed(1f);
                List<DropRateInfo> dropList = new List<DropRateInfo>();
                foreach(IItemDropRule rule in drops){
                    rule.ReportDroprates(dropList,ratesInfo);
                }              

                foreach(DropRateInfo info in dropList){
                    int drop = info.itemId;
                    if (drop==item.type){
                        bool combined = false;
                        foreach(UINpcIcon icon in added.Keys){
                            if (info.Equals(added[icon])&&pair.Value.TypeName==icon.npc.TypeName){
                                icon.addNPC(pair.Value);
                                combined = true;
                            }
                        }
                        if (!combined){
                        

                        
                        UIPanel recipePanel = new UIPanel();
                        recipePanel.Width.Set(0,1f);
                        recipePanel.HAlign = .5f;
                        recipePanel.Height.Set(56,0f);
                        recipePanel.SetPadding(6);

                        

                        UIText chance = new UIText((info.dropRate * 100) + "%");
                        UIPanel textBox = new UIPanel();
                        textBox.Width.Set(44,0f);
                        textBox.Height.Set(44,0f);
                        textBox.VAlign = 0.5f;
                        textBox.Left.Set(-48,0f);
                        textBox.HAlign = 1f;
                        textBox.SetPadding(10);
                        chance.VAlign = chance.HAlign = .5f;
                        textBox.Append(chance);
                        recipePanel.Append(textBox);

                        
                     
            

                        TMTItemSlot slot = new TMTItemSlot(new Item(drop),44);
                        if(info.stackMin==info.stackMax){
                            slot.setCount(info.stackMax.ToString());
                        } else slot.setCount(info.stackMin + "-" + info.stackMax);
                        
                        
                    
                        slot.HAlign = 1f;
                    
                        slot.OnClick += OnItemClicked;
                        slot.OnRightClick += OnItemRightClicked;
                        recipePanel.Append(slot);
                        

                        

                        
                       
                        
                        UINpcIcon image = new UINpcIcon(pair.Value);
                        image.HAlign = .5f;
                        image.VAlign = .5f;
                        image.Height.Set(44,0f);
                        image.Width.Set(44,0f);
                     
                        added.Add(image,info);
                        recipePanel.Append(image);
                
                       
                        list.Add(recipePanel);
                        }
                    }
                }
            }
            }
            
           
            
        }

        private void OnStartTakingInput()
		{
			searchBox.BorderColor = Main.OurFavoriteColor;
		}

		private void OnEndTakingInput()
		{
			searchBox.BorderColor = new Color(35, 40, 83);
		}

        private void OnSearchClicked(UIMouseEvent evt,UIElement listeningElement)
        {
			if (evt.Target.Parent != searchBox)
			{
				searchBar.ToggleTakingText();
                clickedSearch = true;
			}

		}

        	private void OnSearchContentsChanged(string contents)
		{
			SearchMenu.currentSearch = contents;
            SearchMenu menu = TMTSystem.searchMenu;
            TMTSystem.menuUserInterface.SetState(menu);
            menu.searchBar.SetContents(SearchMenu.currentSearch);
            if(! menu.searchBar.IsWritingText) menu.searchBar.ToggleTakingText();
            menu.panel.Left.Set(panel.Left.Pixels, 0f);
			menu.panel.Top.Set(panel.Top.Pixels, 0f);
            menu.OnStartTakingInput();
            menu.clickedSearch = true;
      
		}

        private void OnItemClicked(UIMouseEvent evt,UIElement listeningElement)
		{
            Item clicked = ((TMTItemSlot) listeningElement).item;
            Vector2 location = new Vector2(panel.Left.Pixels,panel.Top.Pixels);
			TMTSystem.menuUserInterface.SetState(new RecipeMenu(clicked, false,location));
		}

        private void OnItemRightClicked(UIMouseEvent evt,UIElement listeningElement)
		{
            Item clicked = ((TMTItemSlot) listeningElement).item;
            Vector2 location = new Vector2(panel.Left.Pixels,panel.Top.Pixels);
			TMTSystem.menuUserInterface.SetState(new RecipeMenu(clicked, true,location));
		}

        public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
	        clickedAnything = true;
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