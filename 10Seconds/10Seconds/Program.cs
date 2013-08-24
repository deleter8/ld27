using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenTK;
using SFML;
using SFML.Graphics;
using SFML.Window;
using OpenTK.Graphics.OpenGL;
using System.Linq;

namespace Deleter.Tenseconds
{
    class Program
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

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
            
            var font = new Font("Assets/arial.ttf");
            
            var texture = new Texture("Assets/test.png");
            var sprite = new Sprite(texture);

            int startTime = Environment.TickCount;

            var mousePos = Mouse.GetPosition(window);

            const int fpsFramesToTrack = 1000;
            var fpsFrames = new float[fpsFramesToTrack];
            for (int i = 0; i < fpsFrames.Length; i++)
            {
                fpsFrames[i] = 0;
            }

            var fpsFrame = 0;

            long lastTickCount = 0;
            var tickCount = lastTickCount;
            long tickCountFrequency;
            QueryPerformanceFrequency(out tickCountFrequency);

            var watch = new Stopwatch();
            watch.Start();
            
            // Start the game loop
            while (window.IsOpen())
            {
                // Process events
                window.DispatchEvents();

                long newCount;
                QueryPerformanceCounter(out newCount);
                tickCount = (int) (newCount - lastTickCount);
                lastTickCount = newCount;

                // Clear color and depth buffer
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // Apply some transformations
                float time = (Environment.TickCount - startTime) / 1000.0F;

                fpsFrames[(fpsFrame++) % fpsFramesToTrack] = (float)tickCount / tickCountFrequency;// / 1000.0F;
                var fps = (int)(1/fpsFrames.Average());

                var text = new Text("Fps: " + fps, font, 24);
                var text2 = new Text("Average Fps: " + (int)((fpsFrame) / (watch.Elapsed.TotalMilliseconds / 1000)), font, 24);
                text.Position = new Vector2f(10,10);
                text2.Position = new Vector2f(10, 58);

                if (hasFocus)
                {
                    var newMousePos = Mouse.GetPosition(window);
                    if (newMousePos.X >= 0 && newMousePos.X < window.Size.X && newMousePos.Y >= 0 && newMousePos.Y < window.Size.Y)
                    {
                        mousePos = newMousePos;
                    }
                }

                sprite.Scale = new Vector2f(((float)Math.Sin(time*10) / 2 + 1) * 3, ((float)Math.Cos(time*3) / 2 + 1)*3);
                sprite.Position = new Vector2f(mousePos.X - (sprite.Texture.Size.X / 2) * sprite.Scale.X, mousePos.Y - (sprite.Texture.Size.Y / 2) * sprite.Scale.Y);

                window.Draw(sprite);
                window.Draw(text);
                window.Draw(text2);

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
