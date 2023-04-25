using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public Platform()
        {

        }
    }
}
