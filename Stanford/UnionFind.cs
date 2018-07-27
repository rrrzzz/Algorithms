namespace Algorithms.Stanford
{
    public class UnionFind
    {
        public int[] Nodes { get; set; }
        public int[] Sizes { get; set; }

        public UnionFind(){}

        public UnionFind(int n)
        {
            IntializeUnionFindStructure(n);
        }

        public void IntializeUnionFindStructure(int n)
        {
            Nodes = new int[n];
            Sizes = new int[n];

            for (int i = 0; i < n; i++)
            {
                Nodes[i] = i;
                Sizes[i] = 1;
            }
        }

        public int FindRoot(int index)
        {
            var parentIndex = Nodes[index];
            if (Nodes[index] != Nodes[parentIndex])
            {
                Nodes[index] = FindRoot(parentIndex);
            }

            return Nodes[index];
        }

        public bool IsComponentsConnected(int firstComponent, int secondComponent)
        {
            return FindRoot(firstComponent) == FindRoot(secondComponent);
        }

        public void Union(int firstComponent, int secondComponent)
        {
            var firstRoot = FindRoot(firstComponent);
            var secondRoot = FindRoot(secondComponent);

            if (Sizes[firstRoot] < Sizes[secondRoot])
            {
                Nodes[firstRoot] = secondRoot;
                Sizes[secondRoot] += Sizes[firstRoot];
            }
            else
            {
                Nodes[secondRoot] = firstRoot;
                Sizes[firstRoot] += Sizes[secondRoot];
            }
        }
    }
}
