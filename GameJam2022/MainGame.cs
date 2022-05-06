using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameJam2022
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TileMapBackground[,] tiles;
        private TileMapBackground[,] backgroundTiles;
        private Building[] buildings;
        private MenuSelector[] selectors;
        private MainPlayer player;
        private MiniMap map;
        private int worldHeight;
        private int worldWidth;
        private List<Police> policeList = new List<Police>();
        private List<SpecialPolice> specialPoliceList = new List<SpecialPolice>();
        private List<SWAT> swatList = new List<SWAT>();
        private List<Spike> spikeList = new List<Spike>();
        private List<Pickup> pickupList = new List<Pickup>();
        private int gameLevel = 0;
        private bool keyDownPressed = false;
        private bool keyUpPressed = false;
        private SpriteFont spriteFont;
        private Texture2D healthTexture;
        private int score = 0;
        private float health = 200;
        private float healthLoss = .1f;
        private Rectangle healthRectangle;
        private Song backgroundMusic;
        private Song gameMusic;
        private double grassTimer;
        private double stationTimer;
        private double specialTimer;
        private double pickUpTimer;
        private double swatTimer;
        private double spikeTimer;
        private Random r = new Random();


        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            worldHeight = 32;
            worldWidth = 32;
            tiles = new TileMapBackground[worldWidth,worldHeight];
            backgroundTiles = new TileMapBackground[worldWidth, worldHeight];
            map = new MiniMap(new Vector2(0,0), worldWidth, worldHeight);
            buildings = new Building[4];
            selectors = new MenuSelector[2];
            policeList.Add(new Police(new Vector2(4 * 64, 7 * 64)));
            policeList.Add(new Police(new Vector2(7 * 64, 7 * 64)));
            player = new MainPlayer(new Vector2(7 * 64, 4 * 64));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteFont = Content.Load<SpriteFont>("Arial");
            //Generate Player
            player.LoadContent(Content);
            healthTexture = Content.Load<Texture2D>("Health");
            healthRectangle = new Rectangle(0, 0, (int)health, healthTexture.Height);
            backgroundMusic = Content.Load<Song>("GameJam2022Song");
            gameMusic = Content.Load<Song>("GameJamBSong");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = .5f;
            MediaPlayer.Play(backgroundMusic);

            //Generate the background
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    backgroundTiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    backgroundTiles[x, y].LoadContent(Content);
                }
            }
            map.LoadContent(Content);

            //Generate the menuselectors
            selectors[0] = new MenuSelector(new Vector2(3 * 64, 2 * 64), 0);
            selectors[0].LoadContent(Content);
            selectors[0].IsSelected = true;

            selectors[1] = new MenuSelector(new Vector2(3 * 64, 4 * 64), 1);
            selectors[1].LoadContent(Content);
            selectors[1].IsSelected = false;

            //Generate the buildings
            buildings[0] = new Building(new Vector2(64 * 16, 64 * 16),0);
            buildings[1] = new Building(new Vector2(64 * 8, 64 * 8), 1);
            buildings[2] = new Building(new Vector2(64 * 8, 64 * 25), 2);
            buildings[3] = new Building(new Vector2(64 * 25, 64 * 8), 3);
            //buildings[4] = new Building(new Vector2(64 * 24, 64 *), 4);

            foreach(Building build in buildings)
            {
                build.LoadContent(Content);
            }

            //Generate the water
            for (int y = 0; y < worldHeight; y++)
            {
                int x = 0;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                tiles[x, y].LoadContent(Content);

                x = 31;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                tiles[x, y].LoadContent(Content);
            }
            for (int x = 0; x < worldWidth; x++)
            {
                int y = 0;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                tiles[x, y].LoadContent(Content);

                y = 31;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                tiles[x, y].LoadContent(Content);
            }

            //Generate the sand
            for (int y = 1; y < worldHeight - 1; y++)
            {
                int x = 1;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 1);
                tiles[x, y].LoadContent(Content);

                x = 30;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 1);
                tiles[x, y].LoadContent(Content);
            }
            for (int x = 1; x < worldWidth - 1; x++)
            {
                int y = 1;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 1);
                tiles[x, y].LoadContent(Content);

                y = 30;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 1);
                tiles[x, y].LoadContent(Content);
            }

            //Generate the Grass
            for (int x = 2; x < worldWidth - 2; x++)
            {
                for (int y = 2; y < worldHeight - 2; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 2);
                    tiles[x, y].LoadContent(Content);
                }
            }

            //Generate ponds
            for(int y = 8; y < 12; y++)
            {
                int xx = 13;
                tiles[xx, y] = new TileMapBackground(new Vector2(xx * 64, y * 64), 0);
                tiles[xx, y].LoadContent(Content);
            }

            for(int x = 22; x < 24; x++)
            {
                for (int y = 8; y < 12; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    tiles[x, y].LoadContent(Content);
                }
            }

            for (int x = 18; x < 24; x++)
            {
                for (int y = 14; y < 18; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    tiles[x, y].LoadContent(Content);
                }
            }

            for (int x = 2; x < 6; x++)
            {
                int y = 13;
                tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                tiles[x, y].LoadContent(Content);
            }

            for (int x = 18; x < 24; x++)
            {
                for (int y = 14; y < 18; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    tiles[x, y].LoadContent(Content);
                }
            }

            for (int x = 8; x < 12; x++)
            {
                for (int y = 22; y < 24; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    tiles[x, y].LoadContent(Content);
                }
            }

            for (int x = 19; x < 21; x++)
            {
                for (int y = 25; y < 28; y++)
                {
                    tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 0);
                    tiles[x, y].LoadContent(Content);
                }
            }

            //Generate the Road
            for (int x = 2; x < worldWidth - 2; x++)
            {
                for (int y = 2; y < worldHeight - 2; y++)
                {
                    if(x % 6 == 0 || x % 7 == 0)
                    {
                        tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 3);
                        tiles[x, y].LoadContent(Content);
                    }
                    if (y % 6 == 0 || y % 7 == 0)
                    {
                        tiles[x, y] = new TileMapBackground(new Vector2(x * 64, y * 64), 3);
                        tiles[x, y].LoadContent(Content);
                    }
                }
            }

            //Generate the initial Police
            foreach (Police popo in policeList)
            {
                popo.LoadContent(Content);
            }
    }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (gameLevel == 0)
            {
                if (keyboardState.IsKeyDown(Keys.Up) && keyUpPressed == false)
                {
                    keyUpPressed = true;
                    for (int i = 0; i < 2; i++)
                    {
                        if (selectors[i].IsSelected)
                        {
                            selectors[i].IsSelected = false;
                            if (i <= 0)
                            {
                                selectors[1].IsSelected = true;
                            }
                            else
                            {
                                selectors[i - 1].IsSelected = true;
                            }
                            i = 2;
                        }
                    }
                }
                else if (keyboardState.IsKeyUp(Keys.Up))
                {
                    keyUpPressed = false;
                }

                if (keyboardState.IsKeyDown(Keys.Down) && keyDownPressed == false)
                {
                    keyDownPressed = true;
                    for (int i = 0; i < 2; i++)
                    {
                        if (selectors[i].IsSelected)
                        {
                            selectors[i].IsSelected = false;
                            if (i >= 1)
                            {
                                selectors[0].IsSelected = true;
                            }
                            else
                            {
                                selectors[i + 1].IsSelected = true;
                            }
                            i = 2;
                        }
                    }
                }
                else if (keyboardState.IsKeyUp(Keys.Down))
                {
                    keyDownPressed = false;
                }

                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (selectors[i].IsSelected)
                        {
                            switch (i)
                            {
                                case 0:
                                    gameLevel = 2;
                                    break;
                                case 1:
                                    gameLevel = 1;
                                    MediaPlayer.Stop();
                                    MediaPlayer.Play(gameMusic);
                                    stationTimer = 0;
                                    specialTimer = 0;
                                    pickUpTimer = 0;
                                    grassTimer = 0;
                                    break;
                            }
                            i = 3;
                        }
                    }
                }
            }
            else if (gameLevel == 1)
            {
                player.Update(gameTime);
                stationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (stationTimer > 30.0)
                {
                    stationTimer -= 30.0;
                    policeList.Add(new Police(new Vector2(64 * 16, 64 * 16)));
                    policeList[policeList.Count - 1].LoadContent(Content);
                }
                specialTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (specialTimer > 60.0)
                {
                    specialTimer -= 60.0;
                    specialPoliceList.Add(new SpecialPolice(new Vector2(64 * 16, 64 * 16)));
                    specialPoliceList[specialPoliceList.Count - 1].LoadContent(Content);
                }
                swatTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (swatTimer > 45.0)
                {
                    swatTimer -= 45.0;
                    swatList.Add(new SWAT(new Vector2(64 * 16, 64 * 16)));
                    swatList[swatList.Count - 1].LoadContent(Content);
                }
                spikeTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (spikeTimer > 20.0)
                {
                    spikeTimer -= 20.0;
                    foreach(SWAT swat in swatList)
                    {
                        spikeList.Add(new Spike(new Vector2(swat.Position.X, swat.Position.Y)));
                        spikeList[spikeList.Count - 1].LoadContent(Content);
                    }
                }
                spikeTimer += gameTime.ElapsedGameTime.TotalSeconds;
                pickUpTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (pickUpTimer > 20.0)
                {
                    pickUpTimer -= 20.0;
                    pickupList.Add(new Pickup(new Vector2(r.Next(0, worldWidth * 64), r.Next(0, worldHeight * 64)), this));
                }

                foreach (TileMapBackground tile in tiles)
                {
                    if (tile.imageNumber == 0)
                    {
                        for (int i = 0; i < pickupList.Count; i++)
                        {
                            if (pickupList[i].Bounds.CollidesWith(tile.Bounds))
                            {
                                pickupList.RemoveAt(i);
                                i--;
                                pickUpTimer = 21.0;
                            }
                        }
                    }
                    if (tile.imageNumber == 0 && tile.Bounds.CollidesWith(player.Bounds))
                    {
                        //Add Game Over part here
                        player.Endgame = true;
                        foreach (Police popo in policeList)
                        {
                            popo.Endgame = true;
                        }
                        foreach (SpecialPolice popo in specialPoliceList)
                        {
                            popo.Endgame = true;
                        }
                        foreach (SWAT popo in swatList)
                        {
                            popo.Endgame = true;
                        }
                    }
                    else if (tile.imageNumber == 1 && tile.Bounds.CollidesWith(player.Bounds))
                    {
                        //Collided with Ground
                    }
                    else if (tile.imageNumber == 2 && tile.Bounds.CollidesWith(player.Bounds))
                    {
                        grassTimer += gameTime.ElapsedGameTime.TotalSeconds;
                        if (grassTimer > 4.0)
                        {
                            grassTimer -= 4.0;
                            policeList.Add(new Police(new Vector2(64 * 16, 64 * 16)));
                            policeList[policeList.Count - 1].LoadContent(Content);
                        }
                    }
                    else if (tile.imageNumber == 3 && tile.Bounds.CollidesWith(player.Bounds))
                    {
                        //Collided with street
                    }
                }
                foreach(Building build in buildings)
                {
                    if(build.imageNumber == 1 && build.Bounds.CollidesWith(player.Bounds) && !build.Closed)
                    {
                        health += 150;
                        healthLoss = healthLoss * 1.1f;
                        build.Closed = true;
                    }
                    else if (build.imageNumber == 2 && build.Bounds.CollidesWith(player.Bounds) && !build.Closed)
                    {
                        health += 25;
                        healthLoss = healthLoss * .8f;
                        build.Closed = true;
                    }
                    else if (build.imageNumber == 3 && build.Bounds.CollidesWith(player.Bounds) && !build.Closed)
                    {
                        health += 50;
                        build.Closed = true;
                    }
                }
                foreach(SpecialPolice special in specialPoliceList)
                {
                    if (special.Bounds.CollidesWith(player.Bounds) && special.Stop == false)
                    {
                        player.HitDirection = special.Direction * 3;
                        player.Hit = true;
                        special.Stop = true;
                    }
                }
                foreach (SWAT swat in swatList)
                {
                    if (swat.Bounds.CollidesWith(player.Bounds) && swat.Stop == false)
                    {
                        player.HitDirection = swat.Direction * 1.3f;
                        player.Hit = true;
                        swat.Stop = true;
                    }
                }
                for (int i = 0; i < pickupList.Count; i++)
                {
                    if (pickupList[i].Bounds.CollidesWith(player.Bounds))
                    {
                        health += 50;
                        pickupList.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < policeList.Count; i++)
                {
                    if (policeList[i].Bounds.CollidesWith(player.Bounds) && policeList[i].Stop == false)
                    {
                        player.HitDirection = policeList[i].Direction * 3;
                        player.Hit = true;
                        policeList[i].Stop = true;
                    }
                    for (int j = 1; j < policeList.Count; j++)
                    {
                        if (policeList[i].Bounds.CollidesWith(policeList[j].Bounds) && i != j && policeList[j].Stop == false)
                        {
                            policeList[i].Stop = true;
                        }
                    }
                }
                for(int i = 0; i < spikeList.Count; i++)
                {
                    if(spikeList[i].Bounds.CollidesWith(player.Bounds))
                    {
                        player.Hit = true;
                        spikeList.RemoveAt(i);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.Y))
                {
                    gameLevel = 0;
                    score = 0;
                    health = 200;
                    policeList = new List<Police>();
                    specialPoliceList = new List<SpecialPolice>();
                    policeList.Add(new Police(new Vector2(4 * 64, 7 * 64)));
                    policeList.Add(new Police(new Vector2(7 * 64, 7 * 64)));
                    player = new MainPlayer(new Vector2(7 * 64, 4 * 64));
                    foreach (Police popo in policeList)
                    {
                        popo.LoadContent(Content);
                    }
                    foreach(Building b in buildings)
                    {
                        b.Closed = false;
                    }
                    player.LoadContent(Content);
                }
                foreach (Police popo in policeList)
                {
                    popo.Update(gameTime, player.Position);
                }
                foreach (SpecialPolice popo in specialPoliceList)
                {
                    popo.Update(gameTime, player.Position);
                }
                foreach (SWAT popo in swatList)
                {
                    popo.Update(gameTime, player.Position);
                }
                foreach (Pickup pick in pickupList)
                {
                    pick.Update(gameTime);
                }
                if (health <= 0)
                {
                    player.Endgame = true;
                    foreach (Police popo in policeList)
                    {
                        popo.Endgame = true;
                    }
                    foreach (SpecialPolice popo in specialPoliceList)
                    {
                        popo.Endgame = true;
                    }
                    foreach (SWAT popo in swatList)
                    {
                        popo.Endgame = true;
                    }
                }
            }
            else if (gameLevel == 2)
            {
                if (keyboardState.IsKeyDown(Keys.Back))
                {
                    gameLevel = 0;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(gameLevel == 0)
            {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.CornflowerBlue);
                foreach(TileMapBackground t in backgroundTiles)
                {
                    t.Draw(gameTime, _spriteBatch);
                }
                foreach(MenuSelector s in selectors)
                {
                    s.Draw(gameTime, _spriteBatch);
                }
                _spriteBatch.End();
            }
            if(gameLevel == 1)
            {
                float playerX = MathHelper.Clamp(player.Position.X, 300, worldWidth * 64 - 500);
                float offsetX = 300 - playerX;

                float playerY = MathHelper.Clamp(player.Position.Y, 300, worldHeight * 64 - 180);
                float offsetY = 300 - playerY;

                Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0);
                Matrix transform3D = Matrix.CreateTranslation(offsetX, -offsetY, 0);
                _spriteBatch.Begin(transformMatrix: transform);
                GraphicsDevice.Clear(Color.CornflowerBlue);
                for (int x = 0; x < worldWidth; x++)
                {
                    for (int y = 0; y < worldHeight; y++)
                    {
                        tiles[x, y].Draw(gameTime, _spriteBatch);
                        map.Map[x, y] = tiles[x, y].imageNumber;
                    }
                }
                foreach (Spike spike in spikeList)
                {
                    spike.Draw(gameTime, _spriteBatch);
                    int x = (int)spike.Position.X / 64;
                    int y = (int)spike.Position.Y / 64;
                    map.Map[x, y] = 6;
                }
                foreach (Police popo in policeList)
                {
                    popo.Draw(gameTime, _spriteBatch);
                    int x = (int)popo.Position.X / 64;
                    int y = (int)popo.Position.Y / 64;
                    map.Map[x, y] = 6;
                }
                foreach (SWAT popo in swatList)
                {
                    popo.Draw(gameTime, _spriteBatch);
                    int x = (int)popo.Position.X / 64;
                    int y = (int)popo.Position.Y / 64;
                    map.Map[x, y] = 6;
                }
                foreach (SpecialPolice popo in specialPoliceList)
                {
                    popo.Draw(gameTime, _spriteBatch);
                    int x = (int)popo.Position.X / 64;
                    int y = (int)popo.Position.Y / 64;
                    map.Map[x, y] = 6;
                }
                if (player.Endgame == false)
                {
                    score++;
                    if(player.Nitro != 1)
                    {
                        health -= (healthLoss * 2);
                    }
                    else
                    {
                        health -= healthLoss;
                    }
                    
                }
                healthRectangle = new Rectangle(0, 0, (int)health, healthTexture.Height);

                player.Draw(gameTime, _spriteBatch);
                int pX = (int)player.Position.X / 64;
                int pY = (int)player.Position.Y / 64;
                map.Map[pX, pY] = 5;
                foreach (Building build in buildings)
                {
                    build.Draw(gameTime, _spriteBatch);
                    int x = (int)build.Position.X / 64;
                    int y = (int)build.Position.Y / 64;
                    map.Map[x, y] = 4;
                }
                _spriteBatch.DrawString(spriteFont, score + "", new Vector2(playerX - 64 * 4 - 32, playerY - 64 * 4 - 32), Color.Gold);
                _spriteBatch.Draw(healthTexture, new Vector2(playerX + 64 * 7 - health + 48, playerY - 64 * 4 - 40), healthRectangle, Color.White);
                if (player.Endgame)
                {
                    _spriteBatch.DrawString(spriteFont, "Game Over. Final Score: " + score, new Vector2(playerX - 64 * 2, playerY - 64 - 32), Color.Red);
                    _spriteBatch.DrawString(spriteFont, "Press Y to restart", new Vector2(playerX - 64, playerY - 32), Color.Red);
                }
                foreach (Pickup pick in pickupList)
                {
                    pick.Draw(transform3D);
                    int x = (int)pick.Position.X / 64;
                    int y = (int)pick.Position.Y / 64;
                    map.Map[x, y] = 7;
                }
                map.Position = new Vector2(playerX + 64 * 5 + 48, playerY + 64 * 1 - 16);
                map.Draw(_spriteBatch);
                _spriteBatch.End();
                foreach (Pickup pick in pickupList)
                {
                    pick.Draw(transform3D);
                }
            }
            else if(gameLevel == 2)
            {
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.CornflowerBlue);
                foreach (TileMapBackground t in backgroundTiles)
                {
                    t.Draw(gameTime, _spriteBatch);
                }
                _spriteBatch.DrawString(spriteFont, "Basics", new Vector2(5 * 64, 2), Color.Black);
                _spriteBatch.DrawString(spriteFont, "The goal of the game is to stay alive", new Vector2(32, 64 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "To stay alive, you must get food from restraunts", new Vector2(32, 64 * 2 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "Stay on the road or extra enemies will spawn", new Vector2(32, 64 * 3 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "Press W or Up to use nitro. Beware this costs health", new Vector2(32, 64 * 4 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "Your health is on the bottom right of the screen", new Vector2(32, 64 * 5 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "Use either the a and d or left and right keys to move", new Vector2(32, 64 * 6 - 16), Color.Black);
                _spriteBatch.DrawString(spriteFont, "Press the Backspace key to leave this page", new Vector2(32, 64 * 7 - 16), Color.Black);

                _spriteBatch.End();
            }





            base.Draw(gameTime);
        }
    }
}
