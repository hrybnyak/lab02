using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.Mesh
{
    public interface IMesh
    {
        public abstract Box BoundingBox { get; }

        public abstract bool Intersect(Ray ray, out Surfel surfel);

        public abstract void Apply(Matrix4x4 matrix);
    }
}
