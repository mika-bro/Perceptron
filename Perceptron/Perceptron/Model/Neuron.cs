using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Model
{
    class Neuron
    {
        public int ID { get; set; }
        public List<Dendrite> Dendrites { get; set; }
        public double Value { get; set; }
        public double ErrorSignal { get; set; }
        public bool Bias { get; set; }

        public Neuron(int dendritesNumber, int id)
        {
            ID = id;
            List<Dendrite> dendrites = new List<Dendrite>();
            for (int i = 0; i < dendritesNumber; i++)
            {
                dendrites.Add(new Dendrite(i + 1));
            }
            Dendrites = dendrites;
        }

        public void CountValue(Layer layer)
        {
            if(Bias == false)
            {
                List<double> all_multipications = new List<double>();
                foreach (var n in layer.Neurons)
                {
                    double m = n.Value * n.Dendrites.SingleOrDefault(x => x.TargetNeuron == ID).Weight;
                    all_multipications.Add(m);
                }
                var result = all_multipications.Sum();
                Value = SigmoidFunction(result);
            }
           
        }

        private double SigmoidFunction(double v)
        {
            double result = 1 / (1 + Math.Pow(2.7183, -v));
            return result;
        }

        public void CountError(int expected)
        {
            var result = Value * (1 - Value) * (expected - Value);
            ErrorSignal = result;
        }

        public void CountError(Layer layer)
        {
            List<double> all_multiplications = new List<double>();
            foreach (var neuron in layer.Neurons)
            {
                if (neuron.Bias) continue;
                double weight = Dendrites
                    .SingleOrDefault(x => x.TargetNeuron == neuron.ID)
                    .Weight;

                all_multiplications.Add(weight * neuron.ErrorSignal);
            }

            double result = Value * (1 - Value) * all_multiplications.Sum();
            ErrorSignal = result;
        }

        public void ChangeWeights(Layer layer)
        {
            foreach (var dendrite in Dendrites)
            {
                dendrite.ChangeWight(Value, layer);
            }
        }
    }
}
