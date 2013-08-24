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
            var window = new RenderWindow(new VideoMode(1600, 900), "Test SFML window with OpenGL via OpenTK", Styles.Close | Styles.Titlebar, contextSettings);

            // Make it the active window for OpenGL calls
            window.SetActive();

            // Setup event handlers
            window.Closed     += OnClosed;
            window.KeyPressed += OnKeyPressed;
            window.Resized    += OnResized;

            var windowInfo = OpenTK.Platform.Utilities.CreateWindowsWindowInfo(window.SystemHandle);
            var graphicsContext = new OpenTK.Graphics.GraphicsContext(OpenTK.Graphics.GraphicsMode.Default, windowInfo);
            graphicsContext.MakeCurrent(windowInfo);
            graphicsContext.LoadAll();
            //OpenTK.Graphics.GraphicsContext.CreateDummyContext(new ContextHandle(window.SystemHandle));

            // Set the color and depth clear values
            GL.ClearDepth(1);
            GL.ClearColor(0,0,0,1);

            //// Enable Z-buffer read and write
            //GL.Enable(EnableCap.DepthTest);
            //GL.DepthMask(true);

            //// Disable lighting and texturing
            //GL.Disable(EnableCap.Lighting);
            //GL.Disable(EnableCap.Texture2D);
            
            //// Configure the viewport (the same size as the window)
            //GL.Viewport(0,0, (int)window.Size.X, (int)window.Size.Y);

            //// Setup a perspective projection
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            //float ratio = (float)(window.Size.X) / window.Size.Y;
            //GL.Frustum(-ratio, ratio, -1, 1, 1, 500);

            //// Define a 3D cube (6 faces made of 2 triangles composed by 3 vertices)
            //float[] cube = new float[]
            //{
            //    // positions    // colors (r, g, b, a)
            //    -50, -50, -50,  0, 0, 1, 1,
            //    -50,  50, -50,  0, 0, 1, 1,
            //    -50, -50,  50,  0, 0, 1, 1,
            //    -50, -50,  50,  0, 0, 1, 1,
            //    -50,  50, -50,  0, 0, 1, 1,
            //    -50,  50,  50,  0, 0, 1, 1,

            //     50, -50, -50,  0, 1, 0, 1,
            //     50,  50, -50,  0, 1, 0, 1,
            //     50, -50,  50,  0, 1, 0, 1,
            //     50, -50,  50,  0, 1, 0, 1,
            //     50,  50, -50,  0, 1, 0, 1,
            //     50,  50,  50,  0, 1, 0, 1,

            //    -50, -50, -50,  1, 0, 0, 1,
            //     50, -50, -50,  1, 0, 0, 1,
            //    -50, -50,  50,  1, 0, 0, 1,
            //    -50, -50,  50,  1, 0, 0, 1,
            //     50, -50, -50,  1, 0, 0, 1,
            //     50, -50,  50,  1, 0, 0, 1,

            //    -50,  50, -50,  0, 1, 1, 1,
            //     50,  50, -50,  0, 1, 1, 1,
            //    -50,  50,  50,  0, 1, 1, 1,
            //    -50,  50,  50,  0, 1, 1, 1,
            //     50,  50, -50,  0, 1, 1, 1,
            //     50,  50,  50,  0, 1, 1, 1,

            //    -50, -50, -50,  1, 0, 1, 1,
            //     50, -50, -50,  1, 0, 1, 1,
            //    -50,  50, -50,  1, 0, 1, 1,
            //    -50,  50, -50,  1, 0, 1, 1,
            //     50, -50, -50,  1, 0, 1, 1,
            //     50,  50, -50,  1, 0, 1, 1,

            //    -50, -50,  50,  1, 1, 0, 1,
            //     50, -50,  50,  1, 1, 0, 1,
            //    -50,  50,  50,  1, 1, 0, 1,
            //    -50,  50,  50,  1, 1, 0, 1,
            //     50, -50,  50,  1, 1, 0, 1,
            //     50,  50,  50,  1, 1, 0, 1,
            //};

            //// Enable position and color vertex components
            //GL.EnableClientState(ArrayCap.VertexArray);
            //GL.EnableClientState(ArrayCap.ColorArray);
            //GL.VertexPointer(3, VertexPointerType.Float, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 0));
            //GL.ColorPointer(4, ColorPointerType.Float, 7 * sizeof(float), Marshal.UnsafeAddrOfPinnedArrayElement(cube, 3));

            //// Disable normal and texture coordinates vertex components
            //GL.DisableClientState(ArrayCap.NormalArray);
            //GL.DisableClientState(ArrayCap.TextureCoordArray);
            var texture = new Texture("Assets/test.png");
            var sprite = new Sprite(texture);

            int startTime = Environment.TickCount;

            // Start the game loop
            while (window.IsOpen())
            {
                // Process events
                window.DispatchEvents();

                // Clear color and depth buffer
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // Apply some transformations
                float time = (Environment.TickCount - startTime) / 1000.0F;
                
                //GL.MatrixMode(MatrixMode.Modelview);
                //GL.LoadIdentity();
                //GL.Translate(0.0F, 0.0F, -200.0F);
                //GL.Rotate(time * 50, 1.0F, 0.0F, 0.0F);
                //GL.Rotate(time * 30, 0.0F, 1.0F, 0.0F);
                //GL.Rotate(time * 90, 0.0F, 0.0F, 1.0F);

                //// Draw the cube
                //GL.DrawArrays(BeginMode.Triangles, 0, 36);
                var mousePos = Mouse.GetPosition(window);

                sprite.Position = new Vector2f((1600 * time) % 1600, mousePos.Y );

                window.Draw(sprite);
                
                // Finally, display the rendered frame on screen
                window.Display();
            }
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
        }

        /// <summary>
        /// Function called when the window is resized
        /// </summary>
        static void OnResized(object sender, SizeEventArgs e)
        {
            GL.Viewport(0, 0, (int)e.Width, (int)e.Height);
        }
    }
}
