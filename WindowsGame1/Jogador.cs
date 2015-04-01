using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Jogador : Character
    {

        public Jogador ()
        {
            hp = 100;
            atk = 10;
            def = 3;
            ki = 0;
            spd = 1;
            will = 2;
            winning = false;
            for (int i = 0; i < acaoTurno.Length; i++)
            {
                acaoTurno[i] = false;
            }
        }

        public void resetPlayer()
        {
            hp = 100;
            atk = 10;
            def = 3;
            ki = 0;
            spd = 1;
            will = 2;
        }

    }
}
