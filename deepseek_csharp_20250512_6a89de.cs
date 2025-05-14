using System;
using System.Collections.Generic;

public class BinarySearchTree
{
    private class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key)
        {
            Key = key;
            Left = null;
            Right = null;
        }
    }
}
    private Node root;

    // Конструкторы
    public BinarySearchTree()
    {
        root = null;
    }

    public BinarySearchTree(int key)
    {
        root = new Node(key);
    }

    // Деструктор (финализатор)
    ~BinarySearchTree()
    {
        ClearTree(ref root);
    }

    private void ClearTree(ref Node node)
    {
        if (node != null)
        {
            ClearTree(ref node.Left);
            ClearTree(ref node.Right);
            node = null;
        }
    }

    // Добавление нового узла
    public void Add(int key)
    {
        if (root == null)
        {
            root = new Node(key);
        }
        else
        {
            AddRecursive(root, key);
        }
    }

    private void AddRecursive(Node node, int key)
    {
        if (key < node.Key)
        {
            if (node.Left == null)
            {
                node.Left = new Node(key);
            }
            else
            {
                AddRecursive(node.Left, key);
            }
        }
        else if (key > node.Key)
        {
            if (node.Right == null)
            {
                node.Right = new Node(key);
            }
            else
            {
                AddRecursive(node.Right, key);
            }
        }
        // Если ключ уже существует, ничего не делаем
    }

    // Удаление узла по ключу
    public void Remove(int key)
    {
        root = RemoveRecursive(root, key);
    }

    private Node RemoveRecursive(Node node, int key)
    {
        if (node == null) return null;

        if (key < node.Key)
        {
            node.Left = RemoveRecursive(node.Left, key);
        }
        else if (key > node.Key)
        {
            node.Right = RemoveRecursive(node.Right, key);
        }
        else
        {
            // Узел с одним потомком или без потомков
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }

            // Узел с двумя потомками: получаем inorder-преемника (минимальный в правом поддереве)
            node.Key = MinValue(node.Right);

            // Удаляем inorder-преемника
            node.Right = RemoveRecursive(node.Right, node.Key);
        }
        return node;
    }

    private int MinValue(Node node)
    {
        int minValue = node.Key;
        while (node.Left != null)
        {
            minValue = node.Left.Key;
            node = node.Left;
        }
        return minValue;
    }

    // Вычисление глубины дерева
    public int Depth()
    {
        return DepthRecursive(root);
    }

    private int DepthRecursive(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        else
        {
            int leftDepth = DepthRecursive(node.Left);
            int rightDepth = DepthRecursive(node.Right);

            return Math.Max(leftDepth, rightDepth) + 1;
        }
    }

    // Объединение двух деревьев
    public void Merge(BinarySearchTree otherTree)
    {
        if (otherTree == null || otherTree.root == null)
            return;

        // Собираем все узлы второго дерева в список
        List<int> nodes = new List<int>();
        InOrderTraversal(otherTree.root, nodes);

        // Добавляем все узлы в текущее дерево
        foreach (int key in nodes)
        {
            Add(key);
        }
    }

    private void InOrderTraversal(Node node, List<int> nodes)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, nodes);
            nodes.Add(node.Key);
            InOrderTraversal(node.Right, nodes);
        }
    }

    // Количество узлов на заданном уровне
    public int NodesAtLevel(int level)
    {
        if (level < 0)
            return 0;

        return NodesAtLevelRecursive(root, level);
    }

    private int NodesAtLevelRecursive(Node node, int level)
    {
        if (node == null)
            return 0;
        if (level == 0)
            return 1;

        return NodesAtLevelRecursive(node.Left, level - 1) + 
               NodesAtLevelRecursive(node.Right, level - 1);
    }

    // Проверка подобия
