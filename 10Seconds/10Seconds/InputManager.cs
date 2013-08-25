using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace Deleter.Tenseconds
{
    public class InputManager : IDisposable
    {
        private bool _hasFocus;
        private Action _unregister;
        private int _mouseWheel;
        private Vector2i _mousePos;

        public Vector2i MousePosition
        {
            get { return _mousePos; }
        }

        public int MouseWheel
        {
            get { return _mouseWheel; }
        }

        public InputManager()
        {
            _hasFocus = true;
            _mouseWheel = 0;
        }

        public void Init(RenderWindow window)
        {
            window.GainedFocus += OnGainedFocus;
            window.LostFocus += OnLostFocus;
            window.MouseWheelMoved += OnMouseWheelMoved;

            _unregister = () =>
            {
                window.GainedFocus -= OnGainedFocus;
                window.LostFocus -= OnLostFocus;
                window.MouseWheelMoved -= OnMouseWheelMoved;
            };
        }

        private void OnGainedFocus(object sender, EventArgs args)
        {
            _hasFocus = true;
        }

        private void OnLostFocus(object sender, EventArgs args)
        {
            _hasFocus = false;
        }

        private void OnMouseWheelMoved(object sender, MouseWheelEventArgs args)
        {
            if (_hasFocus)
            {
                _mouseWheel += args.Delta;
            }
        }

        public void Update(RenderWindow window)
        {
            if (_hasFocus)
            {
                var newMousePos = Mouse.GetPosition(window);
                if (newMousePos.X >= 0 && newMousePos.X < window.Size.X && newMousePos.Y >= 0 && newMousePos.Y < window.Size.Y)
                {
                    _mousePos = newMousePos;
                }
            }
        }

        public void Dispose()
        {
            _unregister.Invoke();
        }
    }
}
