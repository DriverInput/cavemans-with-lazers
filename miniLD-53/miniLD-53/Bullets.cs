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
        public Vector2 position;
        public Rectangle rectangle;
        public Vector2 velocity;
        public Vector2 origin;

        public bool destroy = false;

        public Bullets(Texture2D newtexture, Rectangle newrectangle, int newvelocity, Vector2 newpos)
        {
            texture = newtexture;
            rectangle = newrectangle;
            velocity.X = newvelocity;
            position = newpos;
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Bullet");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 1f);
        }

        public void update()
        {
            position += velocity;
            rectangle.X = (int)position.X + 20;
            rectangle.Y = (int)position.Y + 30;
        }
    }
}
