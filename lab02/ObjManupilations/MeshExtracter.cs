using lab02.Mesh;
using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Data.VertexData;
using ObjLoader.Loader.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.ObjManupilations
{
    public class MeshExtracter : IMeshExtracter
    {
        public IMesh ExtractMeshFromLoadResult(LoadResult loadResult)
        {
            var triangles = new List<Triangle>();
            foreach (var face in loadResult.Groups[0].Faces)
            {
                var vertex1 = GetTriangleVertex(face[0], loadResult.Vertices);
                var vertex2 = GetTriangleVertex(face[1], loadResult.Vertices);
                var vertex3 = GetTriangleVertex(face[2], loadResult.Vertices);
                var normal1 = GetTriangleNormal(face[0], loadResult.Normals);
                var normal2 = GetTriangleNormal(face[1], loadResult.Normals);
                var normal3 = GetTriangleNormal(face[2], loadResult.Normals);
                var triangle = new Triangle(vertex1, vertex2, vertex3,
                    normal1, normal2, normal3);
                triangles.Add(triangle);
            }
            return new Mesh.Mesh(triangles);
        }

        private Vector3 GetTriangleVertex(FaceVertex face, IList<Vertex> vertices)
        {
            var vertex = vertices[face.VertexIndex - 1];
            return new Vector3(vertex.X, vertex.Y, vertex.Z);
        }

        private Vector3 GetTriangleNormal(FaceVertex face, IList<Normal> normals)
        {
            var normal = normals[face.NormalIndex - 1];
            return new Vector3(normal.X, normal.Y, normal.Z);
        }
    }
}
