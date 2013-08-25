using System;

namespace Deleter.Tenseconds.Graphics
{
    public class AnimationMap
    {
        private readonly AnimationSet[] _frames;
        private readonly Indexer[] _indexers;

        public AnimationMap()
        {
            _frames = new AnimationSet[(int)AnimationDirection.Right + (int)AnimationAction.Attacking + 1];
            
            _indexers = new Indexer[4];
            foreach (var indexer in Enum.GetValues(typeof(AnimationDirection)))
            {
                var direction = (int) indexer;
                _indexers[direction] = new Indexer(_frames, direction);
            }
        }

        public AnimationMap Add(AnimationAction action, AnimationDirection direction, AnimationSet set)
        {
            _frames[(int) action | (int) direction] = set;
            return this;
        }

        public Indexer this[AnimationDirection key]
        {
            get { return _indexers[(int) key]; }
        }
        
        #region Indexer
        public class Indexer
        {
            private int _direction;
            private readonly AnimationSet[] _frames;

            public Indexer(AnimationSet[] frames, int direction)
            {
                _frames = frames;
                _direction = direction;
            }

            public AnimationSet this[AnimationAction key]
            {
                get { return _frames[(int) key | _direction]; }
            }
        }
        #endregion
    }
}