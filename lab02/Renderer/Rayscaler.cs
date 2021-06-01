using lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab02.Renderer
{
    public class Rayscaler : IRayscaler
    {
        public Surfel[,] CastRays(Scene scene)
        {
            var w = scene.Camera.Width;
            var h = scene.Camera.Height;
            var resultSurfels = new Surfel[h, w];
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var cameraRay = scene.Camera.ScreenPointToRay(new Vector2(x, y));
                    resultSurfels[y, x] = CastRay(scene, cameraRay);
                }
            }
            return resultSurfels;
        }

        public Surfel CastRay(Scene scene, Ray ray)
        {
            Surfel surfel = null;
            if (scene.SceneObject.OptimizedMesh.Perform(m => m.Intersect(ray, out surfel)))
            {
                return surfel;
            }
            return null;
        }
    }
}
