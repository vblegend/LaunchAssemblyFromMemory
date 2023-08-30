using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using System;

namespace WinGame.Disktop
{


    // http://vodacek.zvb.cz/archiv/604.html
    // https://github.com/z2oh/C3.MonoGame.Primitives2D/blob/master/Primitives2D.cs
    // https://github.com/Zintom/BitmapTextRenderer/blob/master/BitmapTextRenderer.cs

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _iconTexture;
        private VertexPositionColor[] _vertexPositionColors;
        private BasicEffect _basicEffect;


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

        private void  RenderBlock()
        {

            RenderTarget2D rt = new RenderTarget2D(GraphicsDevice, 200, 200);
            Rectangle rect = new Rectangle(0, 0, 200, 200);
            GraphicsDevice.SetRenderTarget(rt);
            GraphicsDevice.Clear(Color.Blue);

            //SpriteBatch.Begin(SpriteSortMode.Immediate,
            //                                BlendState.AlphaBlend,
            //                                Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp,
            //                                DepthStencilState.None,
            //                                RasterizerState.CullNone);
            //SpriteBatch.Draw(pixel, rect, Color.White);
            //SpriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            value -= 1;
            if (value < 0) value = 360;
            base.Update(gameTime);
        }

        private Double value = 360;





        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(this._iconTexture, new Rectangle(0, 0, 64, 64), Color.White);
            Primitives2D.DrawLine(_spriteBatch, new Vector2(100, 100), new Vector2(200, 300), Color.Red, 5.0f);
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

            this.DrawMarkRect(new Rectangle(200, 200, 100, 100), value, new Color(255,0,0,80));


   


            this.Fill(new Rectangle(100, 300, 100, 100), new Color(255, 0, 0, 80));
            base.Draw(gameTime);
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

