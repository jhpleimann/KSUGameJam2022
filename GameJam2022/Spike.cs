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
    public class Spike
    {
        /// <summary>
        /// The position of the player
        /// </summary>
        public Vector2 Position;

        private Texture2D texture;

        private BoundingRectangle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Creates a new tile
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public Spike(Vector2 position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Spike");
            this.bounds = new BoundingRectangle(Position, texture.Width, texture.Height);
        }

        /// Draws the animated sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color.Red, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
        }
    }
}
