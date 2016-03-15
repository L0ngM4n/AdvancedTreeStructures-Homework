namespace AvlTreeLab
{
    using System;

    public class AvlTree<T> where T : IComparable<T>
    {
        private Node<T> root;

        public int Count { get; private set; }

        public Node<T> Root
        {
            get
            {
                return this.root;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.GetElementAtIndex(index);
            }
        }

        public void Add(T item)
        {
            var inserted = true;
            if (this.root == null)
            {
                this.root = new Node<T>(item);
            }
            else
            {
                inserted = this.InternalInsert(this.root, item);
            }

            if (inserted)
            {
                this.Count++;
            }
        }

        public void ForeachDfs(Action<int, T> action)
        {
            if (this.Count == 0)
            {
                return;
            }

            this.InOrderDfs(this.root, 1, action);
        }

        public bool Contains(T item)
        {
            var node = this.root;
            while (node != null)
            {
                if (node.Value.CompareTo(item) < 0)
                {
                    node = node.RightChild;
                }
                else if (node.Value.CompareTo(item) > 0)
                {
                    node = node.LeftChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private bool InternalInsert(Node<T> node, T item)
        {
            var currentNode = node;
            var newNode = new Node<T>(item);

            // var shouldRetrace = false;
            while (true)
            {
                if (currentNode.Value.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = newNode;
                        currentNode.BallanceFactor--;

                        // shouldRetrace = currentNode.BallanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.RightChild;
                }
                else if (currentNode.Value.CompareTo(item) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = newNode;
                        currentNode.BallanceFactor++;

                        // shouldRetrace = currentNode.BallanceFactor != 0;
                        break;
                    }

                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    return false;
                }
            }

            // if (shouldRetrace)
            // {
            this.RetraceInsert(currentNode);

            // }
            return true;
        }

        private void RetraceInsert(Node<T> node)
        {
            node.Count++;
            var parent = node.Parent;
            while (parent != null)
            {
                parent.Count++;
                parent = parent.Parent;
            }

            parent = node.Parent;

            while (parent != null)
            {
                if (node.IsLeftChild)
                {
                    if (parent.BallanceFactor == 1)
                    {
                        parent.BallanceFactor++;
                        if (node.BallanceFactor == -1)
                        {
                            this.RotateLeft(node);
                        }

                        this.RotateRight(parent);
                        break;
                    }

                    if (parent.BallanceFactor == -1)
                    {
                        parent.BallanceFactor = 0;
                        break;
                    }

                    parent.BallanceFactor = 1;
                }
                else
                {
                    if (parent.BallanceFactor == -1)
                    {
                        parent.BallanceFactor--;
                        if (node.BallanceFactor == 1)
                        {
                            this.RotateRight(node);
                        }

                        this.RotateLeft(parent);
                        break;
                    }

                    if (parent.BallanceFactor == 1)
                    {
                        parent.BallanceFactor = 0;
                        break;
                    }

                    parent.BallanceFactor = -1;
                }

                node = parent;
                parent = node.Parent;
            }
        }

        private void ModifyRotatedNodesCount(Node<T> node)
        {
            var newCount = 1;
            if (node.LeftChild != null)
            {
                newCount += node.LeftChild.Count;
            }

            if (node.RightChild != null)
            {
                newCount += node.RightChild.Count;
            }

            node.Count = newCount;
        }

        private void RotateLeft(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.RightChild;
            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.RightChild = child.LeftChild;
            child.LeftChild = node;

            node.BallanceFactor += 1 - Math.Min(child.BallanceFactor, 0);
            child.BallanceFactor += 1 + Math.Max(node.BallanceFactor, 0);

            this.ModifyRotatedNodesCount(node);
            this.ModifyRotatedNodesCount(child);
        }

        private void RotateRight(Node<T> node)
        {
            var parent = node.Parent;
            var child = node.LeftChild;

            if (parent != null)
            {
                if (node.IsLeftChild)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }
            }
            else
            {
                this.root = child;
                this.root.Parent = null;
            }

            node.LeftChild = child.RightChild;
            child.RightChild = node;

            node.BallanceFactor -= 1 + Math.Max(child.BallanceFactor, 0);
            child.BallanceFactor -= 1 - Math.Min(node.BallanceFactor, 0);

            this.ModifyRotatedNodesCount(node);
            this.ModifyRotatedNodesCount(child);
        }

        private void InOrderDfs(Node<T> node, int depth, Action<int, T> action)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, depth + 1, action);
            }

            action(depth, node.Value);

            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, depth + 1, action);
            }
        }

        private T GetElementAtIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Invalid Index");
            }

            var node = this.root;
            while (true)
            {
                var leftSubTreeCount = 0;
                if (node.LeftChild != null)
                {
                    leftSubTreeCount = node.LeftChild.Count;
                }

                if (index == leftSubTreeCount)
                {
                    return node.Value;
                }

                if (index <= leftSubTreeCount)
                {
                    node = node.LeftChild;
                    continue;
                }

                if (index > leftSubTreeCount)
                {
                    index -= leftSubTreeCount + 1;
                    node = node.RightChild;
                }
            }
        }
    }
}
