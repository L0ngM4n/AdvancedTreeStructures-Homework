namespace AvlTreeLab
{
    using System;

    public class Node<T>
        where T : IComparable<T>
    {
        private Node<T> leftChild;
        private Node<T> rightChild; 

        public Node(T value)
        {
            this.Value = value;
            this.Count = 1;
        }

        public T Value { get; set; }

        public Node<T> Parent { get; set; }

        public Node<T> LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.leftChild = value;
            }
        }

        public Node<T> RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                if (value != null)
                {
                    value.Parent = this;
                }

                this.rightChild = value;
            }
        }

        public int BallanceFactor { get; set; }

        public int Count { get; set; }

        public bool IsLeftChild
        {
            get
            {
                if (this.Parent != null && this.Parent.leftChild != null)
                {
                    return this.Parent.leftChild == this;
                }

                return false;
            }
        }

        public bool IsRightChild
        {
            get
            {
                if (this.Parent != null && this.Parent.rightChild != null)
                {
                    return this.Parent.RightChild == this;
                }

                return false;
            }
        }

        public int ChildrenCount { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}