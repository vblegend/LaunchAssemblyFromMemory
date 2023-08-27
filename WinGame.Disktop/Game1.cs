using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WinGame.Disktop
{


    // http://vodacek.zvb.cz/archiv/604.html


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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(this._iconTexture, new Rectangle(0, 0, 64, 64), Color.White);
            _spriteBatch.End();




            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.RasterizerState = new RasterizerState
            //{
            //    CullMode = CullMode.None,
            //};

            //GraphicsDevice.BlendState = BlendState.Opaque;
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            _basicEffect.CurrentTechnique.Passes[0].Apply();
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexPositionColors, 0, 2);



            //EffectPassCollection effectPassCollection = _basicEffect.CurrentTechnique.Passes;
            //foreach (EffectPass pass in effectPassCollection)
            //{
            //    pass.Apply();
            //    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexPositionColors, 0, 2);
            //}



            base.Draw(gameTime);
        }
    }
}







//public void DrawMarkRect(System.Drawing.Rectangle rect, Double Angle, D3DColor dwColor)
//{
//    //D3DXVECTOR2
//    Single Cx = 0;
//    Single Cy = 0;
//    Single w2 = 0;
//    Single h2 = 0;
//    Double a2 = 0;
//    Int32 nTriangles = 0;
//    CustomVertex.TransformedColored vResult;
//    CustomVertex.TransformedColored[] vbData = new CustomVertex.TransformedColored[8];
//    for (int i = 0; i < 8; i++)
//    {
//        vbData[i] = new CustomVertex.TransformedColored();
//        vbData[i].Color = dwColor.ToArgb();
//        vbData[i].Rhw = 1;
//        vbData[i].Z = 0;
//    }
//    //  规定中心点
//    Cx = rect.Left + (float)rect.Width / 2;
//    Cy = rect.Top + (float)rect.Height / 2;
//    w2 = (float)rect.Width / 2;
//    h2 = (float)rect.Height / 2;
//    //// 默认的赋值
//    vbData[0].X = Cx;
//    vbData[0].Y = Cy;
//    vbData[1].X = Cx;
//    vbData[1].Y = rect.Top;
//    vbData[2].X = rect.Left;
//    vbData[2].Y = rect.Top;
//    vbData[3].X = rect.Left;
//    vbData[3].Y = (float)rect.Top + rect.Height;
//    vbData[4].X = (float)rect.Width + rect.Left;
//    vbData[4].Y = (float)rect.Top + rect.Height;
//    vbData[5].X = (float)rect.Width + rect.Left;
//    vbData[5].Y = rect.Top;
//    vbData[6].X = Cx;
//    vbData[6].Y = rect.Top;
//    //Rem  计算此时秒针的向量
//    Angle = Angle * Math.PI / 180;
//    a2 = Math.Atan(w2 / h2);
//    if (Angle < a2)
//    {
//        nTriangles = 1;
//        vResult.Y = -h2;
//        vResult.X = -h2 * (Single)Math.Tan(Angle);
//    }
//    else if (Angle < Math.PI - a2)
//    {
//        Angle = Angle - Math.PI / 2;
//        nTriangles = 2;
//        vResult.Y = w2 * (Single)Math.Tan(Angle);
//        vResult.X = -w2;
//    }
//    else if (Angle < Math.PI + a2)
//    {
//        nTriangles = 3;
//        vResult.Y = h2;
//        vResult.X = h2 * (Single)Math.Tan(Angle);
//    }
//    else if (Angle < 2 * Math.PI - a2)
//    {
//        Angle = Angle - Math.PI / 2;
//        nTriangles = 4;
//        vResult.Y = -w2 * (Single)Math.Tan(Angle);
//        vResult.X = w2;
//    }
//    else
//    {
//        nTriangles = 5;
//        vResult.Y = -h2;
//        vResult.X = -h2 * (Single)Math.Tan(Angle);
//    }
//    vbData[nTriangles + 1].X = Cx + vResult.X;
//    vbData[nTriangles + 1].Y = Cy + vResult.Y;
//    //bool bFlipHorizontal = true;
//    //if (bFlipHorizontal)
//    //{
//    //    Cx = Cx * 2;
//    //    for (int i = 0; i < nTriangles + 1; i++)
//    //    {
//    //        //vbData[i].X = Cx - vbData[i].X;
//    //    }
//    //}
//    using (var s = new RenderBlock(directX_context, RenderMode.Geometry))
//    {
//        directX_context.D3DDevice.RenderState.FillMode = FillMode.Solid;
//        directX_context.D3DDevice.RenderState.CullMode = Cull.None;
//        directX_context.D3DDevice.VertexFormat = CustomVertex.TransformedColored.Format;
//        directX_context.D3DDevice.SetTexture(0, null);
//        directX_context.D3DDevice.DrawUserPrimitives(PrimitiveType.TriangleFan, nTriangles, vbData);
//    }
//}


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

///// <summary>
///// 用颜色填充矩形
///// </summary>
///// <param name="rect">位置与大小</param>
///// <param name="nColor">颜色</param>
//public void Fill(System.Drawing.Rectangle rect, D3DColor nColor)
//{
//    CustomVertex.TransformedColoredTextured[] vbData = new CustomVertex.TransformedColoredTextured[4];
//    vbData[0] = VertexBuilder.CreateVertex(rect.Left, rect.Top, 0, 0, nColor);
//    vbData[1] = VertexBuilder.CreateVertex(rect.Left, rect.Bottom, 0, 1, nColor);
//    vbData[2] = VertexBuilder.CreateVertex(rect.Right, rect.Top, 1, 0, nColor);
//    vbData[3] = VertexBuilder.CreateVertex(rect.Right, rect.Bottom, 1, 1, nColor);
//    if (!directX_context.DeviceLosting)
//    {
//        using (var s = new RenderBlock(directX_context, RenderMode.Geometry))
//        {
//            directX_context.D3DDevice.RenderState.FillMode = FillMode.Solid;
//            directX_context.D3DDevice.RenderState.CullMode = Cull.None;
//            directX_context.D3DDevice.VertexFormat = CustomVertex.TransformedColoredTextured.Format;
//            directX_context.D3DDevice.SetTexture(0, null);
//            directX_context.D3DDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, vbData);
//        }
//    }
//}