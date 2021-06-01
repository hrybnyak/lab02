using lab02.Extensions;
using lab02.Models;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Mesh
{
    public class Box : IMesh
    {
        public Vector3 Min { get; init; }
        public Vector3 Max { get; init; }
        public Vector3 Center { get; init; }
        public Vector3 Extents { get; init; }
        public Box BoundingBox { get; init; }

        public Box(Vector3 min, Vector3 max)
        {
            Max = max;
            Min = min;
            Center = new Vector3((Min.X + Max.X) / 2, (Min.Y + Max.Y) / 2, (Min.Z + Max.Z) / 2);
            Extents = max - Center;
            BoundingBox = this;
        }

        public bool Intersect(Ray ray, out Surfel surfel)
        {
            var tmin = ((ray.InverseDirection.X < 0 ? Max.X : Min.X) - ray.Origin.X) * ray.InverseDirection.X;
            var tmax = ((ray.InverseDirection.X < 0 ? Min.X : Max.X) - ray.Origin.X) * ray.InverseDirection.X;

            var tymin = ((ray.InverseDirection.Y < 0 ? Max.Y : Min.Y) - ray.Origin.Y) * ray.InverseDirection.Y;
            var tymax = ((ray.InverseDirection.Y < 0 ? Min.Y : Max.Y) - ray.Origin.Y) * ray.InverseDirection.Y;

            if ((tmin > tymax) || (tymin > tmax))
            {
                surfel = null;
                return false;
            }
            tmin = tymin > tmin ? tymin : tmin;
            tmax = tymax < tmax ? tymax : tmax;

            var tzmin = ((ray.InverseDirection.Y < 0 ? Max.Y : Min.Y) - ray.Origin.Y) * ray.InverseDirection.Y;
            var tzmax = ((ray.InverseDirection.Y < 0 ? Min.Y : Max.Y) - ray.Origin.Y) * ray.InverseDirection.Y;

            if ((tmin > tzmax) || (tzmin > tmax))
            {
                surfel = null;
                return false;
            }
            tmin = tzmin > tmin ? tzmin : tmin;
            tmax = tzmax < tmax ? tzmax : tmax;
            
            if (tmin < 0 && tmax < 0)
            {
                surfel = null;
                return false;
            }

            var t = tmin < 0 ? tmax : Math.Min(tmin, tmax);
            var point = ray.GetIntersactionPoint(t);

            surfel = new Surfel
            {
                T = t,
                Point = point,
                Normal = CalculateNormal(point)
            };

            return true;
        }

        public IMesh Transform(Matrix4x4 transformationMatrix)
        {
            var extents = transformationMatrix.MultiplyVector(Extents);
            var center = transformationMatrix.MultiplyPoint(Center);
            var min = center - extents;
            var max = center + extents;
            return new Box(min, max);
        }

        private Vector3 CalculateNormal(Vector3 point)
        {
            var pointInBox = point - Center;

            var min = Math.Abs(Extents.X - Math.Abs(pointInBox.X));
            var normal = Vector3.UnitX;

            normal *= Math.Sign(pointInBox.X);

            var distance = Math.Abs(Extents.Y - Math.Abs(pointInBox.Y));
            if (distance < min)
            {
                min = distance;
                normal = Vector3.UnitY;
                normal *= Math.Sign(pointInBox.Y);
            }

            distance = Math.Abs(Extents.Z - Math.Abs(pointInBox.Z));

            if (distance < min)
            {
                normal = Vector3.UnitZ;
                normal *= Math.Sign(pointInBox.Z);
            }
            
            return normal;
        }
    }
}
