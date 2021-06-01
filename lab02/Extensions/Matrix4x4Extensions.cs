using System.Numerics;

namespace lab02.Extensions
{
    public static class Matrix4x4Extensions
    {
        public static Vector3 MultiplyVector(this Matrix4x4 matrix, Vector3 vector)
        {
            Vector3 result = new();
            result.X = matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z;
            result.Y = matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z;
            result.Z = matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z;
            return result;
        }

        public static Vector3 MultiplyPoint(this Matrix4x4 matrix, Vector3 point)
        {
            var vector = matrix.MultiplyVector(point);
            vector.X += matrix.M14;
            vector.Y += matrix.M24;
            vector.Z += matrix.M34;
            
            var n = 1 / (matrix.M41 * point.X + matrix.M42 * point.Y + matrix.M43 * point.Z + matrix.M44);

            vector.X *= n;
            vector.Y *= n;
            vector.Z *= n;

            return vector;
        }
    }
}
