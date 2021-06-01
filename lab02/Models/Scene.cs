using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Models
{
    public class Scene
    {
        public Ligth Ligth { get; init; }
        public Camera Camera { get; init; }

        public SceneObject SceneObject { get; init; }

        public Scene(Camera camera, Ligth ligth, SceneObject sceneObject)
        {
            Ligth = ligth;
            SceneObject = sceneObject;
        }

        /// <summary>
        /// Default set up
        /// </summary>
        /// <param name="sceneObject"></param>
        public Scene(SceneObject sceneObject)
        {
            SceneObject = sceneObject;
            Ligth = new Ligth(Color.FromArgb(135, 15, 220), 1f, 80);
            Ligth.Transformation.Translate(new Vector3(10, 20, 20));
            Camera = new Camera(1920/4, 1080/4, 60);
            Camera.Transformation.LookAt(new Vector3(1f, 0, 1f), sceneObject.OptimizedMesh.Root.Value.BoundingBox.Center);
        }
    }
}
