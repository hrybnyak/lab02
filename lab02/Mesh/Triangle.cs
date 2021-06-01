using lab02.Extensions;
using lab02.Models;
using System;
using System.Numerics;

namespace lab02.Mesh
{
    public class Triangle : IMesh
    {
        public Vector3 Vertex1 { get; init; }
        public Vector3 Vertex2 { get; init; }
        public Vector3 Vertex3 { get; init; }

        public Vector3 Normal1 { get; init; }
        public Vector3 Normal2 { get; init; }
        public Vector3 Normal3 { get; init; }


        public Box BoundingBox { get; init; }

        public Triangle(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, 
            Vector3 normal1, Vector3 normal2, Vector3 normal3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
            Normal1 = normal1;
            Normal2 = normal2;
            Normal3 = normal3;

            Vector3 min, max = new();

            max.X = Math.Max(vertex1.X, Math.Max(vertex2.X, vertex3.X));
            max.Y = Math.Max(vertex1.Y, Math.Max(vertex2.Y, vertex3.Y));
            max.Z = Math.Max(vertex1.Z, Math.Max(vertex2.Z, vertex3.Z));
            min.X = Math.Min(vertex1.X, Math.Min(vertex2.X, vertex3.X));
            min.Y = Math.Min(vertex1.Y, Math.Min(vertex2.Y, vertex3.Y));
            min.Z = Math.Min(vertex1.Z, Math.Min(vertex2.Z, vertex3.Z));

            BoundingBox = new Box(min, max);
        }

        public bool Intersect(Ray ray, out Surfel surfel)
        {
            var epsilon = 0.0000001;
            var edge1 = Vertex2 - Vertex1;
            var edge2 = Vertex3 - Vertex1;
            var h = Vector3.Cross(ray.Direction, edge2);
            var a = Vector3.Dot(edge1, h);
            if (a > -epsilon && a < epsilon)
            {
                surfel = null;
                return false;
            }
            var f = 1.0 / a;
            var s = ray.Origin - Vertex1;
            var u = f * Vector3.Dot(s,h);
            if (u < 0.0 || u > 1.0)
            {
                surfel = null;
                return false;
            }
            var q = Vector3.Cross(s,edge1);
            var v = f * Vector3.Dot(ray.Direction, q);
            if (v < 0.0 || u + v > 1.0)
            {
                surfel = null;
                return false;
            }
            float t = (float)f * Vector3.Dot(edge2, q);
            if (t > epsilon)
            {
                surfel = new Surfel
                {
                    T = t,
                    Point = ray.GetIntersactionPoint(t),
                    Normal = Vector3.Normalize(Normal1)
                };
                return true;
            }
            else // This means that there is a line intersection but not a ray intersection.
            {
                surfel = null;
                return false; 
            }
        }

        public IMesh Transform(Matrix4x4 transformationMatrix)
        {
            var vertex1 = transformationMatrix.MultiplyByPoint(Vertex1);
            var vertex2 = transformationMatrix.MultiplyByPoint(Vertex2);
            var vertex3 = transformationMatrix.MultiplyByPoint(Vertex3);
            if (Matrix4x4.Invert(transformationMatrix, out var inverted))
            {
                var normal1 = inverted.MultiplyByVector(Normal1);
                var normal2 = inverted.MultiplyByVector(Normal2);
                var normal3 = inverted.MultiplyByVector(Normal3);
                return new Triangle(vertex1, vertex2, vertex3,
                    normal1, normal2, normal3);
            }
            return new Triangle(vertex1, vertex2, vertex3,
                   Normal1, Normal2, Normal3);
        }
    }
}
