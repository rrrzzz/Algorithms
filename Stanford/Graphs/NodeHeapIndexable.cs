using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class NodeHeapIndexable : Node, IHeapIndexable
    {
        public int Value { get; set; } = Globals.DefaultDijkstraValue;
        public int HeapIndex { get; set; }

        public NodeHeapIndexable(){}
        public NodeHeapIndexable(int id) : base(id){}

        public int GetKey()
        {
            return Value;
        }

        public void SetKey(int key)
        {
            Value = key;
        }
    }
}
