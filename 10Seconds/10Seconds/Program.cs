using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using SFML.Graphics;
using SFML.Window;

namespace Deleter.Tenseconds
{
    class Program
    {
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var time = new TimeMonitor(1000);

            // Request a 32-bits depth buffer when creating the window
            var contextSettings = new ContextSettings { DepthBits = 32 };

            // Create the main window
            var window = new RenderWindow(new VideoMode(1600, 900), "TenSeconds", Styles.Close | Styles.Titlebar, contextSettings);

            // Make it the active window for OpenGL calls
            window.SetActive();

            var windowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(window.SystemHandle);
            var graphicsContext = new OpenTK.Graphics.GraphicsContext(OpenTK.Graphics.GraphicsMode.Default, windowInfo);
            graphicsContext.MakeCurrent(windowInfo);
            graphicsContext.LoadAll();

            // Setup event handlers
            window.Closed += OnClosed;
            window.KeyPressed += OnKeyPressed;
            window.Resized += OnResized;

            var inputManager = new InputManager();
            inputManager.Init(window);
            var game = new Game();
            game.Init(window);

            while (window.IsOpen() && game.IsRunning)
            {
                time.Update();
                
                // Process events
                window.DispatchEvents();

                inputManager.Update(window);
                game.HandleInput(inputManager);

                game.Update(time);

                // Clear color and depth buffer
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                game.Draw(window);

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
