using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameJam2022
{
    public class MiniMap
    {
        public Vector2 Position;

        //private Vector2 position;
        //private Texture2D texture;
        private Texture2D texture;

        public int[,] Map;
        // 0 = Water    Blue
        // 1 = Ground   Brown
        // 2 = Grass    Green
        // 3 = Street   Black
        // 4 = Building Gold
        // 5 = Main P   Purple
        // 6 = Enemy    Red
        // 7 = Pickup   White

        private int worldX;
        private int worldY;
        private float scale = 4;

        /// <summary>
        /// Creates a new tile
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public MiniMap(Vector2 position, int worldWidth, int worldHeight)
        {
            this.Position = position;
            worldX = worldWidth;
            worldY = worldHeight;
            Map = new int[worldX, worldY];
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Pixel");
        }

        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < worldX; x++)
            {
                for (int y = 0; y < worldY; y++)
                {
                    switch (Map[x, y])
                    {
                        case 0:
                            //Water
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Blue, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 1:
                            //Ground
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Brown, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 2:
                            //Grass
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Green, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 3:
                            //Street
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Black, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 4:
                            //Building
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Gold, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 5:
                            //Main Player
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Purple, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 6:
                            //Enemy
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.Red, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                        case 7:
                            //Pickup
                            spriteBatch.Draw(texture, Position + new Vector2(x * scale, y * scale), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                            break;
                    }
                }
            }

        }
    }
}