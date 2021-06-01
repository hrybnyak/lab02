using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Renderer
{
    public class ImageCreator : IImageCreator
    {
        public Bitmap CreateBitmapFromColors(Color[,] colors)
        {
            var w = colors.GetLength(1);
            var h = colors.GetLength(0);
            var bitmap = new Bitmap(w, h);

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    bitmap.SetPixel(x, y, colors[y, x]);
                }
            }
            return bitmap;
        }
    }
}
