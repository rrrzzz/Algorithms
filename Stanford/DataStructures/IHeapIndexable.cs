namespace Algorithms.Stanford.DataStructures
{
    public interface IHeapIndexable
    {
        int HeapIndex { get; set; }
        int GetKey();
        void SetKey(int key);
    }
}
