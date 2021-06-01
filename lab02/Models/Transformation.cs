using System.Numerics;

namespace lab02.Models
{
    public class Transformation
    {
        public Matrix4x4 TransformationMatrix { get; private set; } = Matrix4x4.Identity;
        public Vector3 ObjectPosition { get => new(TransformationMatrix.M14, TransformationMatrix.M24, TransformationMatrix.M34); }
        public Vector3 ObjectScale 
        { get => 
                new(
                new Vector3(TransformationMatrix.M11, TransformationMatrix.M21, TransformationMatrix.M31).Length(),
                new Vector3(TransformationMatrix.M12, TransformationMatrix.M22, TransformationMatrix.M32).Length(),
                new Vector3(TransformationMatrix.M13, TransformationMatrix.M23, TransformationMatrix.M33).Length()
                    );
        }

        public Quaternion ObjectRotation { get => Quaternion.CreateFromRotationMatrix(TransformationMatrix); }

        public void Translate(Vector3 vector)
        {
            Matrix4x4 translationMatrix = Matrix4x4.Identity;
            translationMatrix.M14 = vector.X;
            translationMatrix.M24 = vector.Y;
            translationMatrix.M34 = vector.Z;
            TransformationMatrix *= translationMatrix;
        }

        public void Rotate(Quaternion q)
        {
            var temp1 = q.X * 2;
            var temp2 = q.Y * 2;
            var temp3 = q.Z * 2;
            var temp4 = q.X * temp1;
            var temp5 = q.Y * temp2;
            var temp6 = q.Z * temp3;
            var temp7 = q.X * temp2;
            var temp8 = q.X * temp3;
            var temp9 = q.Y * temp3;
            var temp10 = q.W * temp1;
            var temp11 = q.W * temp2;
            var temp12 = q.W * temp3;

            Matrix4x4 rotationMatrix = new();
            rotationMatrix.M11 = 1f - (temp5 + temp6);
            rotationMatrix.M21 = temp7 + temp12;
            rotationMatrix.M31 = temp8 - temp11;
            rotationMatrix.M12 = temp7 - temp12;
            rotationMatrix.M22 = 1f - (temp4 + temp6);
            rotationMatrix.M32 = temp9 + temp10;
            rotationMatrix.M13 = temp8 + temp11;
            rotationMatrix.M23 = temp9 - temp10;
            rotationMatrix.M33 = 1f - (temp4 + temp5);
            rotationMatrix.M44 = 1;
            TransformationMatrix *= rotationMatrix;
        }

        public void Scale(Vector3 vector)
        {
            Matrix4x4 scaleMatrix = Matrix4x4.Identity;
            scaleMatrix.M11 = vector.X;
            scaleMatrix.M22 = vector.Y;
            scaleMatrix.M33 = vector.Z;
            TransformationMatrix *= scaleMatrix;
        }

        public void LookAt(Vector3 from, Vector3 to)
        {
            TransformationMatrix = LookAt(from, to, Vector3.UnitY);
        }

        public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
        {
            var forward = Vector3.Normalize(from - to);
            var right = Vector3.Cross(up, forward);
            up = Vector3.Cross(forward, right);

            Matrix4x4 matrix;

            matrix.M11 = right.X;
            matrix.M21 = right.Y;
            matrix.M31 = right.Z;

            matrix.M12 = up.X;
            matrix.M22 = up.Y;
            matrix.M32 = up.Z;

            matrix.M13 = forward.X;
            matrix.M23 = forward.Y;
            matrix.M33 = forward.Z;

            matrix.M14 = from.X;
            matrix.M24 = from.Y;
            matrix.M34 = from.Z;

            matrix.M41 = 0;
            matrix.M42 = 0;
            matrix.M43 = 0;
            matrix.M44 = 1;

            return matrix;
        }
    }
}
