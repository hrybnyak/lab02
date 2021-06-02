using lab02.Models;
using lab02.ObjManupilations;
using lab02.Renderer;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace lab02.CommandExecutor
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly IArgumentsListParser _argumentListParser;
        private readonly IObjectLoader _objectLoader;
        private readonly IMeshExtracter _meshExtracter;
        private readonly IRenderer _renderer;
        private readonly IImageCreator _imageCreator;
            
        public CommandExecutor(IArgumentsListParser argumentListParser,
            IObjectLoader objectLoader,
            IMeshExtracter meshExtracter,
            IRenderer renderer,
            IImageCreator imageCreator)
        {
            _argumentListParser = argumentListParser;
            _objectLoader = objectLoader;
            _meshExtracter = meshExtracter;
            _renderer = renderer;
            _imageCreator = imageCreator;
        }

        public void ExecuteCommand(string[] args)
        {
            _argumentListParser.ExtractArguments(args, out string source, out string output);
            var loadResult = _objectLoader.Load(source);
            var mesh = _meshExtracter.ExtractMeshFromLoadResult(loadResult) as Mesh.Mesh;
            var sceneObject = new SceneObject(Color.FromArgb(130, 15, 220), mesh);
            var scene = new Scene(sceneObject);
            var rendered = _renderer.RenderImage(scene);
            using (var fs = new FileStream(output, FileMode.Create))
            {
                rendered.Save(fs, ImageFormat.Png);
            }

        }
    }
}
