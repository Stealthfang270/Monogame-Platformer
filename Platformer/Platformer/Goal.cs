using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class Goal : GameObject
    {
        public int Requirement = 2; //Number of coins needed

        public Goal(Game game, Transform transform, Rectangle rect, Texture2D texture) : base(game, transform, rect, texture){}

        public override void Update(GameTime gameTime)
        {
            _rectangleBounds.Location = _transform.Position.ToPoint();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(Texture, RectangleBounds, Color.White);
            spriteBatch.End();
        }

        public bool HandleCollisions(ActorObject player)
        {
            bool didCollide = false;
            if (_rectangleBounds.Intersects(player._rectangleBounds))
            {
                if(player.Coins >= Requirement)
                {
                    //End le game
                }
                didCollide = true;
            }
            return didCollide;
        }
    }
}
