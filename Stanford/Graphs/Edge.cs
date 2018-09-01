namespace Algorithms.Stanford.Graphs
{
    public class Edge
    {
        public Edge(int head, int tail, int weight)
        {
            HeadId = head;
            TailId = tail;
            Weight = weight;
        }

        public int HeadId { get; set; }
        public int TailId { get; set; }
        public int Weight { get; set; }
    }
}
