using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class CelAnimationSet
    {
        readonly CelAnimationSequence _idle;
        readonly CelAnimationSequence _run;
        readonly CelAnimationSequence _jump;
        public CelAnimationSequence Idle => _idle;
        public CelAnimationSequence Run => _run;
        public CelAnimationSequence Jump => _jump;

        public CelAnimationSet(Texture2D idle, Texture2D run, Texture2D jump)
        {
            _idle = new CelAnimationSequence(idle, 48, 0.2f);
            _run = new CelAnimationSequence(run, 48, 0.125f);
            _jump = new CelAnimationSequence(jump, 48, 0.25f);
        }
    }
}
