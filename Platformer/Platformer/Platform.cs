using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platformer.Colliders;

namespace Platformer
{
    public class Platform
    {
        protected Texture2D texture;
        protected string textureName;
        protected Vector2 position;
        protected Vector2 dimensions;

        protected TopCollider colliderTop;
        protected RightCollider colliderRight;
        protected BottomCollider colliderBottom;
        protected LeftCollider colliderLeft;

        public Platform(Vector2 position, Vector2 dimensions, string texture)
        {
            this.textureName = texture;
            colliderTop = new TopCollider(
                new Vector2(position.X + 3, position.Y),
                new Vector2(dimensions.X - 6, 1));

            colliderRight = new RightCollider(
               new Vector2(position.X + dimensions.X - 1, position.Y + 1),
               new Vector2(1, dimensions.Y - 2));

            colliderBottom = new BottomCollider(
                new Vector2(position.X + 3, position.Y + dimensions.Y),
                new Vector2(dimensions.X - 6, 1));

            colliderLeft = new LeftCollider(
                new Vector2(position.X, position.Y + 1),
                new Vector2(1, dimensions.Y - 2));
        }

        internal void LoadContent(ContentManager content)
        {
            colliderTop.LoadContent(content);
            colliderRight.LoadContent(content);
            colliderBottom.LoadContent(content);
            colliderLeft.LoadContent(content);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            colliderTop.Draw(spriteBatch);
            colliderRight.Draw(spriteBatch);
            colliderBottom.Draw(spriteBatch);
            colliderLeft.Draw(spriteBatch);
        }

        internal void ProcessCollisions(ActorObject actor)
        {
            colliderTop.ProcessCollisions(actor);
            colliderRight.ProcessCollisions(actor);
            colliderBottom.ProcessCollisions(actor);
            colliderLeft.ProcessCollisions(actor);
        }
    }
}
