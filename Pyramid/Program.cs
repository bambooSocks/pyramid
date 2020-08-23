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
            if (numOfLayers < 1)
                throw new Exception("Number of layers too low");
            
            return numOfLayers * (numOfLayers + 1) / 2;
        }

        // Returns the Layer in the pyramid structure for a given node
        // Throws exception if node is negative (as there are no nodes that are negative)
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

        // Returns a list of lists with corresponding children for the node,
        // if the node doesn't have a child the element is null
        // Example: if node 0 has two children 1 and 2 then returns a list with one element at index 0
        //          with two elements 1 and 2 and since nodes 1 and 2 are leaf nodes the list contains
        //          two elements at indecies 1 and 2 with reference to null
        public static List<List<int>> GenerateAdjacencyList(int numOfLayers)
        {
            int numberOfNodes = GetNumberOfNodes(numOfLayers);
            var output = new List<List<int>>();

            for (int node = 0; node < numberOfNodes; node++)
            {
                var children = new List<int>();
                int layer = GetLayerFromNode(node);
                // check whether the node is not in the last layer (no children)
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

        // Returns a list of paths (path = list of values of nodes) 
        public static List<List<int>> FindAllValidPaths(int node, List<List<int>> adjList, List<int> valueList)
        {
            if (node < 0 || node >= adjList.Count)
                throw new Exception("The node is out of the range");

            var output = new List<List<int>>();

            // checking for being a leaf node
            if (adjList[node] == null)
            {
                output.Add( new List<int> { valueList[node] } );
                return output;
            }

            // investigate each child for potential paths
            foreach (var childIdx in adjList[node])
            {
                // checking for opposite parity of parent and child 
                if (Math.Abs(valueList[childIdx] - valueList[node]) % 2 == 1)
                {
                    var subPaths = FindAllValidPaths(childIdx, adjList, valueList);

                    foreach (var path in subPaths)
                    {
                        var partialPath = new List<int> { valueList[node] };
                        partialPath.AddRange(path);
                        output.Add(partialPath);
                    }
                }
            }

            return output;
        }

        // Returns a list representing the values of the nodes in the path with largest sum
        public static List<int> FindMaxSumPath(string pyramid)
        {
            var lines = new List<string>(pyramid.Split('\n'));
            int layers = lines.Count;

            var adjList = GenerateAdjacencyList(layers);
            var valueList = lines.SelectMany(x => x.Split())
                .Select(x => int.Parse(x))
                .ToList();

            var paths = FindAllValidPaths(0, adjList, valueList);

            List<int> result = new List<int>();
            int maxSum = 0;
            foreach (var path in paths)
            {
                int sum = path.Sum();
                if (sum > maxSum)
                {
                    maxSum = sum;
                    result = path;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            string fileContent = File.ReadAllText("../../../input2.txt");
            var maxSumPath = FindMaxSumPath(fileContent);
            
            Console.WriteLine($"Path with the largest sum of {maxSumPath.Sum()} in the pyramid is:");
            foreach (var num in maxSumPath)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }
        
    }
}