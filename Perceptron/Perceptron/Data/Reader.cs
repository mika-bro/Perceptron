using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Perceptron.Data
{
    class Reader
    {
        public static PatternCollection ReadData(string path)
        {
            List<Pattern> patterns = new List<Pattern>();
            List<string> file = File.ReadLines(path).ToList();
            foreach (var line in file)
            {
                string[] firstSplit = line.Split('|');
                string[] splitedLine = firstSplit[0].Split(';');
                string[] splitedLine_expected = firstSplit[1].Split(';');
                patterns.Add(
                    new Pattern
                    {
                        Values = ConvertToInt(splitedLine).ToList(),
                        Expected = ConvertToInt(splitedLine_expected).ToList()
                    });
            }

            return new PatternCollection { Patterns = patterns };
        }

        public static Dictionary<string, double> ReadConfigFile(string path)
        {
            Dictionary<string, double> config = new Dictionary<string, double>();
            var file = File.ReadLines(path);
            foreach(var item in file)
            {
                string[] splitedLine = item.Split(':');
                config.Add(
                    splitedLine[0],
                    Double.Parse(splitedLine[1]));

            }
            return config;
        }

        static public string DefineFilePath(string fileName)
        {
            var path1 = Directory.GetCurrentDirectory();
            var path2 = Directory.GetParent(path1).ToString();
            var path3 = Directory.GetParent(path2).ToString();
            var root = Directory.GetParent(path3).ToString();
            return root + @"\files\" + fileName;
        }

        private static IEnumerable<int> ConvertToInt(string[] line)
        {
            foreach (var item in line)
            {
                yield return Int32.Parse(item);
            }
            yield break;
        }
    }
}
