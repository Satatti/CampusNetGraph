using CampusNetGraph.Model;
using System;

// Vista para mostrar mensajes y el grafo en la consola
namespace CampusNetGraph.View
{
    public class ConsoleView
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowGraph(Graph graph)
        {
            foreach (var vertex in graph.AdjacencyList)
            {
                Console.Write(vertex.Key + " -> ");

                foreach (string neighbor in vertex.Value)
                {
                    Console.Write(neighbor + " ");
                }

                Console.WriteLine();
            }
        }
    }
}