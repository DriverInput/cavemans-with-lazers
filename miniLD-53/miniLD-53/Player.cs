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
    class Player
    {
        public Texture2D rightWalk, leftWalk, currentAnimation;
        public Rectangle rectangle;
        public Vector2 velocity;
        public Vector2 position;
        public Rectangle sourceRectangle;
        public float elapsed;
        public float delay = 200f;
        public sbyte frames = 1;
        public int dir;

        public sbyte speed = 5;

        private bool hasJumped = false;
        public bool isMoving;

        public bool IsShooting;
        Texture2D bulletTexture;
        public Rectangle bulletRectangle;

        List<Bullets> bullets;

        int timeBetweenShots = 300;
        int shotTimer = 0;

        public Player(Vector2 newPos, Texture2D newBulletTexture, Rectangle newBulletRectangle)
        {
            position = newPos;
            bulletTexture = newBulletTexture;
            bulletRectangle = newBulletRectangle;

            bullets = new List<Bullets>();
        }

        public void Update(GameTime gameTime, sbyte ctr)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)32, 64);

            movement(gameTime, ctr);
            bulletRectangle.X = (int)position.X;
            bulletRectangle.Y = (int)position.Y;
            
            if (IsShooting)
            {
                shotTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shotTimer > timeBetweenShots)
                {
                    shotTimer = 0;

                    Bullets b = new Bullets(bulletTexture, bulletRectangle, dir * 10, position);

                    bullets.Add(b);
                }
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].update();
            }
        }


        public void Load(ContentManager Content, sbyte playerNumber)
        {
            leftWalk = Content.Load<Texture2D>("LeftTexture" + playerNumber);
            rightWalk = Content.Load<Texture2D>("RightTexture" + playerNumber);

            currentAnimation = rightWalk;
        }

        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (isMoving == true)
                {
                    frames++;
                    if (frames > 4)
                        frames = 1;
                }
                else { frames = 0; }
                elapsed = 0;
            }

            sourceRectangle = new Rectangle(25 * frames, 0, 25, 46);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentAnimation, rectangle, sourceRectangle, Color.White);
            foreach (Bullets b in bullets)
            {
                b.draw(spriteBatch);
            }
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X -= speed;
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
                velocity.Y = 1f;

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void movement(GameTime gameTime, sbyte ctr)
        {
            switch (ctr)
            {
                case 1:
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        velocity.X = -speed;
                        currentAnimation = leftWalk;
                        Animate(gameTime);
                        isMoving = true;
                        dir = -1;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        velocity.X = speed;
                        currentAnimation = rightWalk;
                        Animate(gameTime);
                        isMoving = true;
                        dir = 1;
                    }
                    else
                    {
                        velocity.X = 0f;
                        isMoving = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
                    {
                        position.Y -= 5f;
                        velocity.Y = -10f;
                        hasJumped = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
                    { IsShooting = true; }
                    else { IsShooting = false; }

                    if (velocity.Y < 10)
                        velocity.Y += 0.5f;
                    break;

                case 2:
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        velocity.X = -speed;
                        currentAnimation = leftWalk;
                        Animate(gameTime);
                        isMoving = true;
                        dir = -1;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        velocity.X = speed;
                        currentAnimation = rightWalk;
                        Animate(gameTime);
                        isMoving = true;
                        dir = 1;
                    }
                    else
                    {
                        velocity.X = 0f;
                        isMoving = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.W) && hasJumped == false)
                    {
                        position.Y -= 5f;
                        velocity.Y = -10f;
                        hasJumped = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    { IsShooting = true; }
                    else { IsShooting = false; }

                    if (velocity.Y < 10)
                        velocity.Y += 0.5f;
                    break;
            }
        }

    }
}
