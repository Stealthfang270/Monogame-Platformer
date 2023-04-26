using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platformer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class ActorObject : GameObject
    {
        //Animation
        CelAnimationPlayer CelPlayer;
        CelAnimationSet CelSet;

        //States
        ActorDirection Direction = ActorDirection.Right;
        internal ActorState State = ActorState.Idle;

        //Constants
        protected const int JumpForce = -400;
        protected const int MoveSpeed = 150;

        protected Vector2 _velocity;
        internal Vector2 Velocity => _velocity;

        Point spriteDimensions;

        public ActorObject(Game game, Transform transform, Rectangle rectangle, Texture2D texture, CelAnimationSet set) : base(game, transform, rectangle, texture)
        {
            CelPlayer = new CelAnimationPlayer();
            CelSet = set;
            CelPlayer.Play(CelSet.Idle);
        }

        //Code needs to be added to Game1.cs. Come back to this later.
        public override void Update(GameTime gameTime)
        {
            _rectangleBounds.Location = _transform.Position.ToPoint();
            CelPlayer.Update(gameTime);
            _velocity.Y += Game1.Gravity;
            _transform.MovePosition(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (Math.Abs(_velocity.Y) > Game1.Gravity)
            {
                State = ActorState.Jump;
                CelPlayer.Play(CelSet.Jump);
            }
            if (_transform.Position.Y > Game.Window.ClientBounds.Height - _rectangleBounds.Height)
            {
                _transform.SetPosition(_transform.Position.X, Game.Window.ClientBounds.Height - _rectangleBounds.Height);
                _velocity.Y = 0;
                State = ActorState.Walking;
            }
            Direction = Velocity.X >= 0 ? ActorDirection.Right : ActorDirection.Left;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            CelPlayer.Draw(spriteBatch, _transform.Position, (SpriteEffects)Direction);
            spriteBatch.End();
        }

        internal void HorizontalStop()
        {
            if (State == ActorState.Walking)
            {
                _velocity = Vector2.Zero;
                State = ActorState.Idle;
                CelPlayer.Play(CelSet.Idle);
            }
        }

        internal void MoveHorizontally(float direction)
        {
            float previousDirection = _velocity.X;
            _velocity.X = direction * MoveSpeed;
            if (State != ActorState.Jump)
            {
                CelPlayer.Play(CelSet.Run);
                State = ActorState.Walking;
            }
        }

        internal void MoveVertically(float direction)
        {
            _velocity.Y = direction * MoveSpeed;
        }

        internal void Land(Rectangle colliderRect)
        {
            if (_velocity.Y > 0)
            {
                _transform.SetPosition(_transform.Position.X, colliderRect.Top - _rectangleBounds.Height + 1);
                _velocity.Y = 0;
                State = ActorState.Walking;
            }
        }

        internal void StandOn(Rectangle colliderRect)
        {
            _velocity.Y -= Game1.Gravity;
        }

        internal void Jump()
        {
            if (State != ActorState.Jump)
            {
                _velocity.Y = JumpForce;
            }
        }
    }

    internal enum ActorDirection
    {
        Right,
        Left
    }

    internal enum ActorState
    {
        Idle,
        Walking,
        Jump
    }
}
