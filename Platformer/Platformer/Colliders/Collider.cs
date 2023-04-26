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
    public class Collider
    {

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

        public Collider(Vector2 pos, Vector2 dimensions)
        {
            this.pos = pos;
            this.dimensions = dimensions;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }
    }
}
