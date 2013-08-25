using System.Linq;
using SFML.Graphics;

namespace Deleter.Tenseconds.Graphics
{
    public class AnimationSet
    {
        private readonly Sprite[] _sprites;
        private readonly int _spriteCount;

        public int Count { get { return _spriteCount; }}

        public AnimationSet(Sprite[] sprites)
        {
            _sprites = sprites;
            _spriteCount = _sprites.Count();
        }

        public Sprite this[int key]
        {
            get { return _sprites[key]; }
        }
    }
}