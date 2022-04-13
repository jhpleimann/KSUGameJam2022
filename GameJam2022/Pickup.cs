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
    public class Pickup
    {
        private Vector2 position;

        private BoundingCircle bounds;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// The vertices of the cube
        /// </summary>
        VertexBuffer vertices;

        /// <summary>
        /// The vertex indices of the cube
        /// </summary>
        IndexBuffer indices;

        /// <summary>
        /// The effect to use rendering the cube
        /// </summary>
        BasicEffect effect;

        /// <summary>
        /// The game this cube belongs to 
        /// </summary>
        Game game;

        float angle;

        /// <summary>
        /// Creates a new building
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public Pickup(Vector2 position, MainGame game)
        {
            this.position = position;
            //this.position.Y *= -1;
            this.game = game;
            this.bounds = new BoundingCircle(position, 32f);
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }

        /// <summary>
        /// Initialize the vertex buffer
        /// </summary>
        public void InitializeVertices()
        {
/*            var vertexData = new VertexPositionColor[] {
            new VertexPositionColor() { Position = new Vector3(-3 + position.X,  3 + position.Y, -3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3 + position.X,  3 + position.Y, -3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3(-3 + position.X, -3 + position.Y, -3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3( 3 + position.X, -3 + position.Y, -3), Color = Color.Cyan },
            new VertexPositionColor() { Position = new Vector3(-3 + position.X,  3 + position.Y,  3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3 + position.X,  3 + position.Y,  3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3(-3 + position.X, -3 + position.Y,  3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3( 3 + position.X, -3 + position.Y,  3), Color = Color.Cyan }
        };*/

            var vertexData = new VertexPositionColor[] {
            new VertexPositionColor() { Position = new Vector3(-3,  3, -3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3, -3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3(-3, -3, -3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3( 3, -3, -3), Color = Color.Cyan },
            new VertexPositionColor() { Position = new Vector3(-3,  3,  3), Color = Color.Blue },
            new VertexPositionColor() { Position = new Vector3( 3,  3,  3), Color = Color.Red },
            new VertexPositionColor() { Position = new Vector3(-3, -3,  3), Color = Color.Green },
            new VertexPositionColor() { Position = new Vector3( 3, -3,  3), Color = Color.Cyan }
        };
            vertices = new VertexBuffer(
                game.GraphicsDevice,            // The graphics device to load the buffer on 
                typeof(VertexPositionColor),    // The type of the vertex data 
                8,                              // The count of the vertices 
                BufferUsage.None                // How the buffer will be used
            );
            vertices.SetData<VertexPositionColor>(vertexData);
        }

        /// <summary>
        /// Initializes the index buffer
        /// </summary>
        public void InitializeIndices()
        {
            var indexData = new short[]
            {
            0, 1, 2, // Side 0
            2, 1, 3,
            4, 0, 6, // Side 1
            6, 0, 2,
            7, 5, 6, // Side 2
            6, 5, 4,
            3, 1, 7, // Side 3 
            7, 1, 5,
            4, 5, 0, // Side 4 
            0, 5, 1,
            3, 7, 2, // Side 5 
            2, 7, 6
            };
            indices = new IndexBuffer(
                game.GraphicsDevice,            // The graphics device to use
                IndexElementSize.SixteenBits,   // The size of the index 
                36,                             // The count of the indices
                BufferUsage.None                // How the buffer will be used
            );
            indices.SetData<short>(indexData);
        }

        /// <summary>
        /// Initializes the BasicEffect to render our cube
        /// </summary>
        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.World = Matrix.Identity;
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 20), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
   /*         effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );*/
            effect.Projection = Matrix.CreateOrthographic(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height, 0, 101);
            effect.VertexColorEnabled = true;
        }

        /// <summary>
        /// Updates the Cube
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            angle = (float)gameTime.TotalGameTime.TotalSeconds;
            // Look at the cube from farther away while spinning around it
          /*  effect.View = Matrix.CreateRotationY(angle) * Matrix.CreateLookAt(
                new Vector3(0, 5, -10),
                Vector3.Zero,
                Vector3.Up
            );*/
        }

        /*
        /// <summary>
        /// Updates the Cube
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Look at the cube from farther away
            effect.View = Matrix.CreateLookAt(
                new Vector3(0, 5, -10),
                Vector3.Zero,
                Vector3.Up
            );
        }
        */

        /// <summary>
        /// Draws the Cube
        /// </summary>
        public void Draw(Matrix matrix)
        {
            this.bounds.Center = position;

            effect.World = Matrix.CreateScale(5.5f) * Matrix.CreateRotationY(angle) * Matrix.CreateRotationX(15) * 
                Matrix.CreateTranslation(position.X - game.GraphicsDevice.Viewport.Width / 2, -position.Y + game.GraphicsDevice.Viewport.Height / 2, 0) * matrix;
            // apply the effect 
            effect.CurrentTechnique.Passes[0].Apply();
            // set the vertex buffer
            game.GraphicsDevice.SetVertexBuffer(vertices);
            // set the index buffer

            //game.GraphicsDevice.
            game.GraphicsDevice.Indices = indices;
            // Draw the triangles
            game.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList, // Tye type to draw
                0,                          // The first vertex to use
                0,                          // The first index to use
                12                          // the number of triangles to draw
            );
        }
    }
}
