using System;
using System.Runtime.InteropServices;
using OpenTK;
using SFML;
using SFML.Graphics;
using SFML.Window;
using OpenTK.Graphics.OpenGL;

namespace Deleter.Tenseconds
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Request a 32-bits depth buffer when creating the window
            var contextSettings = new ContextSettings {DepthBits = 32};

            // Create the main window
            var window = new RenderWindow(new VideoMode(1600, 900), "TenSeconds", Styles.Close | Styles.Titlebar, contextSettings);

            // Make it the active window for OpenGL calls
            window.SetActive();

            // Setup event handlers
            window.Closed     += OnClosed;
            window.KeyPressed += OnKeyPressed;
            window.Resized    += OnResized;
            
            bool hasFocus = true;
            int mouseWheel = 0;

            window.GainedFocus += (sender, args) => hasFocus = true;
            window.LostFocus += (sender, args) => hasFocus = false;
            window.MouseWheelMoved += (sender, args) => mouseWheel += args.Delta;

            var windowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(window.SystemHandle);
            var graphicsContext = new OpenTK.Graphics.GraphicsContext(OpenTK.Graphics.GraphicsMode.Default, windowInfo);
            graphicsContext.MakeCurrent(windowInfo);
            graphicsContext.LoadAll();

            // Set the color and depth clear values
            GL.ClearDepth(1);
            GL.ClearColor(0,0,0,1);
            
            var texture = new Texture("Assets/test.png");
            var sprite = new Sprite(texture);

            int startTime = Environment.TickCount;

            var mousePos = Mouse.GetPosition(window);

            // Start the game loop
            while (window.IsOpen())
            {
                // Process events
                window.DispatchEvents();

                // Clear color and depth buffer
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // Apply some transformations
                float time = (Environment.TickCount - startTime) / 1000.0F;

                if (hasFocus)
                {
                    var newMousePos = Mouse.GetPosition(window);
                    if (newMousePos.X >= 0 && newMousePos.X < window.Size.X && newMousePos.Y >= 0 && newMousePos.Y < window.Size.Y)
                    {
                        mousePos = newMousePos;
                    }
                }

                sprite.Position = new Vector2f((1600 * time) % 1600, mousePos.Y );

                window.Draw(sprite);
                
                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        private static void OnClosed(object sender, EventArgs e)
        {
            var window = (Window)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        private static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

        /// <summary>
        /// Function called when the window is resized
        /// </summary>
        private static void OnResized(object sender, SizeEventArgs e)
        {
            GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
        }
    }
}
