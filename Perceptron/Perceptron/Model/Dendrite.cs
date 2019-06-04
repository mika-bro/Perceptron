using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Model
{
    class Dendrite
    {
        public int ID { get; set; }
        public double Weight { get; set; }
        public int TargetNeuron { get; set; }

        public Dendrite(int id)
        {
            ID = id;
            Weight = Parameters.Random.NextDouble();
            TargetNeuron = id;
        }

        public void ChangeWight(double value, Layer layer)
        {
            var error = layer
                .Neurons
                .Single(x => x.ID == TargetNeuron)
                .ErrorSignal;
            var result = Weight + (Parameters.Step * value * error);
            Weight = result;
        }
    }
}
