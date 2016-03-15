namespace _02.RangeInTree
{
    using System;
    using System.Linq;
    using AvlTreeLab;

    public static class RangeInTree
    {
        public static void Main()
        {
            // NOTE: GetRange method written as AvlTree Extension method. Check "AvlTreeExtensions.cs" file.
            var avlTree = new AvlTree<int>();
            var input = GetInputAsNumbers(Console.ReadLine());
            input.ToList().ForEach(avlTree.Add);

            var range = GetInputAsNumbers(Console.ReadLine());

            var numbersInRange = avlTree.GetRange(range[0], range[1]);

            Console.WriteLine(string.Join(" ", numbersInRange));
        }

        private static int[] GetInputAsNumbers(string input)
        {
            return input.Split().Select(int.Parse).ToArray();
        }
    }
}
