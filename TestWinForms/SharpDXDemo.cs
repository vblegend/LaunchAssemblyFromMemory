using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX;
using Color = SharpDX.Color;
using RectangleF = SharpDX.RectangleF;
using FontStyle = SharpDX.DirectWrite.FontStyle;
using SharpDX.Windows;
using System.Windows.Forms;

namespace TestWinForms
{
    public class CustomColorRenderer : SharpDX.DirectWrite.TextRendererBase
    {
        private RenderTarget renderTarget;
        private SolidColorBrush defaultBrush;

        public void AssignResources(RenderTarget renderTarget, SolidColorBrush defaultBrush)
        {
            this.renderTarget = renderTarget;
            this.defaultBrush = defaultBrush;
        }

        public override Result DrawGlyphRun(object clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun, GlyphRunDescription glyphRunDescription, ComObject clientDrawingEffect)
        {
            SolidColorBrush sb = defaultBrush;
            if (clientDrawingEffect != null && clientDrawingEffect is SolidColorBrush)
            {
                sb = (SolidColorBrush)clientDrawingEffect;
            }

            try
            {
                this.renderTarget.DrawGlyphRun(new Vector2(baselineOriginX, baselineOriginY), glyphRun, sb, measuringMode);
                return Result.Ok;
            }
            catch
            {
                return Result.Fail;
            }
        }
    }

    public class SharpDXDemo
    {
        private SharpDX.Direct2D1.Factory d2dFactory;
        private SharpDX.DirectWrite.Factory dwFactory;
        private RenderForm mainForm;

        private WindowRenderTarget renderTarget;

        private TextFormat textFormat;
        private TextLayout textLayout;


        private SolidColorBrush backgroundBrush;
        private SolidColorBrush defaultBrush;
        private SolidColorBrush greenBrush;
        private SolidColorBrush redBrush;

        private CustomColorRenderer textRenderer;


        private RectangleF fullTextBackground;


        private SharpDX.RectangleF textRegionRect;


        private string introText = @"Hello from SharpDX, this is a long text to show some more advanced features like paragraph alignment, custom drawing...";

        public void Setup()
        {
            mainForm = new RenderForm("Advanced Text rendering demo");

            d2dFactory = new SharpDX.Direct2D1.Factory();
            dwFactory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);

            textRenderer = new CustomColorRenderer();

            CreateResources();

            var bgcolor = new Color4(0.1f, 0.1f, 0.1f, 1.0f);

            //This is the offset where we start our text layout
            Vector2 offset = new Vector2(202.0f, 250.0f);

            textFormat = new TextFormat(dwFactory, "Arial", FontWeight.Regular, FontStyle.Normal, 16.0f);
            textLayout = new TextLayout(dwFactory, introText, textFormat, 300.0f, 200.0f);

            //Apply various modifications to text
            textLayout.SetUnderline(true, new TextRange(0, 5));
            textLayout.SetDrawingEffect(greenBrush, new TextRange(10, 20));
            textLayout.SetFontSize(24.0f, new TextRange(6, 4));
            textLayout.SetFontFamilyName("Comic Sans MS", new TextRange(11, 7));

            //Measure full layout
            var textSize = textLayout.Metrics;
            fullTextBackground = new RectangleF(textSize.Left + offset.X, textSize.Top + offset.Y, textSize.Width, textSize.Height);

            //Measure text to apply background to
            var metrics = textLayout.HitTestTextRange(53, 4, 0.0f, 0.0f)[0];
            textRegionRect = new RectangleF(metrics.Left + offset.X, metrics.Top + offset.Y, metrics.Width, metrics.Height);

            //Assign render target and brush to our custom renderer
            textRenderer.AssignResources(renderTarget, defaultBrush);



            mainForm.Show();
            using RenderLoop renderLoop = new RenderLoop(mainForm)
            {
                UseApplicationDoEvents = false
            };
            while (renderLoop.NextFrame())
            {
                renderTarget.BeginDraw();
                renderTarget.Clear(bgcolor);
                renderTarget.FillRectangle(fullTextBackground, backgroundBrush);
                renderTarget.FillRectangle(textRegionRect, redBrush);
                textLayout.Draw(textRenderer, offset.X, offset.Y);
                try
                {
                    renderTarget.EndDraw();
                }
                catch
                {
                    CreateResources();
                }
            }


            //RenderLoop.Run(mainForm, () =>
            //{

            //});

            d2dFactory.Dispose();
            dwFactory.Dispose();
            renderTarget.Dispose();
        }

        private void CreateResources()
        {
            if (renderTarget != null) { renderTarget.Dispose(); }
            if (defaultBrush != null) { defaultBrush.Dispose(); }
            if (greenBrush != null) { greenBrush.Dispose(); }
            if (redBrush != null) { redBrush.Dispose(); }
            if (backgroundBrush != null) { backgroundBrush.Dispose(); }


            HwndRenderTargetProperties wtp = new HwndRenderTargetProperties();
            wtp.Hwnd = mainForm.Handle;
            wtp.PixelSize = new Size2(mainForm.ClientSize.Width, mainForm.ClientSize.Height);
            wtp.PresentOptions = PresentOptions.Immediately;
            renderTarget = new WindowRenderTarget(d2dFactory, new RenderTargetProperties(), wtp);

            defaultBrush = new SolidColorBrush(renderTarget, Color.White);
            greenBrush = new SolidColorBrush(renderTarget, Color.Green);
            redBrush = new SolidColorBrush(renderTarget, Color.Red);
            backgroundBrush = new SolidColorBrush(renderTarget, new Color4(0.3f, 0.3f, 0.3f, 0.5f));

            textRenderer.AssignResources(renderTarget, defaultBrush);

        }
    }
}
