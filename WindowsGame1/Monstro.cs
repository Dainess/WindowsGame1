using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Monstro : Character
    {
        public Monstro (Randing1 randomize1)
        {
            hp = randomize1.Random(31, 40);
            atk = randomize1.Random(4, 6);
            def = randomize1.Random(1, 3);
            winning = false;
            for (int i = 0; i < acaoTurno.Length; i++)
            {
                acaoTurno[i] = false;
            }
        }

    }
}
