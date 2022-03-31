using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameJam2022
{
    public class MenuSelector
    {
        public bool IsSelected { get; set; } = false;
        private int imageNumber;
        private Texture2D textureSH;
        private Texture2D textureUH;
        private Texture2D textureSL;
        private Texture2D textureUL;
        private Texture2D textureSS;
        private Texture2D textureUS;


        private double animationTimer;

        private bool animationPose = false;

        public Vector2 Position;


        /// <summary>
        /// Creates a new Main Player
        /// </summary>
        /// <param name="position">The position of the player in the game</param>
        public MenuSelector(Vector2 position, int type)
        {
            this.Position = position;
            imageNumber = type;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            textureSH = content.Load<Texture2D>("SHOption");
            textureUH = content.Load<Texture2D>("UHOption");
            textureSL = content.Load<Texture2D>("SLOption");
            textureUL = content.Load<Texture2D>("ULOption");
            textureSS = content.Load<Texture2D>("SSOption");
            textureUS = content.Load<Texture2D>("USOption");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3)
            {
                animationPose = !animationPose;
                animationTimer -= 0.3;
            }
            float scale = .4f;
            if (imageNumber == 0)
            {
                if(IsSelected)
                {
                    if (animationPose)
                    {
                        spriteBatch.Draw(textureUH, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(textureSH, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    spriteBatch.Draw(textureUH, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
            else if (imageNumber == 1)
            {
                if(IsSelected)
                {
                    if (animationPose)
                    {
                        spriteBatch.Draw(textureUL, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(textureSL, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    spriteBatch.Draw(textureUL, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
            else if(imageNumber == 2)
            {
                if(IsSelected)
                {
                    if (animationPose)
                    {
                        spriteBatch.Draw(textureUS, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(textureSS, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    spriteBatch.Draw(textureUS, Position, null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
        }
    }
}
