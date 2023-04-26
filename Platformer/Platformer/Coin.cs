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
    internal class Coin : GameObject
    {
        CelAnimationPlayer CelPlayer;
        CelAnimationSequence Sequence;

        public int value;

        public Coin(int value, Game game, Transform transform, Rectangle rect, Texture2D texture, CelAnimationSequence sequence) : base(game, transform, rect, texture)
        {
            this.value = value;
            Sequence = sequence;
            CelPlayer = new CelAnimationPlayer();
            CelPlayer.Play(sequence);
        }

        public override void Update(GameTime gameTime)
        {
            _rectangleBounds.Location = _transform.Position.ToPoint();
            if (CelPlayer is not null)
            {
                CelPlayer.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (CelPlayer is not null)
            {
                CelPlayer.Draw(spriteBatch, _transform.Position, SpriteEffects.None);
            }
            spriteBatch.End();
        }

        public bool HandleCollisions(ActorObject player)
        {
            bool didCollide = false;
            if (_rectangleBounds.Intersects(player._rectangleBounds))
            {
                player.Coins++;
                CelPlayer = null;
                didCollide = true;
            }
            return didCollide;
        }
    }
}
