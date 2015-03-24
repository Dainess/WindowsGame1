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
            winning = false;
        }

        public void resetPlayer()
        {
            hp = 100;
            atk = 10;
            def = 3;
        }

    }
}
