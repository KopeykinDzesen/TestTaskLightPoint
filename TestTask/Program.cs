using System;

namespace TestTask
{

    public class BinaryTree
    {

        private int? value;
        private BinaryTree left;
        private BinaryTree right;
        private BinaryTree parent;
        private int floor;
        private int countNode;
        private int countFloor;

        public int? GetValue()
        {
            return this.value;
        }
        public int? GetFloor()
        {
            return this.floor;
        }

        // добавить элемент в дерево
        public void Insert(int value, BinaryTree parent = null)
        {
            if (this.value == null)
            {
                this.value = value;
                this.parent = parent;
                if (parent != null)
                {
                    int flo = parent.floor;
                    this.floor = flo + 1;
                }
                else this.floor = 1;
                return;
            }
            else if (this.value == value)
                return;
            else if (this.value > value)
            {
                if (left == null)
                    this.left = new BinaryTree();
                left.Insert(value, this);
            }
            else if (this.value < value)
            {
                if (right == null)
                    this.right = new BinaryTree();
                right.Insert(value, this);
            }
        }

        private void Insert(BinaryTree binaryTree)
        {
            if (left == null)
            {
                this.left = binaryTree;
                binaryTree.parent = this;
                return;
            }
            left.Insert(binaryTree);
        }

        // посчитать количество элементов, начиная с узла binaryTree
        public int CountNode()
        {
            countNode = 0;
            if (this.left != null)
                countNode += this.left.CountNode();
            countNode++;
            if (this.right != null)
                countNode += this.right.CountNode();
            return countNode;
        }

        // посчитать количество этажей, начиная с узла binaryTree
        public int CountFloor()
        {
            countFloor = 0;
            if (this.left != null)
                countFloor = this.left.CountFloor();
            if (this.floor > countFloor)
                countFloor = this.floor;
            if (this.right != null)
                countFloor = this.right.CountFloor();
            return countFloor;
        }

        // перевести дерево в тип string (в одну отсортированую строку)
        public string BinaryTreeToStr()
        {
            string result = "";
            if (this.left != null)
                result += this.left.BinaryTreeToStr();
            result += this.value.ToString() + "[" + this.floor.ToString() + "]" + " ";
            if (this.right != null)
                result += this.right.BinaryTreeToStr();
            return result;
        }

        // найти узел с value значением
        public BinaryTree Search(int value)
        {
            if (this.value == value)
                return this;
            if (this.value > value)
            {
                if (left != null)
                    return this.left.Search(value);
                else
                    return null;
            }
            else
            {
                if (right != null)
                    return this.right.Search(value);
                else
                    return null;
            }
        }

        // удалить значение
        public void Remove(int value)
        {
            BinaryTree removeNode = Search(value);
            if (removeNode != null)
                Remove(removeNode);
        }

        // удалить узел
        public void Remove(BinaryTree removeNode)
        {
            BinaryTree treeParent = removeNode.parent;
            bool? side = WhichSide(removeNode); // true: справа, false: слева, null: нет родителя
            if (removeNode.left == null && removeNode.right == null)
            {
                if (side == true)
                    removeNode.parent.right = null;
                else if (side == false)
                    removeNode.parent.left = null;
                return;
            }
            if (removeNode.left == null)
            {
                if (side == true)
                    removeNode.parent.right = removeNode.right;
                else
                    removeNode.parent.left = removeNode.right;
                removeNode.right.parent = removeNode.parent;
                removeNode.right.floor--;
                return;
            }
            if (removeNode.right == null)
            {
                if (side == true)
                    removeNode.parent.right = removeNode.left;
                else
                    removeNode.parent.left = removeNode.left;
                removeNode.left.parent = removeNode.parent;
                removeNode.left.floor--;
                return;
            }
            if (side == true)
            {
                removeNode.parent.right = removeNode.right;
                Insert(removeNode.left);
                RecountFloor();
            }
            if (side == false)
            {
                removeNode.parent.left = removeNode.right;
                Insert(removeNode.left);
                RecountFloor();
            }
            if (side == null)
            {
                BinaryTree bufLeft = removeNode.left;
                BinaryTree bufRightLeft = removeNode.right.left;
                BinaryTree bufRightRight = removeNode.right.right;
                removeNode.value = removeNode.right.value;
                removeNode.right = bufRightRight;
                removeNode.left = bufRightLeft;
                Insert(bufLeft);
                RecountFloor();
            }
        }

        // узнать с какой стороны node относительно родителя
        // true: справа, false: слева, null: нет родителя
        private bool? WhichSide(BinaryTree node)
        {
            if (node.parent == null)
                return null;
            else if (node.parent.right == node)
                return true;
            else
                return false;
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            BinaryTree tree = new BinaryTree();
            tree.Insert(20);
            tree.Insert(10);
            tree.Insert(30);
            tree.Insert(5);
            tree.Insert(8);
            tree.Insert(15);
            tree.Insert(13);
            tree.Insert(25);
            tree.Insert(23);
            tree.Insert(35);
            tree.Insert(40);

            Console.WriteLine(tree.BinaryTreeToStr());
            Console.WriteLine(tree.CountNode());
            Console.WriteLine(tree.CountFloor());

            BinaryTree treeSearch = new BinaryTree();
            treeSearch = tree.Search(30);
            if (treeSearch != null)
            {
                Console.WriteLine(treeSearch.GetValue().ToString());
                Console.WriteLine(treeSearch.GetFloor().ToString());
            }
            treeSearch = tree.Search(4);
            if (treeSearch != null)
                Console.WriteLine(treeSearch.GetValue().ToString());
            treeSearch = tree.Search(40);
            if (treeSearch != null)
                Console.WriteLine(treeSearch.GetValue().ToString());

            tree.Remove(20);

            Console.WriteLine(tree.BinaryTreeToStr());
            Console.WriteLine(tree.CountNode());
            Console.WriteLine(tree.CountFloor());

        }
    }

}
