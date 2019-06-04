using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron.Data
{
    class PatternCollection
    {
        public List<Pattern> Patterns { get; set; }

        public int GetNeuronNumbers()
        {
            return Patterns.
                    First().
                    Values.
                    Count();
        }
    }
}
