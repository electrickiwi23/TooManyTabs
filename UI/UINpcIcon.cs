using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;
using System.Collections.Generic;
using System;
using Terraria.GameInput;
using Terraria.UI.Gamepad;



namespace TooManyTabs.UI
{
    class UINpcIcon : UIElement
    {
        public NPC npc;
        List<NPC> cycleNPCs;
        int currentIndex = 0;
        public UINpcIcon(NPC npc){
            this.npc = (NPC) npc.Clone();
            this.npc.velocity = new Vector2(0,-1);
            this.npc.direction = 1;
            this.npc.directionY = 0;

          
            this.npc.IsABestiaryIconDummy = true;
            this.npc.scale = npc.frame.Height < 48 ? this.npc.scale : this.npc.scale * (48.0f /  this.npc.frame.Height);
    
  
            cycleNPCs = new List<NPC>();
            cycleNPCs.Add(this.npc);
        }

        public void addNPC(NPC npc){
            NPC addedNpc = (NPC) npc.Clone();
            addedNpc.direction = 1;
            addedNpc.directionY = 0;
            addedNpc.aiAction = 2;
            addedNpc.IsABestiaryIconDummy = true;
            addedNpc.scale = addedNpc.frame.Height < 48 ? addedNpc.scale : addedNpc.scale  * (48.0f / this.npc.frame.Height);
            cycleNPCs.Add(addedNpc);

        }
      
        protected override void DrawSelf(SpriteBatch spriteBatch) {

       
            if (cycleNPCs.Count!=1){

                if(Main.essScale==1f){
                    currentIndex++;
                    if (currentIndex==cycleNPCs.Count) currentIndex = 0;
                    npc = cycleNPCs[currentIndex];
                }

            }



            
            npc.frame.X = (int) Left.Pixels;;;
            npc.frame.Y = (int) Top.Pixels;
            if (npc.noGravity){
  
                Vector2 scaleOffset = new Vector2(0f, (1 - this.npc.scale) * this.npc.height/2.0f);
				npc.Center = GetDimensions().Center() - scaleOffset;
                
                
			}else {
                Vector2 location = GetDimensions().ToRectangle().BottomLeft();
                location.X += GetDimensions().Width / 2.0f;
			    npc.Bottom = location;
            }
            
			Main.instance.DrawNPCDirect(spriteBatch, npc, true, Vector2.Zero);
            
        }

    }
}