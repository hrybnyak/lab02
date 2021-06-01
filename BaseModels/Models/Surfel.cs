using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.Models
{
    public class Surfel
    {
        public double T { get; set; }
        public Color Color { get; set; }
        public Vector<double> Point { get; set; }
        public Vector<double> Normal { get; set; }
    }
}
