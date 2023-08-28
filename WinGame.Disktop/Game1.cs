using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.ViewportAdapters;
using System;
 

namespace WinGame.Disktop
{


    // http://vodacek.zvb.cz/archiv/604.html
    // https://github.com/z2oh/C3.MonoGame.Primitives2D/blob/master/Primitives2D.cs

    public class Game1 : Game
    {
        private GuiSystem _guiSystem;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _iconTexture;
        private VertexPositionColor[] _vertexPositionColors;
        private BasicEffect _basicEffect;
        private Texture2D _textTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.PreferMultiSampling = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            this._iconTexture = Content.Load<Texture2D>("Images/Icon");
            // TODO: use this.Content to load your game content here
            _vertexPositionColors = new[]
                {
                    new VertexPositionColor(new Vector3(100, 100, 0), Color.Red),
                    new VertexPositionColor(new Vector3(200, 100, 0), Color.Lavender),
                    new VertexPositionColor(new Vector3(200, 200, 0), Color.Yellow),
                    new VertexPositionColor(new Vector3(200, 200, 0), Color.Gold),
                    new VertexPositionColor(new Vector3(100, 200, 0), Color.Green),
                    new VertexPositionColor(new Vector3(100, 100, 0), Color.Blue),
               };
            _basicEffect = new BasicEffect(GraphicsDevice);
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.World = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0, 1);

            var font = Content.Load<BitmapFont>("Sensation");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);


 


            var stackTest = new DemoViewModel("Stack Panels",
                    new StackPanel
                    {
                        Items =
                        {
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom  },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Centre, VerticalAlignment = VerticalAlignment.Centre  },
                            new Button { Content = "Press Me", HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch },
                        }
                    });

            var dockTest = new DemoViewModel("Dock Panels",
                new DockPanel
                {
                    Items =
                    {
                        new Button { Content = "Dock.Top", AttachedProperties = { { DockPanel.DockProperty, Dock.Top } } },
                        new Button { Content = "Dock.Bottom", AttachedProperties = { { DockPanel.DockProperty, Dock.Bottom } } },
                        new Button { Content = "Dock.Left", AttachedProperties = { { DockPanel.DockProperty, Dock.Left } } },
                        new Button { Content = "Dock.Right", AttachedProperties = { { DockPanel.DockProperty, Dock.Right } } },
                        new Button { Content = "Fill" }
                    }
                });

            var controlTest = new DemoViewModel("Basic Controls",
                new StackPanel
                {
                    Margin = 5,
                    Orientation = Orientation.Vertical,
                    Items =
                    {
                        new Label("Buttons") { Margin = 5 },
                        new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Spacing = 5,
                            Items =
                            {
                                new Button { Content = "Enabled" },
                                new Button { Content = "Disabled", IsEnabled = false },
                                new ToggleButton { Content = "ToggleButton" }
                            }
                        },

                        new Label("TextBox") { Margin = 5 },
                        new TextBox {Text = "TextBox" },

                        new Label("CheckBox") { Margin = 5 },
                        new CheckBox {Content = "Check me please!"},

                        new Label("ListBox") { Margin = 5 },
                        new ListBox {Items = {"ListBoxItem1", "ListBoxItem2", "ListBoxItem3"}, SelectedIndex = 0},

                        new Label("ProgressBar") { Margin = 5 },
                        new ProgressBar {Progress = 0.5f, Width = 100},

                        new Label("ComboBox") { Margin = 5 },
                        new ComboBox {Items = {"ComboBoxItemA", "ComboBoxItemB", "ComboBoxItemC"}, SelectedIndex = 0, HorizontalAlignment = HorizontalAlignment.Left}
                    }
                });

            var demoScreen = new Screen
            {
                Content = new DockPanel
                {
                    LastChildFill = true,
                    Items =
                    {
                        new ListBox
                        {
                            Name = "DemoList",
                            AttachedProperties = { { DockPanel.DockProperty, Dock.Left} },
                            ItemPadding = new Thickness(5),
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            SelectedIndex = 0,
                            Items = { controlTest, stackTest, dockTest }
                        },
                        new ContentControl
                        {
                            Name = "Content",
                            BackgroundColor = new Color(30, 30, 30)
                        }
                    }
                }
            };
            var viewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
            var guiRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);
            _guiSystem = new GuiSystem(viewportAdapter, guiRenderer) { ActiveScreen = demoScreen };

            var demoList = demoScreen.FindControl<ListBox>("DemoList");
            var demoContent = demoScreen.FindControl<ContentControl>("Content");

            demoList.SelectedIndexChanged += (sender, args) => demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;
            demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;


            _textTexture = DrawString("Hello，你好啊！@#￥%%……&", 0, 0, Color.Red, new System.Drawing.Font("Consolas", 14, System.Drawing.FontStyle.Bold));
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            value -= 1;
            if (value < 0) value = 360;


            _guiSystem.Update(gameTime);


            base.Update(gameTime);
        }

        private Double value = 360;





        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here
            this.DrawMarkRect(new Rectangle(200, 200, 100, 100), value, new Color(255, 0, 0, 80));
            this.Fill(new Rectangle(100, 300, 100, 100), new Color(255, 0, 0, 80));
            //_guiSystem.Draw(gameTime);
            _spriteBatch.Begin();
            _spriteBatch.Draw(this._iconTexture, new Rectangle(0, 0, 64, 64), Color.White);
            Primitives2D.DrawLine(_spriteBatch, new Vector2(100, 100), new Vector2(200, 300), Color.Red, 5.0f);

            
            _spriteBatch.Draw(_textTexture, new Rectangle(0, 0, _textTexture.Width, _textTexture.Height), Color.Yellow); //...and draw it!


            _spriteBatch.End();

 

            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.RasterizerState = new RasterizerState
            //{
            //    CullMode = CullMode.None,
            //};

            //GraphicsDevice.BlendState = BlendState.Opaque;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            //_basicEffect.CurrentTechnique.Passes[0].Apply();
            //GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexPositionColors, 0, 2);



            //EffectPassCollection effectPassCollection = _basicEffect.CurrentTechnique.Passes;
            //foreach (EffectPass pass in effectPassCollection)
            //{
            //    pass.Apply();
            //    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexPositionColors, 0, 2);
            //}





            base.Draw(gameTime);
        }






        public Vector2 MeasureString(string text, System.Drawing.Font font, int wrapWidth = int.MaxValue)
        {
            using (var gfx = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)))
            {
                var s = gfx.MeasureString(text, font, wrapWidth); //SmartMeasureString is an extension method I made for System.Drawing.Graphics which applies text rendering hints and formatting rules that I need to make text rendering and measurement accurate and usable without copy-pasting the same code.
                return new Vector2((float)Math.Ceiling(s.Width), (float)Math.Ceiling(s.Height)); //Better to round up the values returned by SmartMeasureString - it's just easier math-wise to deal with whole numbers
            }
        }

        public Texture2D DrawString(string text, int x, int y, Color color, System.Drawing.Font font, int wrapWidth = 0)
        {
            //_startx and _starty are used for making sure coordinates are relative to the clip bounds of the current context
            Vector2 measure;
            if (wrapWidth == 0)
                measure = MeasureString(text, font);
            else
                measure = MeasureString(text, font, wrapWidth);
            using (var bmp = new System.Drawing.Bitmap((int)measure.X, (int)measure.Y))
            {
                using (var gfx = System.Drawing.Graphics.FromImage(bmp))
                {
                    //gfx.Clear(System.Drawing.Color.Red);
                    var textformat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
                    textformat.FormatFlags = System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
                    textformat.Trimming = System.Drawing.StringTrimming.None;
                    textformat.FormatFlags |= System.Drawing.StringFormatFlags.NoClip; //without this, text gets cut off near the right edge of the string bounds

                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel; //Anything but this and performance takes a dive.
                    gfx.DrawString(text, font, new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B)), 0, 0, textformat);
                }

                var lck = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); //Lock the bitmap in memory and give us the ability to extract data from it so we can load it into a Texture2D
                var data = new byte[Math.Abs(lck.Stride) * lck.Height]; //destination array for bitmap data, source for texture data
                System.Runtime.InteropServices.Marshal.Copy(lck.Scan0, data, 0, data.Length); //cool, data's in the destination array
                bmp.UnlockBits(lck); //Unlock the bits. We don't need 'em.
                var tex2 = new Texture2D(GraphicsDevice, bmp.Width, bmp.Height);
                
                    for (int i = 0; i < data.Length; i += 4)
                    {
                        byte r = data[i];
                        byte b = data[i + 2];
                        data[i] = b;
                        data[i + 2] = r;
                    } //This code swaps the red and blue values of each pixel in the bitmap so that they are arranged as BGRA. If we don't do this, we get weird rendering glitches where red text is blue etc.

                    tex2.SetData<byte>(data); //Load the data into the texture
                    //_spriteBatch.Draw(tex2, new Rectangle(x, y, bmp.Width, bmp.Height), Color.Red); //...and draw it!
                return tex2;
            }
        }





        public void DrawMarkRect(Rectangle rect, Double Angle, Color dwColor)
        {
            VertexPositionColor vResult;
            VertexPositionColor[] vbData = new VertexPositionColor[15];
            //  规定中心点
            var nTriangles = 0;
            var Cx = rect.Left + (float)rect.Width / 2;
            var Cy = rect.Top + (float)rect.Height / 2;
            var w2 = (float)rect.Width / 2;
            var h2 = (float)rect.Height / 2;

            // taiangle 1
            vbData[00] = new VertexPositionColor(new Vector3(rect.Left, rect.Top, 0), dwColor);
            vbData[01] = new VertexPositionColor(new Vector3(Cx, rect.Top, 0), dwColor);
            vbData[02] = new VertexPositionColor(new Vector3(Cx, Cy, 0), dwColor);
            // taiangle 2                                           
            vbData[03] = new VertexPositionColor(new Vector3(rect.Left, rect.Bottom, 0), dwColor);
            vbData[04] = new VertexPositionColor(new Vector3(rect.Left, rect.Top, 0), dwColor);
            vbData[05] = new VertexPositionColor(new Vector3(Cx, Cy, 0), dwColor);
            // taiangle 3                                           
            vbData[06] = new VertexPositionColor(new Vector3(rect.Right, rect.Bottom, 0), dwColor);
            vbData[07] = new VertexPositionColor(new Vector3(rect.Left, rect.Bottom, 0), dwColor);
            vbData[08] = new VertexPositionColor(new Vector3(Cx, Cy, 0), dwColor);
            // taiangle 4                                           
            vbData[09] = new VertexPositionColor(new Vector3(rect.Right, rect.Top, 0), dwColor);
            vbData[10] = new VertexPositionColor(new Vector3(rect.Right, rect.Bottom, 0), dwColor);
            vbData[11] = new VertexPositionColor(new Vector3(Cx, Cy, 0), dwColor);
            // taiangle 5                                           
            vbData[12] = new VertexPositionColor(new Vector3(Cx, rect.Top, 0), dwColor);
            vbData[13] = new VertexPositionColor(new Vector3(rect.Right, rect.Top, 0), dwColor);
            vbData[14] = new VertexPositionColor(new Vector3(Cx, Cy, 0), dwColor);

            //Rem  计算此时秒针的向量
            Angle = Angle * Math.PI / 180;
            var a2 = Math.Atan(w2 / h2);
            if (Angle < a2)
            {
                nTriangles = 1;
                vResult.Position.Y = -h2;
                vResult.Position.X = -h2 * (Single)Math.Tan(Angle);
            }
            else if (Angle < Math.PI - a2)
            {
                Angle = Angle - Math.PI / 2;
                nTriangles = 2;
                vResult.Position.Y = w2 * (Single)Math.Tan(Angle);
                vResult.Position.X = -w2;
            }
            else if (Angle < Math.PI + a2)
            {
                nTriangles = 3;
                vResult.Position.Y = h2;
                vResult.Position.X = h2 * (Single)Math.Tan(Angle);
            }
            else if (Angle < 2 * Math.PI - a2)
            {
                Angle = Angle - Math.PI / 2;
                nTriangles = 4;
                vResult.Position.Y = -w2 * (Single)Math.Tan(Angle);
                vResult.Position.X = w2;
            }
            else
            {
                nTriangles = 5;
                vResult.Position.Y = -h2;
                vResult.Position.X = -h2 * (Single)Math.Tan(Angle);
            }
            vbData[nTriangles *3 - 3].Position.X = Cx + vResult.Position.X;
            vbData[nTriangles *3 - 3].Position.Y = Cy + vResult.Position.Y;
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vbData, 0, nTriangles);


            //using (var s = new RenderBlock(directX_context, RenderMode.Geometry))
            //{
            //    directX_context.D3DDevice.RenderState.FillMode = FillMode.Solid;
            //    directX_context.D3DDevice.RenderState.CullMode = Cull.None;
            //    directX_context.D3DDevice.VertexFormat = CustomVertex.TransformedColored.Format;
            //    directX_context.D3DDevice.SetTexture(0, null);
            //    directX_context.D3DDevice.DrawUserPrimitives(PrimitiveType.TriangleFan, nTriangles, vbData);
            //}
        }



        /// <summary>
        /// 用颜色填充矩形
        /// </summary>
        /// <param name="rect">位置与大小</param>
        /// <param name="nColor">颜色</param>
        public void Fill(Rectangle rect, Color dwColor)
        {
            VertexPositionColor[] vbData = new VertexPositionColor[4];
            vbData[0] = new VertexPositionColor(new Vector3(rect.Left, rect.Top, 0), dwColor); 
            vbData[1] = new VertexPositionColor(new Vector3(rect.Right, rect.Top, 0), dwColor);
            vbData[2] = new VertexPositionColor(new Vector3(rect.Left, rect.Bottom, 0), dwColor);
            vbData[3] = new VertexPositionColor(new Vector3(rect.Right, rect.Bottom, 0), dwColor);
            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vbData, 0, 2);
        }




    }



    public class DemoViewModel
    {
        public DemoViewModel(string name, object content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; }
        public object Content { get; }

        public override string ToString()
        {
            return Name;
        }
    }

}







///// <summary>
///// 画一个点
///// </summary>
///// <param name="point"></param>
///// <param name="nColor"></param>
///// <param name="width"></param>
//public void DrawPoint(System.Drawing.Point point, D3DColor nColor, Int32 width = 1)
//{
//    CustomVertex.TransformedColored[] vbData = new CustomVertex.TransformedColored[8];
//    vbData[0] = new CustomVertex.TransformedColored();
//    vbData[0].Color = nColor.ToArgb();
//    vbData[0].Position = new Vector4(point.X, point.Y, 0, 1);
//    if (!directX_context.DeviceLosting)
//    {
//        using (var s = new RenderBlock(directX_context, RenderMode.Geometry))
//        {
//            directX_context.D3DDevice.DrawUserPrimitives(PrimitiveType.PointList, 2, vbData);
//        }
//    }
//}

///// <summary>
///// 画一个矩形边框
///// </summary>
///// <param name="rect">位置和大小</param>
///// <param name="nColor">边框颜色</param>
///// <param name="width">边框宽度</param>
//public void DrawRect(System.Drawing.Rectangle rect, D3DColor nColor, Int32 width = 1)
//{
//    Vector2[] vector = new Vector2[]
//    {
//                new Vector2(rect.Left,rect.Top),
//                new Vector2(rect.Right,rect.Top),
//                new Vector2(rect.Right,rect.Bottom),
//                new Vector2(rect.Left,rect.Bottom),
//                new Vector2(rect.Left,rect.Top),
//    };
//    directX_context.Line.Width = width;
//    directX_context.Line.Antialias = true;
//    directX_context.Line.Begin();
//    directX_context.Line.Draw(vector, nColor.ToArgb());
//    directX_context.Line.End();
//}

///// <summary>
///// 画多条线
///// </summary>
///// <param name="points">线的点</param>
///// <param name="nColor">颜色</param>
///// <param name="width">线宽度</param>
//public void DrawLines(IEnumerable<System.Drawing.Point> points, D3DColor nColor, Int32 width = 1)
//{
//    var Points = (from s in points select new Vector2(s.X, s.Y)).ToArray();
//    directX_context.Line.Width = width;
//    directX_context.Line.Antialias = true;
//    directX_context.Line.Begin();
//    directX_context.Line.Draw(Points, nColor.ToArgb());
//    directX_context.Line.End();
//}

