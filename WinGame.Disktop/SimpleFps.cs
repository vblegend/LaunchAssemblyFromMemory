using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteFontPlus;

namespace WinGame.Disktop
{
    public class SimpleFps
    {
        private double frames = 0;
        private double updates = 0;
        private double elapsed = 0;
        private double last = 0;
        private double now = 0;
        public double msgFrequency = 1.0f;
        public string msg = "";

        /// <summary>
        /// The msgFrequency here is the reporting time to update the message.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            now = gameTime.TotalGameTime.TotalSeconds;

            elapsed = (double)(now - last);
            if (elapsed > msgFrequency)
            {

                msg = " Fps: " + (frames / elapsed).ToString() + "\n Elapsed time: " + elapsed.ToString() + "\n 更新次数: " + updates.ToString() + "\n 渲染帧数: " + frames.ToString();
                //Console.WriteLine(msg);
                elapsed = 0;
                frames = 0;
                updates = 0;
                last = now;
            }
            updates++;
        }

        public void DrawFps(SpriteBatch spriteBatch, DynamicSpriteFont font, Vector2 fpsDisplayPosition, Color fpsTextColor)
        {
            //spriteBatch.DrawString(font, msg, fpsDisplayPosition, fpsTextColor);

            var lines = msg.Split('\n');

            foreach (var line in lines)
            {
                spriteBatch.DrawString(font, line, fpsDisplayPosition, fpsTextColor);
                fpsDisplayPosition.Y += font.Size;
            }




            frames++;
        }
    }

}
