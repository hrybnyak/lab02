using lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Mesh
{
    public interface IMesh
    {
        Box BoundingBox { get; }
        IMesh Transform(Matrix4x4 transformationMatrix);
        bool Intersect(Ray ray, out Surfel surfel);
    }
}
