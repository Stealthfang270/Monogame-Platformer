using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class Collider
    {
        public enum ColliderType
        {
            Left,
            Right,
            Top,
            Bottom
        }

        protected ColliderType colliderType;

        protected Texture2D texture;
        protected Vector2 pos;
        protected Vector2 dimensions;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)pos.X, (int)pos.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        public Collider(Vector2 pos, Vector2 dimensions, ColliderType colliderType)
        {
            this.pos = pos;
            this.dimensions = dimensions;
            this.colliderType = colliderType;
        }

        internal void LoadContent(ContentManager content)
        {
            string textureString = "Collider" + colliderType.ToString();
            texture = content.Load<Texture2D>(textureString);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        internal bool ProcessCollisions(ActorObject actor)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(actor._rectangleBounds))
            {
                didCollide = true;
                switch (colliderType)
                {
                    case ColliderType.Left:
                        if (actor.Velocity.X > 0)
                        {
                            actor.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Right:
                        if (actor.Velocity.X < 0)
                        {
                            actor.MoveHorizontally(0);
                        }
                        break;
                    case ColliderType.Top:
                        actor.Land(BoundingBox);
                        actor.StandOn(BoundingBox);
                        break;
                    case ColliderType.Bottom:
                        if (actor.Velocity.Y < 0)
                        {
                            actor.MoveVertically(0);
                        }
                        break;
                }
            }

            return didCollide;
        }
    }
}
