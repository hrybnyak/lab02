using lab02.Models;
using lab02.ObjManupilations;
using lab02.Renderer;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace lab02
{
    class Program
    {
        static void Main(string[] args)
        {
            var objectLoader = new ObjectLoader();
            var meshExtracter = new MeshExtracter();
            var imageCreator = new ImageCreator();
            var rayscaller = new Rayscaler();
            var shader = new FlatShader();
            var renderer = new Renderer.Renderer(imageCreator, rayscaller, shader);
            var loadresult = objectLoader.Load(@"D:\School\lab02\simplecow.obj");
            //Transformation t = new Transformation();
            //t.Translate(new System.Numerics.Vector3(0, 0, 2));
            var mesh = meshExtracter.ExtractMeshFromLoadResult(loadresult) as Mesh.Mesh;
            var sceneObject = new SceneObject(Color.FromArgb(130, 15, 220), mesh);
            var scene = new Scene(sceneObject);
            var rendered = renderer.RenderImage(scene);
            using (var fs = new FileStream("result.png", FileMode.Create))
            {
                rendered.Save(fs, ImageFormat.Png);
            }
        }
    }
}
