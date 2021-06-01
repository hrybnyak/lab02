using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Models
{
    public record Ray
    {
        public Vector3 Origin { get; init; }
        public Vector3 Direction { get; init; }
        public Vector3 InverseDirection { get; init; }

        public Ray(Vector3 start, Vector3 direction)
        {
            Origin = start;
            Direction = direction == Vector3.Zero ? throw new ArgumentException("Direction of a ray can't be a zero vector") :
                Vector3.Normalize(direction);
            InverseDirection = new Vector3(1/direction.X, 1/direction.Y, 1/direction.Z);
        }

        public Vector3 GetIntersactionPoint(float t) => Origin + t * Direction;
    }
}
