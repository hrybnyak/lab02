using lab02.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Models
{
    public class Camera
    {
        public float HorizontalFieldOfView { get; init; }
        private float Tan { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public float AspectRatio { get; init; }
        public Transformation Transformation { get; init; } = new();

        public Camera(int width, int heigth, float horizontalFieldOfView)
        {
            HorizontalFieldOfView = horizontalFieldOfView;
            Tan = (float)Math.Tan((Math.PI / 180) * horizontalFieldOfView/ 2);
            AspectRatio = heigth / width;
        }

        public Ray ScreenPointToRay(Vector2 screenPoint)
        {
            var x = (2 * (screenPoint.X + 0.5f) / Width - 1) * Tan;
            var y = (1 - 2 * (screenPoint.Y + 0.5f) / Height) / AspectRatio * Tan;
            var dir = new Vector3(x, y, -1);
            dir = Vector3.Normalize(Transformation.LocalToWorldMatrix.MultiplyByVector(dir));
            return new Ray(Transformation.Position, dir);
        }
    }
}
