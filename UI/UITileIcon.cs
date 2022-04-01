using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.UI.Chat;
using System.Collections.Generic;
namespace TooManyTabs.UI
{
    class UITileIcon : UIElement{
        int tile;
        List<int> cycleTiles; 
        public UITileIcon(int tile): base() {
            this.tile = tile;
        }
        public UITileIcon(List<int> tiles): base() {
            this.cycleTiles = tiles;
            tile = tiles[0];
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
          
            Rectangle rect = base.GetDimensions().ToRectangle();
        
            if (!TextureAssets.Tile[tile].IsLoaded)
				Main.instance.LoadTiles(tile); 
            
            Main.NewText(TextureAssets.Tile[tile].Height());
           
             
    
            Texture2D texture2D = Main.instance.TilePaintSystem.TryGetTileAndRequestIfNotReady(tile,1,0);
        
            
            
            Texture2D texture = TextureAssets.Tile[tile].Value;
            Rectangle itemRect = texture.Bounds;
            itemRect.X = (int)(rect.X);
            itemRect.Y = (int)(rect.Y);
            spriteBatch.Draw(texture,itemRect,Color.White);
        }
        
    }
}