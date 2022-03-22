using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;




namespace TooManyTabs.UI
{
    class TMTItemSlot : UIElement{

        public Item item;
        private float size;

        public TMTItemSlot(Item item,float size) : base()
        {
            this.item = item;
            this.size = size;
            Width = new StyleDimension(size, 0f);
            Height = new StyleDimension(size, 0f);
     
        }

        public void setItem(Item item)
        {
            this.item = item;
        }

       

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (IsMouseHovering)
            {
             
                ItemSlot.MouseHover(ref item,31);
            }
            
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            Texture2D texture = TextureAssets.Item[item.type].Value;

            Rectangle rect = base.GetDimensions().ToRectangle();
            Rectangle itemRect = texture.Bounds;

            itemRect.X = (int)(rect.X + (size - itemRect.Width)/2);
            itemRect.Y = (int)(rect.Y + (size - itemRect.Height)/2);

            Main.NewText(itemRect.Width);
            Main.instance.LoadItem(item.type);
            spriteBatch.Draw(TextureAssets.InventoryBack.Value,rect, Color.White);
            spriteBatch.Draw(TextureAssets.Item[item.type].Value, itemRect, Color.White);
            
  
        }

    }
}
