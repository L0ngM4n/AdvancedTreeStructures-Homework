namespace _03.TreeIndexing
{
    using System;
    using System.Linq;
    using AvlTreeLab;

    public static class TreeIndexing
    {
        public static void Main()
        {
            var avlTree = new AvlTree<int>();
            var input = GetInputAsNumbers(Console.ReadLine());
            input.ToList().ForEach(avlTree.Add);

            while (true)
            {
                var index = int.Parse(Console.ReadLine());
                try
                {
                    var element = avlTree[index];
                    Console.WriteLine(element);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static int[] GetInputAsNumbers(string input)
        {
            return input.Split().Select(int.Parse).ToArray();
        }
    }
}
