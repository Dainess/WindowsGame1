using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Character
    {
        protected string name;
        protected int atk { get; set;}
        protected int def { get; set; }
        protected int hp { get; set; }
        public Texture2D face { get; set;}
        public Vector2 position { get; set; } 
        //Random random = new Random();
        protected bool winning, turn;
        public string isTurnActive;
        int[] stats = new int[3];

        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(face, position, Color.White);
        }

        public int[] getStats()
        {
            return stats;
        }

        public int getHp()
        {
            return hp;
        }

        public void setHp(int newHp)
        {
            hp = newHp;
        }

        public int getAtk()
        {
            return atk;
        }

        public void setAtk(int newAtk)
        {
            atk = newAtk;
        }


        public int getDef()
        {
            return def;
        }

        public void setDef(int newDef)
        {
            def = newDef;
        }

        public void resetPlayer(Randing1 randomize1)
        {
            hp = randomize1.Random(31, 40);
            atk = randomize1.Random(4, 6);
            def = randomize1.Random(1, 3);
        }

        public bool getWin()
        {
            return winning;
        }

        public void win (bool win)
        {
            if (win)
                winning = true;
            else
                winning = false;
        }

        public bool getTurn()
        {
            return turn;
        }

        public void setTurn(bool newState)
        {
            if (newState)
                isTurnActive = "true";
            else
                isTurnActive = "false";
            turn = newState;
        }

    }
}

//, def, spc, mgk, spd, hp;