using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameJam2022.Collisions;

namespace GameJam2022
{
    public class TileMapBackground
    {
        public int imageNumber { get; set; } = 0;
        private Vector2 position;
        //private Texture2D texture;
        private Texture2D texture00;
        private Texture2D texture01;
        private Texture2D texture1;
        private Texture2D texture2;
        private Texture2D texture3;
        private double animationTimer;

        private BoundingRectangle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        private bool animationPose = false;
        /// <summary>
        /// Creates a new tile
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public TileMapBackground(Vector2 position, int imgNum)
        {
            this.position = position;
            this.imageNumber = imgNum;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture00 = content.Load<Texture2D>("Water1");
            texture01 = content.Load<Texture2D>("Water2");
            texture1 = content.Load<Texture2D>("Ground");
            texture2 = content.Load<Texture2D>("Grass");
            texture3 = content.Load<Texture2D>("Street");
            this.bounds = new BoundingRectangle(position, texture00.Width, texture00.Height);
        }

        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(imageNumber == 0)
            {
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (animationTimer > 0.2)
                {
                    animationPose = !animationPose;
                    animationTimer -= 0.2;
                }
                if (animationPose)
                {
                    spriteBatch.Draw(texture00, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(texture01, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
            }
            else if(imageNumber == 1)
            {
                spriteBatch.Draw(texture1, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
            else if(imageNumber == 2)
            {
                spriteBatch.Draw(texture2, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
            else if (imageNumber == 3)
            {
                spriteBatch.Draw(texture3, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
