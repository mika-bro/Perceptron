using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perceptron.Data;
using Perceptron.Model;

namespace Perceptron
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataPath = Reader.DefineFilePath("data.txt");
            string configPath = Reader.DefineFilePath("config.txt");
            PatternCollection patterns = Reader.ReadData(dataPath);
            var configs = Reader.ReadConfigFile(configPath);

            Parameters.InputNeurons = patterns.GetNeuronNumbers();
            Parameters.HiddenNeurons = (int)configs["HiddenLayerNeurons"];
            Parameters.OutputNeurons = patterns.GetNeuronNumbers();
            Parameters.Random = new Random();
            Parameters.Epochs = (int)configs["Epochs"];
            Parameters.Step = configs["Step"];

            Console.WriteLine("WITHOUT BIAS");
            Network network = new Network();
            network.Train(patterns);
            network.Test(patterns);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("WITH BIAS");

            network.AddBias();
            network.Train(patterns);
            network.Test(patterns);

            Console.ReadKey();

        }
    }
}
