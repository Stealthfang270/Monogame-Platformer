using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Colliders
{
    public class BottomCollider : Collider
    {
        public BottomCollider(Vector2 pos, Vector2 dimensions) : base(pos, dimensions)
        {
            this.pos = pos;
            this.dimensions = dimensions;
        }

        internal void LoadContent(ContentManager content)
        {
            string textureString = "ColliderBottom";
            texture = content.Load<Texture2D>(textureString);
        }

        internal bool ProcessCollisions(ActorObject actor)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(actor._rectangleBounds))
            {
                didCollide = true;
                if (actor.Velocity.Y < 0)
                {
                    actor.MoveVertically(0);
                }
            }

            return didCollide;
        }
    }
}
