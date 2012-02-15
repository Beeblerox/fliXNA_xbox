using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace fliXNA_xbox
{
    public class FlxTileblock : FlxSprite
    {
        /// <summary>
        /// Creates a new rectangular FlxSprite object with specified position and size.
        /// Great for walls and floors.
        /// </summary>
        /// <param name="X">X position of the block</param>
        /// <param name="Y">Y position of the block</param>
        /// <param name="Width">Width of the block</param>
        /// <param name="Height">Height of the block</param>
        public FlxTileblock(float X, float Y, float Width, float Height)
            : base(X, Y)
        {
            makeGraphic(Width, Height, FlxColor.WHITE);
            active = false;
            immovable = true;
        }

        public FlxTileblock loadTiles(Texture2D TileGraphic, int TileWidth, int TileHeight, int Empties)
        {
            if (TileGraphic == null)
                return this;

            FlxSprite sprite = new FlxSprite().loadGraphic(TileGraphic, true, false, TileWidth, TileHeight);
            float spriteWidth = sprite.width;
            float spriteHeight = sprite.height;

            long total = sprite.frames + Empties;
            bool regen = false;

            if (width % sprite.width != 0)
            {
                width = (int)(width / spriteWidth + 1) * spriteWidth;
                regen = true;
            }
            	if(height % sprite.height != 0)
			{
				height = (int)(height/spriteHeight+1)*spriteHeight;
				regen = true;
			}
			if(regen)
				makeGraphic(width,height,new Color(0,0,0));
			else
				this.fill(new Color(0,0,0));

            int row = 0;
            int column;
            int destinationX;
            int destinationY = 0;
            int widthInTiles =(int)( width / spriteWidth );
            int heightInTiles = (int)(height / spriteHeight);

            while (row < heightInTiles)
            {
                destinationX = 0;
                column = 0;
                while (column < widthInTiles)
                {
                    if (FlxG.random() * total > Empties)
                    {
                        sprite.randomFrame();
                        sprite.drawFrame();
                        stamp(sprite, destinationX, destinationY);
                    }
                    destinationX += (int)spriteWidth;
                    column++;
                }
                destinationY += (int)spriteHeight;
                row++;
            }

            return this;

        }
    
    }


}
