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
    public class Platform
    {
        protected Texture2D texture;
        protected string textureName;
        protected Vector2 position;
        protected Vector2 dimensions;

        protected Collider colliderTop;
        protected Collider colliderBottom;
        protected Collider colliderRight;
        protected Collider colliderLeft;

        public Platform(Vector2 position, Vector2 dimensions, string texture)
        {
            this.textureName = texture;
            colliderTop = new Collider(
                new Vector2(position.X + 3, position.Y),
                new Vector2(dimensions.X - 6, 1),
                Collider.ColliderType.Top);

            colliderRight = new Collider(
               new Vector2(position.X + dimensions.X - 1, position.Y + 1),
               new Vector2(1, dimensions.Y - 2),
               Collider.ColliderType.Right);

            colliderBottom = new Collider(
                new Vector2(position.X + 3, position.Y + dimensions.Y),
                new Vector2(dimensions.X - 6, 1),
                Collider.ColliderType.Bottom);

            colliderLeft = new Collider(
                new Vector2(position.X, position.Y + 1),
                new Vector2(1, dimensions.Y - 2),
                Collider.ColliderType.Left);
        }

        internal void LoadContent(ContentManager content)
        {
            colliderTop.LoadContent(content);
            colliderBottom.LoadContent(content);
            colliderRight.LoadContent(content);
            colliderLeft.LoadContent(content);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            colliderTop.Draw(spriteBatch);
            colliderBottom.Draw(spriteBatch);
            colliderRight.Draw(spriteBatch);
            colliderLeft.Draw(spriteBatch);
        }

        internal void ProcessCollisions(ActorObject actor)
        {
            colliderTop.ProcessCollisions(actor);
            colliderBottom.ProcessCollisions(actor);
            colliderRight.ProcessCollisions(actor);
            colliderLeft.ProcessCollisions(actor);
        }
    }
}
