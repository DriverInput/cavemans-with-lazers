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

namespace miniLD_53
{
    class Bullets
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 origin;

        public bool destroy = false;

        public Bullets(Vector2 newPos) { position = newPos; }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Tile1");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, Color.White);
        }
    }
}
