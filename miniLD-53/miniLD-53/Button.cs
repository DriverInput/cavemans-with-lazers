using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace miniLD_53
{
    class Button
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        public Vector2 size;

        public Button(Texture2D newtexture, GraphicsDevice graphics)
        {
            texture = newtexture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 30);
        }

        public bool isClicked;

        public void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            rectangle = new Rectangle((int)position.X, (int)position.Y,
                (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else
            {
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, Color.White);
        }
    }
}
