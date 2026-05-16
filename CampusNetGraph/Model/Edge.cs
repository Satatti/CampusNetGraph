namespace CampusNetGraph.Model
{
    public class Edge
    {
        public string From { get; set; }

        public string To { get; set; }

        public Edge(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}