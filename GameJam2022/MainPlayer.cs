using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameJam2022.Collisions;

namespace GameJam2022
{
    public class MainPlayer
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        /// <summary>
        /// The position of the player
        /// </summary>
        public Vector2 Position;

        public int Nitro { get; set; } = 0;

        public float Angle { get; set; } = 0;

        private BoundingCircle bounds;

        public bool Hit { get; set; } = false;

        private double hitTimer;

        public bool Endgame { get; set; } = false;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        public Vector2 HitDirection { get; set; } 

        /// <summary>
        /// Creates a new Main Player
        /// </summary>
        /// <param name="position">The position of the player in the game</param>
        public MainPlayer(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingCircle(position - new Vector2(16, 16), 16);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("MainCar");
            //this.bounds = new BoundingRectangle(position, texture.Width, texture.Height);
        }

        /// <summary>
        /// Updates the player's angle based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (!Endgame)
            {

                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    Nitro = 2;
                }
                else
                {
                    Nitro = 1;
                }
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    Angle -= .03f * Nitro;
                }

                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    Angle += .03f * Nitro;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {;
            if(!Endgame)
            {
                if (Hit)
                {
                    hitTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (hitTimer > 0.5)
                    {
                        Hit = false;
                        hitTimer -= 0.5;
                    }
                    Position += HitDirection;
                }
                else
                {
                    Vector2 velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
                    Position += velocity * 3 * Nitro; 
                }
            }
            bounds.Center = Position - new Vector2(16, 16);
            spriteBatch.Draw(texture, Position, null, Color.White, Angle, new Vector2(texture.Height / 2, texture.Width / 2), 1.0f, SpriteEffects.None, 0);
        }
    }
}
