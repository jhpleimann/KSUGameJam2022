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
    public class Building

    {
        public int imageNumber { get; set; } = 0;
        private Vector2 position;
        public Vector2 Position => position;
        private Texture2D texturePopo;
        private Texture2D textureBurg;
        private Texture2D textureSala;
        private Texture2D texturePizz;
        private Texture2D textureClosed;
        private Texture2D healthTexture;
        private Rectangle healthRectangle;
        private double openTimer;

        public bool Closed { get; set; } = false;

        private BoundingRectangle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Creates a new building
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public Building(Vector2 position, int imgNum)
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
            texturePopo = content.Load<Texture2D>("PoliceStation");
            textureBurg = content.Load<Texture2D>("RestrauntBurger");
            textureSala = content.Load<Texture2D>("RestrauntSalad");
            texturePizz = content.Load<Texture2D>("RestrauntPizza");
            textureClosed = content.Load<Texture2D>("RestrauntClosed");
            healthTexture = content.Load<Texture2D>("Wait");
            healthRectangle = new Rectangle(0, 0, 100 - ((int) openTimer * 2), healthTexture.Height);
            this.bounds = new BoundingRectangle(position, texturePopo.Width, texturePopo.Height);
        }

        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            healthRectangle = new Rectangle(0, 0, 100 - ((int)openTimer * 2), healthTexture.Height);
            if (imageNumber == 0)
            {
                spriteBatch.Draw(texturePopo, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
            else if(!Closed)
            {
                if (imageNumber == 1)
                {
                    spriteBatch.Draw(textureBurg, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
                else if (imageNumber == 2)
                {
                    spriteBatch.Draw(textureSala, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
                else if (imageNumber == 3)
                {
                    spriteBatch.Draw(texturePizz, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
                }
            }
            else
            {
                openTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (openTimer > 50.0)
                {
                    openTimer -= 50.0;
                    Closed = false;
                }
                spriteBatch.Draw(healthTexture, position + new Vector2(0,-32), healthRectangle, Color.White);
                spriteBatch.Draw(textureClosed, position, null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
