using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Data
{
    class Pattern
    {
        public List<int> Values { get; set; }
        public List<int> Expected { get; set; }

        public void ShowValues()
        {
            string values = "Pattern: \t";
            foreach(var item in Values)
            {
                values = values + item + "\t";
            }
            Console.WriteLine(values);
        }
    }
}
