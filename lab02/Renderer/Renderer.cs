using lab02.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Renderer
{
    public class Renderer : IRenderer
    {
        private readonly IImageCreator _imageCreator;
        private readonly IRayscaler _rayscaler;
        private readonly IShader _shader;

        public Renderer(IImageCreator imageCreator, IRayscaler rayscaler, IShader shader)
        {
            _imageCreator = imageCreator;
            _rayscaler = rayscaler;
            _shader = shader;
        }

        public Bitmap RenderImage(Scene scene)
        {
            var surfels = _rayscaler.CastRays(scene);
            var colors = _shader.GetColors(scene, surfels);
            return _imageCreator.CreateBitmapFromColors(colors);
        }
    }
}
