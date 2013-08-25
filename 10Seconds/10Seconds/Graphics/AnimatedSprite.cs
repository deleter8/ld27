using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace Deleter.Tenseconds.Graphics
{
    public class AnimatedSprite
    {
        #region Fields

        private readonly AnimationMap _animationMap;
        private int _frame;
        private float _timeSinceLastFrameChange;

        #endregion

        #region Properties

        public AnimationDirection Direction { get; set; }

        public AnimationAction Action { get; set; }

        public float FrameDelay { get; set; }

        public Sprite CurrentFrame 
        {
            get 
            { 
                var set = _animationMap[Direction][Action]; 
                return set[_frame % set.Count]; 
            }
        }

        #endregion

        #region Constructors

        public AnimatedSprite(AnimationMap animationMap)
        {
            _animationMap = animationMap;
            _frame = 0;
            _timeSinceLastFrameChange = 0;
        }

        #endregion

        #region Methods

        public void Update(float delta)
        {
            _timeSinceLastFrameChange += delta;

            if (_timeSinceLastFrameChange > FrameDelay)
            {
                _timeSinceLastFrameChange -= FrameDelay;
                _frame++;
            }
        }

        #endregion
    }
}
