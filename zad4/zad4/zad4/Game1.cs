using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace zad4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle paletka1rect, paletka2rect, pilkarect;
        int predkosc = 3, ruchx = 1, ruchy = 1;
        Texture2D tlo, paletka1, paletka2, pilka;
        int ruchgraczy = 7, wynik1 = 0, wynik2 = 0;
        bool ruchdol = true, ruchprawo = true, start = false;
        SpriteFont font;
        Random r = new Random();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paletka1rect = new Rectangle(10, 250, 10, 100);
            paletka2rect = new Rectangle(980, 250, 10, 100);
            pilkarect = new Rectangle(485, 285, 30, 30);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            tlo = Content.Load<Texture2D>("pongBackground");
            paletka1 = Content.Load<Texture2D>("paddle1");
            paletka2 = Content.Load<Texture2D>("paddle2");
            pilka = Content.Load<Texture2D>("pongBall");
            font = Content.Load<SpriteFont>("SpriteFont1");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState klawiatura = Keyboard.GetState();
            if (klawiatura.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            //start pilki
            if (start == false && klawiatura.IsKeyDown(Keys.Space))
            {
                start = true;
                predkosc = 3;
                
                int i = r.Next(0,2);
                if (i == 1)
                    ruchprawo = true;
                else
                    ruchprawo = false;
                i = r.Next(0, 2);
                if (i == 1)
                    ruchdol = true;
                else
                    ruchdol = false;
                
                
            }


            //ruch graczy
            //gracz1
            if (klawiatura.IsKeyDown(Keys.Q))
            {
                if(paletka1rect.Y > 0)
                    paletka1rect.Y -= ruchgraczy;
                
                    
            }
            else if (klawiatura.IsKeyDown(Keys.A))
            {
                if (paletka1rect.Y < 500)
                    paletka1rect.Y += ruchgraczy;
            }
            //gracz2
            if (klawiatura.IsKeyDown(Keys.P))
            {
                if (paletka2rect.Y > 0)
                    paletka2rect.Y -= ruchgraczy;


            }
            else if (klawiatura.IsKeyDown(Keys.L))
            {
                if (paletka2rect.Y < 500)
                    paletka2rect.Y += ruchgraczy;
            }
            //ruch pilki
            if (start)
            {
                if (ruchdol)
                {
                    pilkarect.Y += ruchy + predkosc;
                    if (pilkarect.Y > 570)
                    {
                        ruchdol = false;
                    }
                }
                else
                {
                    pilkarect.Y -= ruchy + predkosc;
                    if (pilkarect.Y < 1)
                    {
                        ruchdol = true;
                    }
                }
                if (ruchprawo)
                {
                    pilkarect.X += ruchx + predkosc;

                    if (pilkarect.X > 1000)
                    {
                        wynik1++;
                        start = false;
                        pilkarect = new Rectangle(485, 285, 30, 30);
                    }
                }
                else 
                {
                    pilkarect.X -= ruchx + predkosc;

                    if (pilkarect.X < -30)
                    {
                        wynik2++;
                        start = false;
                        pilkarect = new Rectangle(485, 285, 30, 30);
                    }
                }
            }
            //kolizje pilki z paletkami bez podzialu na sektory
            if (paletka2rect.Intersects(pilkarect))
            {
                ruchprawo = false;
                predkosc++;
            }
            else if (paletka1rect.Intersects(pilkarect))
            {
                ruchprawo = true;
                predkosc++;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //rusowanie tla paletek pilki i wyniku
            spriteBatch.Draw(tlo, GraphicsDevice.Viewport.TitleSafeArea, Color.White);
            spriteBatch.Draw(paletka1, paletka1rect, Color.White);
            spriteBatch.Draw(paletka2, paletka2rect, Color.White);
            spriteBatch.Draw(pilka, pilkarect, Color.White);
            spriteBatch.DrawString(font, wynik1.ToString(), new Vector2(430,0), Color.White);
            spriteBatch.DrawString(font, wynik2.ToString(), new Vector2(520, 0), Color.White);
            
            
            //wyswietla podpowiedz o sterowaniu
            if (!start)
            {
                spriteBatch.DrawString(font, "Ruch \n\"Q\" \"A\"", new Vector2(100, 150), Color.White);
                spriteBatch.DrawString(font, "Ruch \n\"P\" \"L\"", new Vector2(600, 150), Color.White);
                spriteBatch.DrawString(font, "Start: SPACE", new Vector2(250, 500), Color.White);
            }
            
            
            spriteBatch.End();
             




            base.Draw(gameTime);
        }
    }
}
