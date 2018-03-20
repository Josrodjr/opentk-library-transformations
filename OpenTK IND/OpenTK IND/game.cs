using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace OpenTK_IND
{
    public class game
    {
        double state = 1.0;
        double scalests = 0.5;
        double scalestsa = 0.5;
        double rotationx, rotationy, rotationz, rotationxa, rotationya, rotationza = 0.0;
        float translatex, translatey, shearx, sheary, translatexa, translateya, shearxa, shearya = 0.0f;
        int leaves, grass, wood, roof, bark, door;
        bool doorstatus = false;
        GameWindow window;
        public game(GameWindow window)
        {
            this.window = window;
            Start();
        }

        void Start()
        {
            window.Load += loaded;
            window.UpdateFrame += update;
            window.RenderFrame += renderF;
            window.Resize += resize;
            window.KeyPress += keyboard;
            window.Run(1.0 / 60.0);
        }

        void keyboard (object o, KeyPressEventArgs e)
        {
               if(e.KeyChar == 'q')
            {
                state = 1.0;
            }
               if(e.KeyChar == 'Q')
            {
                state = -1.0;
            }
               if(e.KeyChar == 'T' | e.KeyChar == 't')
            {
                doorstatus = true;
            }
            if (e.KeyChar == 'P' | e.KeyChar == 'p')
            {
                print_help();
            }
        }

        void resize (object o, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(0.785398f, window.Width / window.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void update(object o, EventArgs e)
        {
            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Key.Z))
            {
                rotationz += state;
            }
            if (k.IsKeyDown(Key.X))
            {
                rotationx += state;
            }
            if (k.IsKeyDown(Key.C))
            {
                rotationy += state;
            }
            if (k.IsKeyDown(Key.A))
            {
                translatex -= 0.5f;
            }
            if (k.IsKeyDown(Key.D))
            {
                translatex += 0.5f;
            }
            if (k.IsKeyDown(Key.W))
            {
                translatey += 0.5f;
            }
            if (k.IsKeyDown(Key.S))
            {
                translatey -= 0.5f;
            }
            if (k.IsKeyDown(Key.E))
            {
                
                scalests += 0.01d;
                
            }
            if (k.IsKeyDown(Key.R))
            {
                if(scalests > 0.02)
                {
                    scalests -= 0.01d;
                }
            }
            if (k.IsKeyDown(Key.F))
            {
                if(state == -1)
                {
                    shearx -= 0.1f;
                }
                if(state == 1)
                {
                    shearx += 0.1f;
                }
            }
            if (k.IsKeyDown(Key.V))
            {
                if (state == -1)
                {
                    sheary -= 0.1f;
                }
                if (state == 1)
                {
                    sheary += 0.1f;
                }
            }

            //para el arbol
            //traslanciones
            if (k.IsKeyDown(Key.J))
            {
                translatexa -= 0.5f;
            }
            if (k.IsKeyDown(Key.L))
            {
                translatexa += 0.5f;
            }
            if (k.IsKeyDown(Key.I))
            {
                translateya += 0.5f;
            }
            if (k.IsKeyDown(Key.K))
            {
                translateya -= 0.5f;
            }

            //rotaciones

            if (k.IsKeyDown(Key.B))
            {
                rotationza += state;
            }
            if (k.IsKeyDown(Key.N))
            {
                rotationxa += state;
            }
            if (k.IsKeyDown(Key.M))
            {
                rotationya += state;
            }

            //scale

            if (k.IsKeyDown(Key.Y))
            {

                scalestsa += 0.01d;

            }
            if (k.IsKeyDown(Key.U))
            {
                if (scalestsa > 0.02)
                {
                    scalestsa -= 0.01d;
                }
            }

            //shear

            if (k.IsKeyDown(Key.G))
            {
                if (state == -1)
                {
                    shearxa -= 0.1f;
                }
                if (state == 1)
                {
                    shearxa += 0.1f;
                }
            }
            if (k.IsKeyDown(Key.H))
            {
                if (state == -1)
                {
                    shearya -= 0.1f;
                }
                if (state == 1)
                {
                    shearya += 0.1f;
                }
            }


        }

        void shearobj (float SY, float SX)
        {

            var test2 = new Matrix4(1, SX, 0, 0,
                                    SY, 1, 0, 0,
                                    0, 0, 1, 0,
                                    0, 0, 0, 1);

            GL.MultMatrix(ref test2);

        }

        void animat_door(float SZ)
        {

            var test2 = new Matrix4(1, 0, SZ, 0,
                                    0, 1, 0, 0,
                                    0, 0, SZ-0.2f, 0,
                                    0, 0, 0, 1);

            GL.MultMatrix(ref test2);

        }

        void renderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //RENDEREAR LA PUERTA

            GL.PushMatrix();
            
            GL.Translate(translatex, translatey, -50.0f);
            GL.Rotate(rotationx, 1.0, 0.0, 0.0);
            GL.Rotate(rotationy, 0.0, 1.0, 0.0);
            GL.Rotate(rotationz, 0.0, 0.0, 1.0);
            GL.Scale(scalests, scalests, scalests);
            
            if (doorstatus == true)
            {
                animat_door(1.5f);
            }
            
            draw_door();

            GL.PopMatrix();

            //RENDEREAR LA CASA

            GL.PushMatrix();
           // GL.Translate(0f, 0f, -50.0f);
            GL.Translate(translatex, translatey, -50.0f);
            GL.Rotate(rotationx, 1.0, 0.0, 0.0);
            GL.Rotate(rotationy, 0.0, 1.0, 0.0);
            GL.Rotate(rotationz, 0.0, 0.0, 1.0);
            GL.Scale(scalests, scalests, scalests);
            //GL.Scale(0.5, 0.5, 0.5);

            shearobj(shearx, sheary);

            draw_house();

            GL.PopMatrix();

            //RENDEREAR EL ARBOL

            GL.PushMatrix();
            GL.Translate(25f, 0f, -35f);
            GL.Translate(translatexa, translateya, -50.0f);
            GL.Rotate(rotationxa, 1.0, 0.0, 0.0);
            GL.Rotate(rotationya, 0.0, 1.0, 0.0);
            GL.Rotate(rotationza, 0.0, 0.0, 1.0);
            GL.Scale(scalestsa, scalestsa, scalestsa);

            shearobj(shearxa, shearya);

            draw_tree();

            GL.PopMatrix();

            //RENDEREAR EL PLANO

            GL.PushMatrix();

            GL.Translate(0f, 20f, -35f);

            draw_plane();

            GL.PopMatrix();


            window.SwapBuffers();
        }

        void loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            //lightning
            
            GL.Enable(EnableCap.Lighting);
            
            float[] light_position = { 20, 20, -20 };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f };
            float[] light_ambient = { 1.0f, 1.0f, 1.0f };
            
            GL.Light(LightName.Light0, LightParameter.Position, light_position);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            
            GL.Enable(EnableCap.Light0);

            //texture
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out grass);
            GL.BindTexture(TextureTarget.Texture2D, grass);
            System.Drawing.Imaging.BitmapData texdata = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\grass.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdata.Width, texdata.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdata.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.GenTextures(1, out wood);
            GL.BindTexture(TextureTarget.Texture2D, wood);
            System.Drawing.Imaging.BitmapData texdataw = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\wood.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdataw.Width, texdataw.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdataw.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.GenTextures(1, out roof);
            GL.BindTexture(TextureTarget.Texture2D, roof);
            System.Drawing.Imaging.BitmapData texdatar = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\roof.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdatar.Width, texdatar.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdatar.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.GenTextures(1, out bark);
            GL.BindTexture(TextureTarget.Texture2D, bark);
            System.Drawing.Imaging.BitmapData texdatab = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\bark.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdatab.Width, texdatab.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdatab.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.GenTextures(1, out leaves);
            GL.BindTexture(TextureTarget.Texture2D, leaves);
            System.Drawing.Imaging.BitmapData texdatal = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\leaves.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdatal.Width, texdatal.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdatal.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.GenTextures(1, out door);
            GL.BindTexture(TextureTarget.Texture2D, door);
            System.Drawing.Imaging.BitmapData texdatad = loadImage(@"C:\Users\Josro\source\repos\OpenTK IND\OpenTK IND\res\door.bmp");
            GL.TexImage2D(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgb, texdatad.Width, texdatad.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, texdatad.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        void draw_plane()
        {
            GL.BindTexture(TextureTarget.Texture2D, grass);
            GL.Begin(BeginMode.Quads);

            //ABAJO
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-75, -25, -75);
            GL.TexCoord2(5, 0);
            GL.Vertex3(75, -25, -75);
            GL.TexCoord2(5, 5);
            GL.Vertex3(75, -25, 75);
            GL.TexCoord2(0, 5);
            GL.Vertex3(-75, -25, 75);

            GL.End();
        }

        void draw_tree()
        {
            GL.BindTexture(TextureTarget.Texture2D, bark);
            GL.Begin(BeginMode.Quads);

            //FRENTE
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-5, -10, -5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(5, -10, -5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(5, 0, -5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-5, 0, -5);

            //LADO DERECHOO
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(5, -10, -5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(5, -10, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(5, 0, 5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(5, 0, -5);

            //ATRAS
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-5, -10, 5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(5, -10, 5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(5, 0, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-5, 0, 5);

            //LADO IQUIERDO
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-5, -10, -5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-5, -10, 5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-5, 0, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-5, 0, -5);

            //ABAJO
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-5, -10, -5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(5, -10, -5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(5, -10, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-5, -10, 5);

            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, leaves);

            GL.Begin(BeginMode.Quads);

            //CUADRADO ABAJO
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(-10, 0, -5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(10, 0, -5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 0, 5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 0, 5);

            //CUADRADO IZQ
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10, 0, -5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 0, 5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(0, 20, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 20, -5);

            //CUADRADO DER
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10, 0, -5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 0, 5);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 20, 5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 20, -5);

            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, leaves);

            GL.Begin(BeginMode.Triangles);

            //TRIANGULO 1 FRENTE
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 0, -5);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 0, -5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 20, -5);

            //TRIANGULO 1 ATRAS
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10, 0, 5);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10, 0, 5);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 20, 5);

            GL.End();

        }

        void draw_door()
        {
            GL.BindTexture(TextureTarget.Texture2D, door);

            GL.Begin(BeginMode.Quads);

            //CARA 3 ATRAS
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-2, -10, 10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(2, -10, 10);
            GL.TexCoord2(0, 1);
            GL.Vertex3(2, 0, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(-2, 0, 10);

            GL.End();
        }

        void draw_house()
        {
            GL.BindTexture(TextureTarget.Texture2D, wood);

            GL.Begin(BeginMode.Quads);


            GL.Color3(1.0, 0.0, 1.0);

            //cuadrado
            //CARA 1 FRENTE
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, -10, -10);
            GL.TexCoord2(2, 0);
            GL.Vertex3(10, -10, -10);
            GL.TexCoord2(2, 2);
            GL.Vertex3(10, 10, -10);
            GL.TexCoord2(0, 2);
            GL.Vertex3(-10, 10, -10);

            //CARA 2 DERECHA
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.TexCoord2(0, 2);
            GL.Vertex3(10, 10, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10, -10, -10);
            GL.TexCoord2(2, 0);
            GL.Vertex3(10, -10, 10);
            GL.TexCoord2(2, 2);
            GL.Vertex3(10, 10, 10);

            //CARA 3 ATRAS
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(2, 0);
            GL.Vertex3(-10, -10, 10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10, -10, 10);
            GL.TexCoord2(0, 2);
            GL.Vertex3(10, 10, 10);
            GL.TexCoord2(2, 2);
            GL.Vertex3(-10, 10, 10);

            //CARA 4 IZQUIERDA
            GL.Normal3(1.0, 0.0, 0.0);
            GL.TexCoord2(2, 2);
            GL.Vertex3(-10, 10, -10);
            GL.TexCoord2(2, 0);
            GL.Vertex3(-10, -10, -10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, -10, 10);
            GL.TexCoord2(0, 2);
            GL.Vertex3(-10, 10, 10);

            //EL PISO ABAJO
            GL.Normal3(0.0, 1.0, 0.0);
            GL.TexCoord2(0, 2);
            GL.Vertex3(-10, -10, -10);
            GL.TexCoord2(2, 2);
            GL.Vertex3(10, -10, -10);
            GL.TexCoord2(2, 0);
            GL.Vertex3(10, -10, 10);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, -10, 10);

            GL.End();

            //TECHO
            GL.BindTexture(TextureTarget.Texture2D, roof);

            GL.Begin(BeginMode.Quads);
            //CUADRADO 2 DERECHO
            GL.Normal3(1.0, 1.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 10, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(-10, 10, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 15, 10);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 15, -10);

            //CUADRADO 1 IZQUIERDO
            GL.Normal3(-1.0, 1.0, 0.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(10, 10, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 10, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 15, 10);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0, 15, -10);
            GL.End();

            GL.BindTexture(TextureTarget.Texture2D, wood);
            GL.Begin(BeginMode.Triangles);

            //TRIANGULO 1 FRENTE
            GL.Normal3(0.0, 0.0, 1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 10, -10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 10, -10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 15, -10);

            //TRIANGULO 2 ATRAS
            GL.Normal3(0.0, 0.0, -1.0);
            GL.TexCoord2(0, 0);
            GL.Vertex3(-10, 10, 10);
            GL.TexCoord2(1, 0);
            GL.Vertex3(10, 10, 10);
            GL.TexCoord2(1, 1);
            GL.Vertex3(0, 15, 10);

            GL.End();
        }

        System.Drawing.Imaging.BitmapData loadImage(string filePath)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(filePath);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpdata = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            bmp.UnlockBits(bmpdata);

            return bmpdata;
        }

        void print_help()
        {
            System.Console.Clear();
            Console.WriteLine("Hola, bienvenido al menu de ayuda \nHemos agrupado el teclado en dos diferentes barras para realizar transformaciones con openGL\n");
            Console.WriteLine("El boton  mas importante de la aplicacion es" + " Q " + "ya que si es" + " q " + "las transformaciones se aplicaran en una direccion y si es" + " Q " + "se aplicaran en otra direccion\n");
            Console.WriteLine("CASA" + " A\t" + "| " + "ARBOL" + " J\t" + "Movimiento hacia izquierda\n");
            Console.WriteLine("CASA" + " S\t" + "| " + "ARBOL" + " K\t" + "Movimiento hacia abajo\n");
            Console.WriteLine("CASA" + " D\t" + "| " + "ARBOL" + " L\t" + "Movimiento hacia derecha\n");
            Console.WriteLine("CASA" + " W\t" + "| " + "ARBOL" + " I\t" + "Movimiento hacia arriba\n");
            Console.WriteLine("CASA" + " Z\t" + "| " + "ARBOL" + " B\t" + "Rotacion en Z\n");
            Console.WriteLine("CASA" + " X\t" + "| " + "ARBOL" + " N\t" + "Rotacion en X\n");
            Console.WriteLine("CASA" + " C\t" + "| " + "ARBOL" + " M\t" + "Rotacion en Y\n");
            Console.WriteLine("CASA" + " E\t" + "| " + "ARBOL" + " Y\t" + "Escala positiva\n");
            Console.WriteLine("CASA" + " R\t" + "| " + "ARBOL" + " U\t" + "Escala Negativa\n");
            Console.WriteLine("CASA" + " F\t" + "| " + "ARBOL" + " G\t" + "Shear en X\n");
            Console.WriteLine("CASA" + " V\t" + "| " + "ARBOL" + " H\t" + "Shear en Y\n");
            Console.WriteLine("CASA" + " A\t" + "| " + "ARBOL" + " J\t" + "Movimiento hacia izquierda\n");
            Console.WriteLine("CASA" + " T\t" + "| " + "Abrir Puerta\n");
        }
    }
}
