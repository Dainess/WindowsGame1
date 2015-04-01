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
        Buttan butao, butao2, butao3, butao4, butaoStart, butaoAv, butaoKill, butaoKill2;
        MouseState currentMouseState, previousMouseState;
        //List<Vector2> Positions = new List<Vector2>();

        //variáveis dos personagens
        bool endGame;
        int count, countTexto, hp1, hp2, dano, dano2, finalHp1, finalHp2;
        Jogador player;
        Monstro monster;
        Randing1 randomize = new Randing1();
        List<bool> listTexto = new List<bool>();
        List<string> erroMsg = new List<string>();
       

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

            //inicia os botões gerais

            butaoKillPos = new Vector2(450, 180);
            butaoKill = new Buttan(butaoKillPos);
            butaoKill.butFace = Content.Load<Texture2D>("buttonGame");
            butaoKill.setActive(false);

            butaoKillPos = new Vector2(450, 220);
            butaoKill2 = new Buttan(butaoKillPos);
            butaoKill2.butFace = Content.Load<Texture2D>("buttonGame");
            butaoKill2.setActive(false);

            butaoAv = new Buttan(monster.position + new Vector2(-40, 430 - 90));
            butaoAv.butFace = Content.Load<Texture2D>("buttonGameBig");
            butaoAv.setActive(true);

            butaoPos3 = new Vector2(272, 300);
            butaoStart = new Buttan(butaoPos3);
            butaoStart.butFace = Content.Load<Texture2D>("buttonGame");
            butaoStart.setActive(false);

            //Inicia os botões de turno

            butao = new Buttan(new Vector2(62, 180));
            butao.butFace = Content.Load<Texture2D>("buttonGame");
            butao.setActive(false);

            butao2 = new Buttan(new Vector2(146, 180));
            butao2.butFace = Content.Load<Texture2D>("buttonGame");
            butao2.setActive(false);

            butao3 = new Buttan(new Vector2(62, 202));
            butao3.butFace = Content.Load<Texture2D>("buttonGame");
            butao3.setActive(false);

            butao4 = new Buttan(new Vector2(146, 202));
            butao4.butFace = Content.Load<Texture2D>("buttonGame");
            butao4.setActive(false);


            endGame = false;
            muda = Color.White;
            muda2 = Color.Black;
            count = 0;
            countTexto = 0;
            listTexto.Add(true);
            erroMsg.Add("");
            erroMsg.Add("Não há ki insuficiente");
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
                
                butaoStart.setActive(true);
                butaoAv.setActive(false);

            }

            if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (mousePos.X >= butaoAv.getPosition().X && mousePos.Y >= butaoAv.getPosition().Y &&
                    mousePos.X <= (butaoAv.getPosition().X + 64) && mousePos.Y <= (butaoAv.getPosition().Y + 20) && butaoAv.getActive())
                {
                    if (player.getTurn())
                    {
                        butao.setActive(true);
                        butao2.setActive(true);
                        butao3.setActive(true);
                        butao4.setActive(true);
                        butaoAv.setActive(false);
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
                            butaoAv.setActive(true);
                            monster.setTurn(true);
                            butaoKill.setActive(true);
                            butaoKill2.setActive(true);
                        }
                    }
                }

                else if (mousePos.X >= butao.getPosition().X && mousePos.Y >= butao.getPosition().Y &&
                    mousePos.X <= (butao.getPosition().X + 64) && mousePos.Y <= (butao.getPosition().Y + 20) && butao.getActive())
                {
                        count = count + 1;
                        dano2 = player.getAtk() + randomize.Random(1, 4) - monster.getDef();
                        hp2 = monster.getHp() - (dano2);
                        player.setKi(player.getKi() + 1);
                        if (player.getKi() >= 2)
                            player.setWarning(0);
                        monster.setHp(hp2);
                        monster.setTurn(true);
                        player.setTurn(false);
                        butao.setActive(false);
                        butao2.setActive(false);
                        butao3.setActive(false);
                        butao4.setActive(false);
                        butaoAv.setActive(true);
                }

                else if (mousePos.X >= butao2.getPosition().X && mousePos.Y >= butao2.getPosition().Y &&
                mousePos.X <= (butao2.getPosition().X + 64) && mousePos.Y <= (butao2.getPosition().Y + 20) && butao.getActive())
                {
                    if (player.getKi() < 2)
                    {
                        player.setWarning(1);
                    }
                    else
                    {
                        dano2 = player.getAtk() + randomize.Random(1, 4)*2 - monster.getDef();
                        hp2 = monster.getHp() - (dano2);
                        monster.setHp(hp2);
                        player.dropKi(2);
                        monster.setTurn(true);
                        player.setTurn(false);
                        butao.setActive(false);
                        butao2.setActive(false);
                        butao3.setActive(false);
                        butao4.setActive(false);
                        butaoAv.setActive(true);
                    }
                }


                else if (mousePos.X >= butaoStart.getPosition().X && mousePos.Y >= butaoStart.getPosition().Y &&
                    mousePos.X <= (butaoStart.getPosition().X + 64) && mousePos.Y <= (butaoStart.getPosition().Y + 20) && butaoStart.getActive())
                {
                    endGame = false;
                    butaoAv.setActive(true);
                    butaoStart.setActive(false);
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
                spriteBatch.DrawString(gameFont, "Você veio a este antigo templo em uma busca.", new Vector2(62, 300), Color.White, 0f, new Vector2(0,0), 0.7f, spriteEffect, 0.0f);
                spriteBatch.DrawString(gameFont, "Irás descobrir o que as portas de carvalho escondem... se puder", new Vector2(62, 320), Color.White, 0f, new Vector2(0, 0), 0.7f, spriteEffect, 0.0f);
            }
            else if (countTexto == 1)
            {
                spriteBatch.DrawString(gameFont, "As portas de carvalho são abertas com um clique da maçaneta.\nAlgo antigo se materializa, e avança sedento sobre você", new Vector2(62, 300), Color.White, 0f, new Vector2(0, 0), 0.7f, spriteEffect, 0.0f);
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
                butaoKill.Draw(spriteBatch, Color.Black);
                butaoKill2.Draw(spriteBatch, Color.Purple);

                if (player.getTurn() && !butaoAv.getActive())
                {
                    butao.Draw(spriteBatch, Color.Red);
                    butao2.Draw(spriteBatch, Color.White);
                    butao3.Draw(spriteBatch, Color.Green);
                    butao4.Draw(spriteBatch, Color.Yellow);
                }
                
                if (player.getWarning() == 1)
                    spriteBatch.DrawString(gameFont, erroMsg[player.getWarning()], new Vector2(62, 320), Color.White);

               
                
            }
            if (butaoAv.getActive())
            {
                butaoAv.Draw(spriteBatch, Color.White);
            }
            else if (butaoStart.getActive())
                butaoStart.Draw(spriteBatch, Color.White);

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