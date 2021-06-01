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
        public SceneObject(Color color, Mesh.Mesh mesh)
        {
            Color = color;
            OptimizedMesh = new KdTree<Mesh.Mesh>(mesh);
        }
    }
}
