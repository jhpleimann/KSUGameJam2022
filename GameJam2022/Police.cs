using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameJam2022.Collisions;

namespace GameJam2022
{
    //Pass in the player position to this class
    public class Police
    {
        private KeyboardState keyboardState;

        private Texture2D texture1;

        private Texture2D texture2;

        private double animationTimer;

        private double stopTimer;

        private double speedTimer;

        private bool animationPose = false;

        private Vector2 direction;

        private BoundingCircle bounds;

        public bool Stop { get; set; } = false;

        public bool Endgame { get; set; } = false;

        public Vector2 Direction => direction;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;


        /// <summary>
        /// The position of the player
        /// </summary>
        public Vector2 Position;

        public float Angle { get; set; } = 0;

        /// <summary>
        /// Creates a new Main Player
        /// </summary>
        /// <param name="position">The position of the player in the game</param>
        public Police(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingCircle(position + new Vector2(16, 16), 16);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture1 = content.Load<Texture2D>("Police1");
            texture2 = content.Load<Texture2D>("Police2");
        }

        /// <summary>
        /// Updates the player's angle based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime, Vector2 target)
        {
            Vector2 heading = target - Position;

            float d = heading.Length();

            float distance = (float)Math.Sqrt(heading.X * heading.X + heading.Y * heading.Y);

            direction = heading / distance;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!Endgame)
            {
                if (!Stop)
                {
                    speedTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (speedTimer > 1.5 && speedTimer < 2.0)
                    {
                        Position += direction * 4;
                    }
                    else if (speedTimer >= 2.0)
                    {
                        Position += direction * 3;
                        speedTimer -= 2.0;
                    }
                    else
                    {
                        Position += direction * 2;
                    }
                    bounds.Center = Position;
                    if (direction.X < 0)
                    {
                        Angle = MathF.Atan(direction.Y / direction.X) - 60;
                    }
                    else
                    {
                        Angle = MathF.Atan(direction.Y / direction.X);
                    }
                }
                else
                {
                    stopTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (stopTimer > 1.0)
                    {
                        stopTimer -= 1.0;
                        Stop = false;
                    }
                }
            }
            
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3)
            {
                animationPose = !animationPose;
                animationTimer -= 0.3;
            }
            if (animationPose)
            {
                spriteBatch.Draw(texture1, Position, null, Color.White, Angle, new Vector2(texture1.Height / 2, texture1.Width / 2), 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(texture2, Position, null, Color.White, Angle, new Vector2(texture2.Height / 2, texture2.Width / 2), 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
