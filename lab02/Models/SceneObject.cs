using lab02.Tree;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Models
{
    public class SceneObject
    {
        public KdTree<Mesh.Mesh> OptimizedMesh { get; init; }
        public Color Color { get; init; }
        public Transformation Transformation { get; init; }
        public SceneObject(Color color, Mesh.Mesh mesh, Transformation transformation = null)
        {
            Color = color;
            if (transformation != null)
            {
                mesh = mesh.Transform(transformation.TransformationMatrix) as Mesh.Mesh;
                Transformation = transformation;
            }
            else
            {
                Transformation = new Transformation();
                mesh = mesh.Transform(Transformation.TransformationMatrix) as Mesh.Mesh;
            }
            OptimizedMesh = new KdTree<Mesh.Mesh>(mesh);
        }
    }
}
