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