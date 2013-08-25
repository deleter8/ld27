using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deleter.Tenseconds.Graphics;
using SFML.Graphics;

namespace Deleter.Tenseconds
{
    public class ResourceManager
    {
        #region Singleton Implementation
        
        private static readonly Lazy<ResourceManager> _instance = new Lazy<ResourceManager>(() => new ResourceManager());

        public static ResourceManager Instance { get { return _instance.Value; } }

        private ResourceManager()
        {
        }

        #endregion

        private readonly Dictionary<string, Texture> _spriteTextures = new Dictionary<string, Texture>();
        private readonly Dictionary<string, AnimationMap> _animationMaps = new Dictionary<string, AnimationMap>();
        private readonly Dictionary<string, Font> _fonts = new Dictionary<string, Font>();

        public Sprite GetSprite(string fileName)
        {
            Texture texture;
            if (!_spriteTextures.TryGetValue(fileName, out texture))
            {
                texture = new Texture(fileName);
                _spriteTextures[fileName] = texture;
            }

            return new Sprite(texture);
        }

        public AnimationSet GetSpriteSet(string[] fileNames)
        {
            return new AnimationSet(fileNames.Select(GetSprite).ToArray());
        }


        public Font GetFont(string fileName)
        {
            Font font;
            if (!_fonts.TryGetValue(fileName, out font))
            {
                font = new Font(fileName);
                _fonts[fileName] = font;
            }

            return font;
        }

        public AnimationMap GetAnimationMap(string name)
        {
            AnimationMap map;
            if (!_animationMaps.TryGetValue(name, out map))
            {
                map = new AnimationMap();
                _animationMaps[name] = map;
            }

            return map;
        }

        public AnimatedSprite GetAnimatedSprite(string mapName)
        {
            AnimationMap map;
            if (!_animationMaps.TryGetValue(mapName, out map))
            {
                throw new ArgumentException(string.Format("No such argument map: {0}", mapName));
            }

            return new AnimatedSprite(map);
        }
    }
}
