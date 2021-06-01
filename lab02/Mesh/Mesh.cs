using lab02.Interfaces;
using lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Mesh
{
    public class Mesh : IMesh, ISplitable<Mesh>
    {
        private const int MinNumberOfTriangles = 8;
        public List<Triangle> Triangles { get; init; }

        public Box BoundingBox { get; }

        public Mesh(List<Triangle> triangles)
        {
            Triangles = triangles;

            var min = Vector3.One * float.MaxValue;
            var max = Vector3.One * float.MinValue;
            foreach (var box in triangles.Select(t => t.BoundingBox))
            {
                min.X = Math.Min(min.X, box.Min.X);
                min.Y = Math.Min(min.Y, box.Min.Y);
                min.Z = Math.Min(min.Z, box.Min.Z);
                max.X = Math.Max(max.X, box.Max.X);
                max.Y = Math.Max(max.Y, box.Max.Y);
                max.Z = Math.Max(max.Z, box.Max.Z);
            }
            BoundingBox = new Box(min, max);
        }

        public bool Intersect(Ray ray, out Surfel surfel)
        {
            surfel = null;
            var intersected = false;
            foreach (var t in Triangles)
            {
                if (t.Intersect(ray, out var s))
                {
                    intersected = true;
                    if (surfel == null || surfel.T > s.T)
                    {
                        surfel = s;
                    }
                }
            }
            return intersected;
        }

        public IMesh Transform(Matrix4x4 transformationMatrix)
        {
            var triangles = new List<Triangle>();
            foreach (var triangle in Triangles)
            {
                triangles.Add(triangle.Transform(transformationMatrix) as Triangle);
            }
            return new Mesh(triangles);
        }

        public bool Split(int depth, out Mesh left, out Mesh rigth, out Mesh middle)
        {
            if (Triangles.Count > MinNumberOfTriangles)
            {
                var splitValue = GetMedian(Triangles, depth);

                return SplitMesh(Triangles, depth, splitValue, out left, out rigth, out middle);
            }
            else
            {
                left = null;
                rigth = null;
                middle = null;
                return false;
            }
        }

        private float GetMedian(List<Triangle> triangles, int depth)
        {
            var sortedAxis = triangles
                .Select(t => GetDimension(t.BoundingBox.Center, depth))
                .OrderBy(v => v)
                .ToArray();
            var l = sortedAxis.Length;
            var i = (l - 1) / 2;
            return l % 2 == 0 ? (sortedAxis[i] + sortedAxis[i + 1]) * 0.5f : sortedAxis[i];
        }

        private bool SplitMesh(List<Triangle> triangles, int depth, float v,
            out Mesh left, out Mesh right, out Mesh middle)
        {
            var leftTriangles = triangles
                .Where(t => GetDimension(t.BoundingBox.Max, depth) <= v)
                .ToList();
            var rightTriangles = triangles
                .Where(t => GetDimension(t.BoundingBox.Min, depth) >= v)
                .ToList();
            var middleTriangles = triangles
                .Where(t => !leftTriangles.Contains(t) && !rightTriangles.Contains(t))
                .ToList();
            if (middleTriangles.Count == Triangles.Count)
            {
                left = null;
                right = null;
                middle = null;
                return false;
            }
            left = new Mesh(leftTriangles);
            right = new Mesh(rightTriangles);
            middle = new Mesh(middleTriangles);
            return true;
        }

        private float GetDimension(Vector3 v, int depth)
        {
            depth %= 3;
            return depth == 0 ? v.X : depth == 1 ? v.Y : v.Z;
        }
    }
}
