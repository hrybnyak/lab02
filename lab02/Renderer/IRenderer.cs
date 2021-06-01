using lab02.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Renderer
{
    public interface IRenderer
    {
        Bitmap RenderImage(Scene scene);
    }
}
