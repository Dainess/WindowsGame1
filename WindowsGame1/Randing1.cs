using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Randing1
    {
        Random randing = new Random();

        public int Random(int bottom, int top ) {
            int toRandom = randing.Next(bottom, top);
            return toRandom;
        }
    }
}
