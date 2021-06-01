using lab02.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Renderer
{
    public class FlatShader : IShader
    {
        public Color BackgroundColor { get; init; } = Color.FromArgb(43, 43, 43);

        public Color[,] GetColors(Scene scene, Surfel[,] surfels)
        {
            var w = surfels.GetLength(1);
            var h = surfels.GetLength(0);
            var colors = new Color[h, w];

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    colors[y, x] = GetColor(scene, surfels[y, x]);
                }
            }

            return colors;
        }

        private Color GetColor(Scene scene, Surfel surfel)
        {
            if (surfel == null)
            {
                return BackgroundColor;
            }

            var objectColor = scene.SceneObject.Color;
            var baseColor = new Vector3(objectColor.R, objectColor.G, objectColor.B);
            float luminosity = 0;

            var direction = scene.Ligth.GetDirection(surfel.Point);
            var dotProduct = Vector3.Dot(direction, surfel.Normal);
            dotProduct = Math.Max(dotProduct, 0);

            var intensity = scene.Ligth.GetIntensity(surfel.Point);
            var lightLuminosity = dotProduct * intensity;
            luminosity += lightLuminosity;
            var result = baseColor * luminosity;
            return Color.FromArgb((byte)result.X, (byte)result.Y, (byte)result.Z);
        }
    }
}
