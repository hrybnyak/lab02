using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Models
{
    public class Scene
    {
        public Light Ligth { get; init; }
        public Camera Camera { get; init; }
        public SceneObject SceneObject { get; init; }

        public Scene(Light ligth, SceneObject sceneObject)
        {
            Ligth = ligth;
            SceneObject = sceneObject;
        }
    }
}
