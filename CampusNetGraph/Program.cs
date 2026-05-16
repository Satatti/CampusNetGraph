using CampusNetGraph.Controller;

namespace CampusNetGraph
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GraphController controller = new GraphController();

            controller.Start();
        }
    }
}