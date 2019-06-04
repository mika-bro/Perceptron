using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Model
{
    public enum LayerName { input, hidden, output }
    class Layer
    {
        public List<Neuron> Neurons { get; set; }
        public LayerName Type { get; set; }

        public Layer(LayerName type)
        {
            List<Neuron> neurons = new List<Neuron>();
            if(type == LayerName.input)
            {
                Type = type;
                for (int i = 0; i < Parameters.InputNeurons; i++)
                {
                    neurons.Add(new Neuron(Parameters.HiddenNeurons, i + 1));
                }

            }
            if(type == LayerName.hidden)
            {
                Type = type;
                for (int i = 0; i < Parameters.HiddenNeurons; i++)
                {
                    neurons.Add(new Neuron(Parameters.OutputNeurons, i + 1));
                }

            }
            if(type == LayerName.output)
            {
                Type = type;
                for (int i = 0; i < Parameters.OutputNeurons; i++)
                {
                    neurons.Add(new Neuron(0, i + 1));
                }

            }
            Neurons = neurons;            
        }

        public void AddBias()
        {
            Neuron lastNeuron = Neurons.Last();
            Neuron newNeuron = new Neuron(
                lastNeuron.Dendrites.Count(),
                lastNeuron.ID + 1);
            newNeuron.Value = 1;
            newNeuron.Bias = true;
            Neurons.Add(newNeuron);
        }

        public void PrintNeuronValues()
        {
            string values = "";
            if(Type == LayerName.hidden)
            {
                values = "Hidden Layer: \t";
            }
            if(Type == LayerName.output)
            {
                values = "Output Layer: \t";
            }
            foreach(var neuron in Neurons)
            {
                double value = Math.Round(neuron.Value, 3);
                values = values + value.ToString() + "\t";              
            }
            Console.WriteLine(values);
        }
    }
}
