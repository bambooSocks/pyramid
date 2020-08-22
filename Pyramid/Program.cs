using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pyramid
{
    public class Program
    {
        // Returns the number of nodes in a pyramid given the number of layers of the pyramid
        // Throws exception if layers are less that 1
        public static int GetNumberOfNodes(int numOfLayers)
        {
            if (numOfLayers == 1) 
            {
                return 1;
            } 
            else if (numOfLayers < 1)
            {
                throw new Exception("Number of layers too low");
            } 
            else 
            {
                return numOfLayers + GetNumberOfNodes(numOfLayers - 1);
            }
        }

        public static int GetLayerFromNode(int node)
        {
            if (node < 0)
                throw new Exception("Invalid node");

            int testLayer = 1;
            
            while (node >= GetNumberOfNodes(testLayer))
            {
                testLayer++;
            }

            return testLayer;
        }

        static List<List<int>> GenerateAdjacencyList(int numOfLayers)
        {
            int numberOfNodes = GetNumberOfNodes(numOfLayers);
            var output = new List<List<int>>();

            for (int node = 0; node < numberOfNodes; node++)
            {
                var children = new List<int>();
                int layer = GetLayerFromNode(node);
                if (layer < numOfLayers)
                {
                    children.Add(node + layer);     // formula for first child
                    children.Add(node + layer + 1); // formula for second child
                    output.Add(children);
                }
                else
                {
                    output.Add(null);
                }
            }

            return output;
        }

        static List<List<int>> FindAllValidPaths(int node, List<List<int>> adjList, List<int> valueList)
        {
            if (node < 0 || node >= adjList.Count())
            {
                // TODO: crash or smth
            }
            
            var output = new List<List<int>>();

            if (adjList[node] == null)
            {
                var singleList = new List<int>();
                singleList.Add(valueList[node]);
                output.Add(singleList);
                return output;
            }

            foreach (var childIdx in adjList[node])
            {
                // checking for opposite parity of parent and child 
                if (Math.Abs(valueList[childIdx] - valueList[node]) % 2 == 1)
                {
                    var subPaths = FindAllValidPaths(childIdx, adjList, valueList);

                    foreach (var path in subPaths)
                    {
                        var partialPath = new List<int>();
                        partialPath.Add(valueList[node]);
                        partialPath.AddRange(path);
                        output.Add(partialPath);
                    }
                }
            }

            return output;
        }

        static void Main(string[] args)
        {
            string fileContent = File.ReadAllText("../../../input.txt");
            var lines = new List<string>(fileContent.Split('\n'));
            int layers = lines.Count();

            var adjList = GenerateAdjacencyList(layers);
            var valueList = lines.SelectMany(x => x.Split())
                                 .Select(x => int.Parse(x))
                                 .ToList();

            var paths = FindAllValidPaths(0, adjList, valueList);
            
            foreach (var inList in paths)
            {
                if (inList != null)
                {
                    foreach (var num in inList)
                    {
                        Console.Write($"{num} ");
                    }
                    Console.WriteLine($" = {inList.Sum()}");
                }
            }
        }
        
    }
}