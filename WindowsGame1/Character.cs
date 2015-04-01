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
        protected int warning;
        protected int atk { get; set;}
        protected int def { get; set; }
        protected int hp { get; set; }
        protected int ki { get; set; }
        protected int spd { get; set; }
        protected int will { get; set; }
        public Texture2D face { get; set;}
        public Vector2 position { get; set; } 
        //Random random = new Random();
        protected bool winning, turn;
        public string isTurnActive;
        int[] stats = new int[3];
        protected bool[] acaoTurno = new bool[4];

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(face, position, Color.White);
        }

        public void selAcao(int acao)
        {
            acaoTurno[acao] = true;
        }

        public int[] getStats()
        {
            return stats;
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

        public int getHp()
        {
            return hp;
        }

        public void setHp(int newHp)
        {
            hp = newHp;
        }
   
        public int getKi()
        {
            return ki;
        }

        public void setKi(int newKi)
        {
            ki = newKi;
        }

        public void dropKi(int lostKi)
        {
            ki = ki - lostKi;
        }

        public int getSpd()
        {
            return spd;
        }

        public void setSpd(int newSpd)
        {
            spd = newSpd;
        }

        public int getWill()
        {
            return will;
        }

        public void setWill(int newWill)
        {
            will = newWill;
        }

        public void resetPlayer(Randing1 randomize1)
        {
            hp = randomize1.Random(31, 40);
            atk = randomize1.Random(4, 6);
            def = randomize1.Random(1, 3);
            ki = randomize1.Random(0, 2);
            spd = 1; 
            will = randomize1.Random(0, 2);
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

        public int getWarning()
        {
            return warning;
        }

        public void setWarning(int war)
        {
            warning = war;

        }
    }
}

//, def, spc, mgk, spd, hp;