
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WinGame.Disktop
{
    internal class GraphicUtils
    {
        private static Bitmap bitmap = new Bitmap(8192, 4096);
        private static Graphics graphics = Graphics.FromImage(bitmap);
        private static StringFormat textformat = new StringFormat(StringFormat.GenericTypographic);
        private static Byte[] empty = new Byte[8192 * 4];
        static GraphicUtils()
        {
            textformat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.Transparent);
            }
        }


        public static void Test()
        {

        }


        public static Microsoft.Xna.Framework.Graphics.Texture2D BuildString(Microsoft.Xna.Framework.Graphics.GraphicsDevice device, string text, Font font)
        {
            var width = 0;
            var height = 0;
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //graphics.Clear(Color.Black);
                var s = graphics.MeasureString(text, font, new PointF(), textformat);
                width = (Int32)Math.Ceiling(s.Width);
                height = (Int32)Math.Ceiling(s.Height);
                using (var brush = new SolidBrush(Color.FromArgb(255, 255, 255, 255)))
                {
                    graphics.DrawString(text, font, brush, 0, 0, textformat);
                }

            }
            var lck = bitmap.LockBits(new Rectangle(0, 0, width, height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var rowLen = width * 4;
            var data = new byte[rowLen * lck.Height];
            for (int i = 0; i < height; i++)
            {
                int srcIndex = i * lck.Stride;
                int croppedIndex = i * rowLen;
                System.Runtime.InteropServices.Marshal.Copy(lck.Scan0 + srcIndex, data, croppedIndex, rowLen);
                // clean content range
                System.Runtime.InteropServices.Marshal.Copy(empty, 0, lck.Scan0 + srcIndex, rowLen);
            }
            bitmap.UnlockBits(lck);
            var tex2 = new Microsoft.Xna.Framework.Graphics.Texture2D(device, width, height);
            for (int i = 0; i < data.Length; i+= 4)
            {
                var r = data[i];
                data[i] = data[i + 2];
                data[i + 2] = r;
            }
            tex2.SetData(data);
            return tex2;
        }



    }
}
