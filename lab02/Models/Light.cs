using System;
using System.Drawing;
using System.Numerics;

namespace lab02.Models
{
    public class Light
    {
        public Color Color { get; init; }
        public float Intensity { get; init; }
        public  float Radius { get; init; }
        public Transformation Transformation { get; init; } = new();

        public Light(Color color, float intensity, float radius)
        {
            Color = color;
            Intensity = intensity;
            Radius = radius;
        }

        public Vector3 GetDirection(Vector3 point)
        {
            return Vector3.Normalize((point - Transformation.Position));
        }

        public float GetIntensity(Vector3 point)
        {
            var distance = (point - Transformation.Position).Length();
            return Math.Max(1 - distance / Radius, 0) * Intensity;
        }
    }
}
