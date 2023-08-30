
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinGame.Disktop
{
    internal class GraphicUtils
    {
        static Bitmap bitmap = new Bitmap(1920, 1080);
        static StringFormat textformat = new StringFormat(StringFormat.GenericTypographic);

        static GraphicUtils()
        {
            textformat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        }


        public static void Test()
        {

        }


        public static Microsoft.Xna.Framework.Graphics.Texture2D BuildString(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, string text, int x, int y, Font font)
        {
            var width = 0;
            var height = 0;
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.Clear(Color.Transparent);
                var s = graphics.MeasureString(text, font, new PointF(), textformat);
                width = (Int32)Math.Ceiling(s.Width);
                height = (Int32)Math.Ceiling(s.Height);
                graphics.DrawString(text, font, Brushes.White, 0, 0, textformat);
            }
            var lck = bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var rowLen = width * 4;
            var data = new byte[rowLen * lck.Height];
            for (int i = 0; i < height; i++)
            {
                int srcIndex = i * lck.Stride;
                int croppedIndex = i * rowLen;
                System.Runtime.InteropServices.Marshal.Copy(lck.Scan0 + srcIndex, data, croppedIndex, rowLen);
            }
            bitmap.UnlockBits(lck);
            var tex2 = new Microsoft.Xna.Framework.Graphics.Texture2D(device, width, height);
            tex2.SetData(data);
            return tex2;
        }



    }
}
