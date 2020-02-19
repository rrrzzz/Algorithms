namespace Algorithms.Stanford.Graphs
{
    public class Node
    {
        public int Id { get; }
        public bool IsVisited { get; private set; }

        public Node(){}

        public Node(int id)
        {
            Id = id;
        }

        public void Visit()
        {
            IsVisited = true;
        }
    }
}