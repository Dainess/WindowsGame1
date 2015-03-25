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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // variáveis do sistema
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //variáveis gráficas
        SpriteFont gameFont;
        Texture2D cursor;
        Color muda = new Color();
        Color muda2 = new Color();

        //variáveis de input
        Vector2 mousePos, butaoPos, butaoPos2, butaoPos3, butaoKillPos;
        Buttan butao, butao2, butao3, butao4, butao5, butaoKill, butaoKill2;
        MouseState currentMouseState, previousMouseState;
        //List<Vector2> Positions = new List<Vector2>();

        //variáveis dos personagens
        bool  endGame;
        int count, countTexto, hp1, hp2, dano, dano2, finalHp1, finalHp2;
        Jogador player;
        Monstro monster;
        Randing1 randomize = new Randing1();
        List<bool> listTexto = new List<bool>();
       

        //algo sobre o texto que tem que funcionar pra escalar
        SpriteEffects spriteEffect = new SpriteEffects();
       
        
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //gráficos básicos do jogo
            gameFont = Content.Load<SpriteFont>("GameFont");
            cursor = Content.Load<Texture2D>("cursorTestin");

            //inicia os personagens
            monster = new Monstro(randomize);
            player = new Jogador();

            //inicia seus valores
            monster.face = Content.Load<Texture2D>("spr_ball_blue");
            player.face = Content.Load<Texture2D>("spr_ball_green");

            player.position = new Vector2(62,45);
            monster.position = new Vector2(674, 45);

            player.setTurn(false);
            monster.setTurn(false);

            //inicia os botões
            butaoPos = new Vector2(62, 180);
            butao = new Buttan(butaoPos);
            butao.butFace = Content.Load<Texture2D>("buttonGame");
            butao.setActive(false);

            butaoKillPos = new Vector2(450, 180);
            butaoKill = new Buttan(butaoKillPos);
            butaoKill.butFace = Content.Load<Texture2D>("buttonGame");
            butaoKill.setActive(false);

            butao4 = new Buttan(new Vector2(360, 348));
            butao4.butFace = Content.Load<Texture2D>("buttonGameBig");
            butao4.setActive(false);

            butaoKillPos = new Vector2(450, 220);
            butaoKill2 = new Buttan(butaoKillPos);
            butaoKill2.butFace = Content.Load<Texture2D>("buttonGame");
            butaoKill2.setActive(false);

            butaoPos2 = monster.position + new Vector2(0, 180 - 45);
            butao2 = new Buttan(butaoPos2);
            butao2.butFace = Content.Load<Texture2D>("buttonGame");
            butao2.setActive(false);

            butao5 = new Buttan(butaoPos2 + new Vector2(-40, 250 - 45));
            butao5.butFace = Content.Load<Texture2D>("buttonGameBig");
            butao5.setActive(true);

            butaoPos3 = new Vector2(272, 300);
            butao3 = new Buttan(butaoPos3);
            butao3.butFace = Content.Load<Texture2D>("buttonGame");
            butao3.setActive(false);


            endGame = false;
            muda = Color.White;
            muda2 = Color.Black;
            count = 0;
            countTexto = 0;
            listTexto.Add(true);
            // TODO: use this.Content to load your game content here
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
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            mousePos.X = currentMouseState.X;
            mousePos.Y = currentMouseState.Y;

            if (player.getHp() <= 0 || monster.getHp() <= 0)
            {
                endGame = true;
                finalHp1 = player.getHp();
                finalHp2 = monster.getHp();
                if (player.getHp() <= 0)
                {
                    player.win(false);
                    monster.win(true);
                    player.resetPlayer();
                }
                else
                {
                    player.win(true);
                    monster.win(false);
                }
                
                monster.resetPlayer(randomize);
                
                butao3.setActive(true);
                butao5.setActive(false);

            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (mousePos.X >= butao5.getPosition().X && mousePos.Y >= butao5.getPosition().Y &&
                    mousePos.X <= (butao5.getPosition().X + 64) && mousePos.Y <= (butao5.getPosition().Y + 20) && butao5.getActive())
                {
                    if (player.getTurn())
                    {
                        count = count + 1;
                        dano2 = player.getAtk() + randomize.Random(1, 4) - monster.getDef();
                        hp2 = monster.getHp() - (dano2);
                        monster.setHp(hp2);
                        monster.setTurn(true);
                        player.setTurn(false);
                    }
                    else if (monster.getTurn())
                    {
                        count = count + 1;
                        dano = monster.getAtk() + randomize.Random(1, 4) - player.getDef();
                        hp1 = player.getHp() - (dano);
                        player.setHp(hp1);
                        player.setTurn(true);
                        monster.setTurn(false);
                    }
                    else if (listTexto[countTexto])
                    {
                        countTexto++;
                        if (countTexto < 2)
                        {
                            listTexto.Add(true);
                        }
                        else
                        {
                            butao5.setActive(true);
                            monster.setTurn(true);
                            butaoKill.setActive(true);
                            butaoKill2.setActive(true);
                        }
                    }
                }


                else if (mousePos.X >= butao3.getPosition().X && mousePos.Y >= butao3.getPosition().Y &&
                    mousePos.X <= (butao3.getPosition().X + 64) && mousePos.Y <= (butao3.getPosition().Y + 20) && butao3.getActive())
                {
                    endGame = false;
                    butao5.setActive(true);
                    butao3.setActive(false);
                }
                else if (mousePos.X >= butaoKill.getPosition().X && mousePos.Y >= butaoKill.getPosition().Y &&
                    mousePos.X <= (butaoKill.getPosition().X + 64) && mousePos.Y <= (butaoKill.getPosition().Y + 20) && butaoKill.getActive())
                {
                    player.setHp(0);
                }
                else if (mousePos.X >= butaoKill2.getPosition().X && mousePos.Y >= butaoKill2.getPosition().Y &&
                    mousePos.X <= (butaoKill2.getPosition().X + 64) && mousePos.Y <= (butaoKill2.getPosition().Y + 20) && butaoKill2.getActive())
                {
                    monster.setHp(0);
                }
                else if (mousePos.X >= butao4.getPosition().X && mousePos.Y >= butao4.getPosition().Y &&
                    mousePos.X <= (butao4.getPosition().X + 64) && mousePos.Y <= (butao4.getPosition().Y + 20) && butao4.getActive())
                {
                    butao5.setActive(true);
                    butao4.setActive(false);
                    butaoKill.setActive(true);
                    butaoKill2.setActive(true);
                }


            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Olive);
            spriteBatch.Begin();
            if (countTexto == 0)
            {
                spriteBatch.DrawString(gameFont, "Voce veio a este antigo templo em uma busca.", new Vector2(62, 300), Color.White, 0f, new Vector2(0,0), 0.7f, spriteEffect, 0.0f);
                spriteBatch.DrawString(gameFont, "Iras descobrir o que as portas de carvalho escondem... se puder", new Vector2(62, 320), Color.White, 0f, new Vector2(0, 0), 0.7f, spriteEffect, 0.0f);
                butao4.Draw(spriteBatch, Color.White);
            }
            else if (countTexto == 1)
            {
                spriteBatch.DrawString(gameFont, "As portas de carvalho sao abertas com um clique da macaneta.\nAlgo antigo se materializa, e avanca sedento sobre voc", new Vector2(62, 300), Color.White, 0f, new Vector2(0, 0), 0.7f, spriteEffect, 0.0f);
            }
            else if (endGame)
            {
                if (player.getWin())
                {
                    spriteBatch.DrawString(gameFont, "Player 1 venceu!", new Vector2(62, 352), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(gameFont, "Player 2 venceu!", new Vector2(62, 352), Color.White);
                }
                spriteBatch.DrawString(gameFont, "HP: " + Convert.ToString(finalHp1), player.position + new Vector2(0, 30), Color.White);
                spriteBatch.DrawString(gameFont, "HP: " + Convert.ToString(finalHp2), monster.position + new Vector2(0, 30), Color.White);
                spriteBatch.DrawString(gameFont, "Cliques: " + Convert.ToString(count), new Vector2(62, 300), Color.White);
            }
            
            else
            {
                spriteBatch.DrawString(gameFont, "HP: " + Convert.ToString(player.getHp()), player.position + new Vector2(0, 30), Color.White);
                spriteBatch.DrawString(gameFont, "ATK: " + Convert.ToString(player.getAtk()), player.position + new Vector2(0, 60), Color.White);
                spriteBatch.DrawString(gameFont, "DEF: " + Convert.ToString(player.getDef()), player.position + new Vector2(0, 90), Color.White);
                spriteBatch.DrawString(gameFont, "HP: " + Convert.ToString(monster.getHp()), monster.position + new Vector2(0, 30), Color.White);
                spriteBatch.DrawString(gameFont, "ATK: " + Convert.ToString(monster.getAtk()), monster.position + new Vector2(0, 60), Color.White);
                spriteBatch.DrawString(gameFont, "DEF: " + Convert.ToString(monster.getDef()), monster.position + new Vector2(0, 90), Color.White);
                if (player.getTurn())
                    spriteBatch.DrawString(gameFont, "Jogador 1 perdeu: " + Convert.ToString(dano) + " pontos de vida", new Vector2(62, 352), Color.White);
                else
                    spriteBatch.DrawString(gameFont, "Jogador 2 perdeu: " + Convert.ToString(dano2) + " pontos de vida", new Vector2(62, 352), Color.White);
                spriteBatch.DrawString(gameFont, "Jogador e o verde (jogador 1)", new Vector2(62, 372), Color.White);
                butaoKill.Draw(spriteBatch, Color.Green);
                butaoKill2.Draw(spriteBatch, Color.Red);
                
                
               
                
            }
            if (butao5.getActive())
            {
                butao5.Draw(spriteBatch, Color.White);
            }
            else if (butao3.getActive())
                butao3.Draw(spriteBatch, Color.White);

            spriteBatch.Draw(cursor, mousePos, Color.White);
            player.Draw(spriteBatch);
            monster.Draw(spriteBatch);
            spriteBatch.End();
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}

/* for (int i = 0; i < Positions.Count; i++)
              {
                  spriteBatch.Draw(Balloon, Positions[i], Color.White);
                  spriteBatch.DrawString(GameFont, "T", Positions[i], Color.White);
              } */