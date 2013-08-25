using System;
using Deleter.Tenseconds.Graphics;
using SFML.Graphics;
using SFML.Window;

namespace Deleter.Tenseconds
{
    public class Game
    {
        private Sprite _testSprite;
        private Font _font;
        private Text _text;
        private Text _text2;
        private Vector2i _mousePos;

        public bool IsRunning
        {
            get { return true; }
        }

        public void Init(RenderWindow window)
        {
            _font = ResourceManager.Instance.GetFont("Assets/arial.ttf");
            _testSprite = ResourceManager.Instance.GetSprite("Assets/test.png");

            _text = new Text("Fps: 0", _font, 24);
            _text2 = new Text("Average Fps: 0", _font, 24);
        }

        public void HandleInput(InputManager inputManager)
        {
            _mousePos = inputManager.MousePosition;
        }

        /// <summary>
        /// The main game loop
        /// </summary>
        public void Update(TimeMonitor time)
        {
            _text.DisplayedString = "Fps: " + time.CurrentFps;
            _text2.DisplayedString = "Average Fps: " + time.AverageFps;
            _text.Position = new Vector2f(10, 10);
            _text2.Position = new Vector2f(10, 58);

            _testSprite.Scale = new Vector2f(((float)Math.Sin(time.TotalTime * 10) / 2 + 1) * 3, ((float)Math.Cos(time.TotalTime * 3) / 2 + 1) * 3);
            _testSprite.Position = new Vector2f(_mousePos.X - (_testSprite.Texture.Size.X / 2) * _testSprite.Scale.X, _mousePos.Y - (_testSprite.Texture.Size.Y / 2) * _testSprite.Scale.Y);
            
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(_testSprite);
            window.Draw(_text);
            window.Draw(_text2);
        }



        private void SetupAnimations()
        {
            var sprites = ResourceManager.Instance.GetSpriteSet(new string[] {"test.png"});
            ResourceManager.Instance.GetAnimationMap("test").Add(AnimationAction.Idle, AnimationDirection.Up, sprites)
        }
    }
}
