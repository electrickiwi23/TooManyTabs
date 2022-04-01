using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI.Chat;
using System.Collections.Generic;




namespace TooManyTabs.UI
{
    class TMTItemSlot : UIElement{

        public Item item;
        private float size;
        private string forceCount;
        private List<Item> shiftItems;
        private int currentIndex = 0;
        private bool showBackground = true;
        

        public TMTItemSlot(Item item,float size) : base()
        {
            this.item = item;
            this.size = size;
            Width = new StyleDimension(size, 0f);
            Height = new StyleDimension(size, 0f);
     
        }
           public TMTItemSlot(List<Item> items,float size) : base()
        {
            this.item = items[0];
            this.size = size;
            shiftItems = items;
            Width = new StyleDimension(size, 0f);
            Height = new StyleDimension(size, 0f);
     
        }

        public void setItem(Item item)
        {
            this.item = item;
        }

        public void setCount(string Count){
            forceCount = Count;
        }

        public void setShowBackground(bool show){
            showBackground = show;
        }

        

       

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
             
                ItemSlot.MouseHover(ref item,31);
            }
            if (shiftItems!=null){

                if(Main.essScale==1f){
                    currentIndex++;
                    if (currentIndex==shiftItems.Count) currentIndex =0;
                    item = shiftItems[currentIndex];
                }

            }

            Main.inventoryScale = 1;
            
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            Texture2D texture = TextureAssets.Item[item.type].Value;
            Rectangle rectangle2 = (Main.itemAnimations[item.type] == null) ? texture.Frame() : Main.itemAnimations[item.type].GetFrame(texture);

            Rectangle rect = base.GetDimensions().ToRectangle();
            Rectangle itemRect = texture.Bounds;

            float num8 = size/52;

            if (rectangle2.Width > 32 || rectangle2.Height > 32)
				num8 = ((rectangle2.Width <= rectangle2.Height) ? (32f / (float)rectangle2.Height) : (32f / (float)rectangle2.Width));



            itemRect.X = (int)(rect.X + (size - itemRect.Width)/2);
            itemRect.Y = (int)(rect.Y + (size - itemRect.Height)/2);
            float scale = 1;
            Color currentColor = Color.White;
            ItemSlot.GetItemLight(ref currentColor, ref scale, item);
            Vector2 vector = TextureAssets.InventoryBack.Value.Size() * Main.inventoryScale;
            
            Vector2 origin = rectangle2.Size() * (scale / 2f - 0.5f);          
            Vector2 position2 = position +  vector/ 2f - rectangle2.Size() * num8 / 2f;  

            Main.instance.LoadItem(item.type);
            if(showBackground)
            spriteBatch.Draw(TextureAssets.InventoryBack.Value,rect, Color.White);
            spriteBatch.Draw(texture, position2 ,rectangle2, item.GetAlpha(currentColor),0f,origin,scale*num8,SpriteEffects.None, 0f);
            	if (item.color != Color.Transparent) {
					Color newColor = Color.White;
					// Extra context.

					spriteBatch.Draw(texture, position2, rectangle2, item.GetColor(newColor), 0f, origin, num8 * scale, SpriteEffects.None, 0f);
				}
            if (item.stack > 1||(forceCount!=null&&!forceCount.Equals("1"))){
                if (forceCount==null) forceCount = item.stack.ToString();

			    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, forceCount, position + new Vector2(10f, 26f) * Main.inventoryScale, Color.White, 0f, Vector2.Zero, new Vector2(Main.inventoryScale), -1f, Main.inventoryScale);
            }

  
        }

    }
}
