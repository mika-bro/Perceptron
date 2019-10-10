using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perceptron.Data;

namespace Perceptron.Model
{
    class Network
    {
        public Layer InputLayer { get; set; }
        public Layer HiddenLayer { get; set; }
        public Layer OutputLayer { get; set; }

        public Network()
        {
            InputLayer = new Layer(LayerName.input);
            HiddenLayer = new Layer(LayerName.hidden);
            OutputLayer = new Layer(LayerName.output);
        }

        public void Train(PatternCollection patterns)
        {
            for (int i = 0; i < Parameters.Epochs; i++)
            {
                foreach (var pattern in patterns.Patterns)
                {
                    CountOutputValues(pattern.Values);
                    ChangeAllWeights(pattern.Expected);
                }
            }
        }

        public void Test(PatternCollection p)
        {
            foreach(var item in p.Patterns)
            {
                CountOutputValues(item.Values);
                item.ShowValues();
                HiddenLayer.PrintNeuronValues();
                OutputLayer.PrintNeuronValues();
                Console.WriteLine("");
            }
        }

        public Dictionary<string, int> GetNeuronsNb()
        {
            int inputLayer = InputLayer.Neurons.Count();
            int hiddenLayer = HiddenLayer.Neurons.Count();
            int outputLayer = OutputLayer.Neurons.Count();
            return new Dictionary<string, int>
            {
                { "InputLayer", inputLayer},
                { "HiddenLayer", hiddenLayer},
                {"OutputLayer", outputLayer }
            };
        }

        private void CountOutputValues(List<int> single_pattern)
        {
            int n = 0;
            foreach (var value in single_pattern)
            {
                InputLayer.Neurons[n].Value = value;
                n = n + 1;
            }

            foreach(var neuron in HiddenLayer.Neurons)
            {
                neuron.CountValue(InputLayer);
            }

            foreach(var neuron in OutputLayer.Neurons)
            {
                neuron.CountValue(HiddenLayer);
            }
        }

        private void ChangeAllWeights(List<int> expected)
        {
            int i = 0;
            foreach(var neuron in OutputLayer.Neurons)
            {
                neuron.CountError(expected[i]);
                i = i + 1;
            }

            foreach(var neuron in HiddenLayer.Neurons)
            {
                neuron.CountError(OutputLayer);
                neuron.ChangeWeights(OutputLayer);
            }

            foreach(var neuron in InputLayer.Neurons)
            {
                neuron.CountError(HiddenLayer);
                neuron.ChangeWeights(HiddenLayer);
            }
        }

        public void AddBias()
        {
            InputLayer.AddBias();
            HiddenLayer.AddBias();
        }
    }
}
