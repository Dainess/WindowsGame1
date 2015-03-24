using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Buttan
    {
        Vector2 position = new Vector2();
        public Texture2D butFace { get; set; }
        bool active;
        public string isActive;

        public Buttan(Vector2 ButaoPos)
        {
            position = ButaoPos;
        }

        public void Draw(SpriteBatch spriteBatch, Color muda)
        {
            spriteBatch.Draw(butFace, position, muda);
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public bool getActive()
        {
            return active;
        }

        public void setActive(bool newState)
        {
            if (newState)
                isActive = "true";
            else
                isActive = "false";
            active = newState;
        }
    }
}
