using System;
using System.Collections.Generic;

class Program
{
    // ===================== Бінарне дерево пошуку (BST) =====================
    class BstNode
    {
        public int Value;
        public BstNode? Left;
        public BstNode? Right;
        public BstNode? Parent;

        public BstNode(int value)
        {
            Value = value;
        }
    }

    class BinarySearchTree
    {
        public BstNode? Root;

        public void Insert(int value)
        {
            BstNode newNode = new BstNode(value);
            if (Root == null)
            {
                Root = newNode;
                return;
            }
            BstNode current = Root;
            while (true)
            {
                if (value < current.Value)
                {
                    if (current.Left == null)
                    {
                        current.Left = newNode;
                        newNode.Parent = current;
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = newNode;
                        newNode.Parent = current;
                        return;
                    }
                    current = current.Right;
                }
            }
        }

        public BstNode? Search(int value)
        {
            BstNode? current = Root;
            while (current != null && current.Value != value)
            {
                current = value < current.Value ? current.Left : current.Right;
            }
            return current;
        }

        public void Delete(int value)
        {
            BstNode? z = Search(value);
            if (z == null)
            {
                return;
            }
            if (z.Left == null)
            {
                Transplant(z, z.Right);
            }
            else if (z.Right == null)
            {
                Transplant(z, z.Left);
            }
            else
            {
                BstNode y = Minimum(z.Right);
                if (y.Parent != z)
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right!.Parent = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
            }
        }

        private void Transplant(BstNode u, BstNode? v)
        {
            if (u.Parent == null)
            {
                Root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        private BstNode Minimum(BstNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public void LeftRotate(BstNode x)
        {
            BstNode? y = x.Right;
            if (y == null)
            {
                return;
            }
            x.Right = y.Left;
            if (y.Left != null)
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;
        }

        public void RightRotate(BstNode x)
        {
            BstNode? y = x.Left;
            if (y == null)
            {
                return;
            }
            x.Left = y.Right;
            if (y.Right != null)
            {
                y.Right.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else
            {
                x.Parent.Left = y;
            }
            y.Right = x;
            x.Parent = y;
        }
    }

    // ===================== Червоно-чорне дерево (RB Tree) =====================
    enum RbColor
    {
        Red,
        Black,
    }

    class RbNode
    {
        public int Value;
        public RbColor Color;
        public RbNode Left;
        public RbNode Right;
        public RbNode Parent;
        public bool IsNil;

        public RbNode(int value, RbColor color)
        {
            Value = value;
            Color = color;
            Left = this;
            Right = this;
            Parent = this;
            IsNil = false;
        }

        public static RbNode CreateNil()
        {
            RbNode nil = new RbNode(0, RbColor.Black);
            nil.IsNil = true;
            return nil;
        }
    }

    class RedBlackTree
    {
        public readonly RbNode Nil;
        public RbNode Root;

        public RedBlackTree()
        {
            Nil = RbNode.CreateNil();
            Root = Nil;
        }

        public void Insert(int value)
        {
            RbNode z = new RbNode(value, RbColor.Red)
            {
                Left = Nil,
                Right = Nil,
                Parent = Nil,
            };

            RbNode y = Nil;
            RbNode x = Root;
            while (!x.IsNil)
            {
                y = x;
                x = z.Value < x.Value ? x.Left : x.Right;
            }
            z.Parent = y;
            if (y.IsNil)
            {
                Root = z;
            }
            else if (z.Value < y.Value)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }
            InsertFixup(z);
        }

        private void InsertFixup(RbNode z)
        {
            while (z.Parent.Color == RbColor.Red)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    RbNode y = z.Parent.Parent.Right;
                    if (y.Color == RbColor.Red)
                    {
                        // Випадок 1: дядько червоний, перефарбовуємо
                        z.Parent.Color = RbColor.Black;
                        y.Color = RbColor.Black;
                        z.Parent.Parent.Color = RbColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            // Випадок 2: дядько чорний, z — правий нащадок
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        // Випадок 3: дядько чорний, z — лівий нащадок
                        z.Parent.Color = RbColor.Black;
                        z.Parent.Parent.Color = RbColor.Red;
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    RbNode y = z.Parent.Parent.Left;
                    if (y.Color == RbColor.Red)
                    {
                        z.Parent.Color = RbColor.Black;
                        y.Color = RbColor.Black;
                        z.Parent.Parent.Color = RbColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = RbColor.Black;
                        z.Parent.Parent.Color = RbColor.Red;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            Root.Color = RbColor.Black;
        }

        public RbNode Search(int value)
        {
            RbNode current = Root;
            while (!current.IsNil && current.Value != value)
            {
                current = value < current.Value ? current.Left : current.Right;
            }
            return current;
        }

        public bool Delete(int value)
        {
            RbNode z = Search(value);
            if (z.IsNil)
            {
                return false;
            }
            DeleteNode(z);
            return true;
        }

        private void DeleteNode(RbNode z)
        {
            RbNode y = z;
            RbColor yOriginalColor = y.Color;
            RbNode x;
            if (z.Left.IsNil)
            {
                x = z.Right;
                Transplant(z, z.Right);
            }
            else if (z.Right.IsNil)
            {
                x = z.Left;
                Transplant(z, z.Left);
            }
            else
            {
                y = Minimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                {
                    x.Parent = y;
                }
                else
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.Parent = y;
                y.Color = z.Color;
            }
            if (yOriginalColor == RbColor.Black)
            {
                DeleteFixup(x);
            }
        }

        private void DeleteFixup(RbNode x)
        {
            while (x != Root && x.Color == RbColor.Black)
            {
                if (x == x.Parent.Left)
                {
                    RbNode w = x.Parent.Right;
                    if (w.Color == RbColor.Red)
                    {
                        w.Color = RbColor.Black;
                        x.Parent.Color = RbColor.Red;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }
                    if (w.Left.Color == RbColor.Black && w.Right.Color == RbColor.Black)
                    {
                        w.Color = RbColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.Color == RbColor.Black)
                        {
                            w.Left.Color = RbColor.Black;
                            w.Color = RbColor.Red;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = RbColor.Black;
                        w.Right.Color = RbColor.Black;
                        LeftRotate(x.Parent);
                        x = Root;
                    }
                }
                else
                {
                    RbNode w = x.Parent.Left;
                    if (w.Color == RbColor.Red)
                    {
                        w.Color = RbColor.Black;
                        x.Parent.Color = RbColor.Red;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }
                    if (w.Right.Color == RbColor.Black && w.Left.Color == RbColor.Black)
                    {
                        w.Color = RbColor.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.Color == RbColor.Black)
                        {
                            w.Right.Color = RbColor.Black;
                            w.Color = RbColor.Red;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }
                        w.Color = x.Parent.Color;
                        x.Parent.Color = RbColor.Black;
                        w.Left.Color = RbColor.Black;
                        RightRotate(x.Parent);
                        x = Root;
                    }
                }
            }
            x.Color = RbColor.Black;
        }

        private void Transplant(RbNode u, RbNode v)
        {
            if (u.Parent.IsNil)
            {
                Root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            v.Parent = u.Parent;
        }

        private RbNode Minimum(RbNode node)
        {
            while (!node.Left.IsNil)
            {
                node = node.Left;
            }
            return node;
        }

        public void LeftRotate(RbNode x)
        {
            RbNode y = x.Right;
            x.Right = y.Left;
            if (!y.Left.IsNil)
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent.IsNil)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;
        }

        public void RightRotate(RbNode x)
        {
            RbNode y = x.Left;
            x.Left = y.Right;
            if (!y.Right.IsNil)
            {
                y.Right.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent.IsNil)
            {
                Root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else
            {
                x.Parent.Left = y;
            }
            y.Right = x;
            x.Parent = y;
        }

        // Перефарбування двох вузлів-братів у протилежний колір
        public bool RecolorSiblings(int parentValue)
        {
            RbNode parent = Search(parentValue);
            if (parent.IsNil || parent.Left.IsNil || parent.Right.IsNil)
            {
                return false;
            }
            if (parent.Left.Color != parent.Right.Color)
            {
                return false;
            }
            parent.Left.Color = parent.Left.Color == RbColor.Red ? RbColor.Black : RbColor.Red;
            parent.Right.Color = parent.Right.Color == RbColor.Red ? RbColor.Black : RbColor.Red;
            return true;
        }
    }

    // ===================== АВЛ-дерево =====================
    class AvlNode
    {
        public int Value;
        public int Height;
        public AvlNode? Left;
        public AvlNode? Right;
        public AvlNode? Parent;

        public AvlNode(int value)
        {
            Value = value;
            Height = 1;
        }
    }

    class AvlTree
    {
        public AvlNode? Root;

        private static int GetHeight(AvlNode? node)
        {
            return node?.Height ?? 0;
        }

        private static int BalanceFactor(AvlNode node)
        {
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private static void UpdateHeight(AvlNode node)
        {
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }

        public AvlNode LeftRotate(AvlNode x)
        {
            AvlNode y = x.Right!;
            AvlNode? t2 = y.Left;
            y.Left = x;
            x.Right = t2;
            y.Parent = x.Parent;
            x.Parent = y;
            if (t2 != null)
            {
                t2.Parent = x;
            }
            UpdateHeight(x);
            UpdateHeight(y);
            return y;
        }

        public AvlNode RightRotate(AvlNode y)
        {
            AvlNode x = y.Left!;
            AvlNode? t2 = x.Right;
            x.Right = y;
            y.Left = t2;
            x.Parent = y.Parent;
            y.Parent = x;
            if (t2 != null)
            {
                t2.Parent = y;
            }
            UpdateHeight(y);
            UpdateHeight(x);
            return x;
        }

        public void Insert(int value)
        {
            Root = InsertRec(Root, value, null);
        }

        private AvlNode InsertRec(AvlNode? node, int value, AvlNode? parent)
        {
            if (node == null)
            {
                AvlNode created = new AvlNode(value);
                created.Parent = parent;
                return created;
            }
            if (value < node.Value)
            {
                node.Left = InsertRec(node.Left, value, node);
            }
            else if (value > node.Value)
            {
                node.Right = InsertRec(node.Right, value, node);
            }
            else
            {
                return node;
            }
            UpdateHeight(node);
            int balance = BalanceFactor(node);
            if (balance > 1 && value < node.Left!.Value)
            {
                return RightRotate(node);
            }
            if (balance < -1 && value > node.Right!.Value)
            {
                return LeftRotate(node);
            }
            if (balance > 1 && value > node.Left!.Value)
            {
                node.Left = LeftRotate(node.Left);
                node.Left.Parent = node;
                return RightRotate(node);
            }
            if (balance < -1 && value < node.Right!.Value)
            {
                node.Right = RightRotate(node.Right);
                node.Right.Parent = node;
                return LeftRotate(node);
            }
            return node;
        }

        public void Delete(int value)
        {
            Root = DeleteRec(Root, value);
            if (Root != null)
            {
                Root.Parent = null;
            }
        }

        private AvlNode? DeleteRec(AvlNode? node, int value)
        {
            if (node == null)
            {
                return null;
            }
            if (value < node.Value)
            {
                node.Left = DeleteRec(node.Left, value);
                if (node.Left != null)
                {
                    node.Left.Parent = node;
                }
            }
            else if (value > node.Value)
            {
                node.Right = DeleteRec(node.Right, value);
                if (node.Right != null)
                {
                    node.Right.Parent = node;
                }
            }
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    AvlNode? child = node.Left ?? node.Right;
                    return child;
                }
                AvlNode successor = node.Right;
                while (successor.Left != null)
                {
                    successor = successor.Left;
                }
                node.Value = successor.Value;
                node.Right = DeleteRec(node.Right, successor.Value);
                if (node.Right != null)
                {
                    node.Right.Parent = node;
                }
            }
            UpdateHeight(node);
            int balance = BalanceFactor(node);
            if (balance > 1 && BalanceFactor(node.Left!) >= 0)
            {
                return RightRotate(node);
            }
            if (balance > 1 && BalanceFactor(node.Left!) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                node.Left.Parent = node;
                return RightRotate(node);
            }
            if (balance < -1 && BalanceFactor(node.Right!) <= 0)
            {
                return LeftRotate(node);
            }
            if (balance < -1 && BalanceFactor(node.Right!) > 0)
            {
                node.Right = RightRotate(node.Right!);
                node.Right.Parent = node;
                return LeftRotate(node);
            }
            return node;
        }
    }

    // ===================== Splay-дерево =====================
    class SplayNode
    {
        public int Value;
        public SplayNode? Left;
        public SplayNode? Right;
        public SplayNode? Parent;

        public SplayNode(int value)
        {
            Value = value;
        }
    }

    class SplayTree
    {
        public SplayNode? Root;

        private void RotateLeft(SplayNode x)
        {
            SplayNode y = x.Right!;
            x.Right = y.Left;
            if (y.Left != null)
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;
        }

        private void RotateRight(SplayNode x)
        {
            SplayNode y = x.Left!;
            x.Left = y.Right;
            if (y.Right != null)
            {
                y.Right.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Right)
            {
                x.Parent.Right = y;
            }
            else
            {
                x.Parent.Left = y;
            }
            y.Right = x;
            x.Parent = y;
        }

        // Splay алгоритм — переміщає вузол x у корінь
        public void Splay(SplayNode x)
        {
            while (x.Parent != null)
            {
                SplayNode parent = x.Parent;
                SplayNode? grandParent = parent.Parent;
                if (grandParent == null)
                {
                    // Zig
                    if (x == parent.Left)
                    {
                        RotateRight(parent);
                    }
                    else
                    {
                        RotateLeft(parent);
                    }
                }
                else if (x == parent.Left && parent == grandParent.Left)
                {
                    // Zig-Zig
                    RotateRight(grandParent);
                    RotateRight(parent);
                }
                else if (x == parent.Right && parent == grandParent.Right)
                {
                    // Zig-Zig (дзеркальний)
                    RotateLeft(grandParent);
                    RotateLeft(parent);
                }
                else if (x == parent.Right && parent == grandParent.Left)
                {
                    // Zig-Zag
                    RotateLeft(parent);
                    RotateRight(grandParent);
                }
                else
                {
                    // Zig-Zag (дзеркальний)
                    RotateRight(parent);
                    RotateLeft(grandParent);
                }
            }
        }

        public void Insert(int value)
        {
            SplayNode newNode = new SplayNode(value);
            if (Root == null)
            {
                Root = newNode;
                return;
            }
            SplayNode current = Root;
            SplayNode parent;
            while (true)
            {
                parent = current;
                if (value < current.Value)
                {
                    if (current.Left == null)
                    {
                        current.Left = newNode;
                        newNode.Parent = current;
                        break;
                    }
                    current = current.Left;
                }
                else if (value > current.Value)
                {
                    if (current.Right == null)
                    {
                        current.Right = newNode;
                        newNode.Parent = current;
                        break;
                    }
                    current = current.Right;
                }
                else
                {
                    Splay(current);
                    return;
                }
            }
            Splay(newNode);
        }

        public SplayNode? Search(int value)
        {
            SplayNode? current = Root;
            SplayNode? last = null;
            while (current != null)
            {
                last = current;
                if (value < current.Value)
                {
                    current = current.Left;
                }
                else if (value > current.Value)
                {
                    current = current.Right;
                }
                else
                {
                    Splay(current);
                    return current;
                }
            }
            if (last != null)
            {
                Splay(last);
            }
            return null;
        }

        public bool Delete(int value)
        {
            SplayNode? node = Search(value);
            if (node == null || node.Value != value)
            {
                return false;
            }
            SplayNode? leftSubtree = Root!.Left;
            SplayNode? rightSubtree = Root.Right;
            if (leftSubtree != null)
            {
                leftSubtree.Parent = null;
            }
            if (rightSubtree != null)
            {
                rightSubtree.Parent = null;
            }
            if (leftSubtree == null)
            {
                Root = rightSubtree;
            }
            else
            {
                Root = leftSubtree;
                SplayNode max = leftSubtree;
                while (max.Right != null)
                {
                    max = max.Right;
                }
                Splay(max);
                Root!.Right = rightSubtree;
                if (rightSubtree != null)
                {
                    rightSubtree.Parent = Root;
                }
            }
            return true;
        }
    }

    // ===================== Друк дерев =====================
    static void PrintBst(BstNode? node, string indent = "", bool last = true)
    {
        if (node == null)
        {
            return;
        }
        Console.Write(indent);
        if (last)
        {
            Console.Write("└── ");
            indent += "    ";
        }
        else
        {
            Console.Write("├── ");
            indent += "│   ";
        }
        Console.WriteLine(node.Value);
        PrintBst(node.Left, indent, node.Right == null);
        PrintBst(node.Right, indent, true);
    }

    static void PrintRb(RedBlackTree tree, RbNode node, string indent = "", bool last = true)
    {
        if (node.IsNil)
        {
            return;
        }
        Console.Write(indent);
        if (last)
        {
            Console.Write("└── ");
            indent += "    ";
        }
        else
        {
            Console.Write("├── ");
            indent += "│   ";
        }
        string color = node.Color == RbColor.Red ? "Ч" : "Б";
        Console.WriteLine($"{node.Value}({color})");
        bool rightIsNil = node.Right.IsNil;
        PrintRb(tree, node.Left, indent, rightIsNil);
        PrintRb(tree, node.Right, indent, true);
    }

    static void PrintAvl(AvlNode? node, string indent = "", bool last = true)
    {
        if (node == null)
        {
            return;
        }
        Console.Write(indent);
        if (last)
        {
            Console.Write("└── ");
            indent += "    ";
        }
        else
        {
            Console.Write("├── ");
            indent += "│   ";
        }
        Console.WriteLine($"{node.Value} (h={node.Height})");
        PrintAvl(node.Left, indent, node.Right == null);
        PrintAvl(node.Right, indent, true);
    }

    static void PrintSplay(SplayNode? node, string indent = "", bool last = true)
    {
        if (node == null)
        {
            return;
        }
        Console.Write(indent);
        if (last)
        {
            Console.Write("└── ");
            indent += "    ";
        }
        else
        {
            Console.Write("├── ");
            indent += "│   ";
        }
        Console.WriteLine(node.Value);
        PrintSplay(node.Left, indent, node.Right == null);
        PrintSplay(node.Right, indent, true);
    }

    // ===================== In-order обхід для перевірки =====================
    static void InOrderBst(BstNode? node, List<int> output)
    {
        if (node == null)
        {
            return;
        }
        InOrderBst(node.Left, output);
        output.Add(node.Value);
        InOrderBst(node.Right, output);
    }

    static void InOrderRb(RedBlackTree tree, RbNode node, List<int> output)
    {
        if (node.IsNil)
        {
            return;
        }
        InOrderRb(tree, node.Left, output);
        output.Add(node.Value);
        InOrderRb(tree, node.Right, output);
    }

    static void InOrderAvl(AvlNode? node, List<int> output)
    {
        if (node == null)
        {
            return;
        }
        InOrderAvl(node.Left, output);
        output.Add(node.Value);
        InOrderAvl(node.Right, output);
    }

    static void InOrderSplay(SplayNode? node, List<int> output)
    {
        if (node == null)
        {
            return;
        }
        InOrderSplay(node.Left, output);
        output.Add(node.Value);
        InOrderSplay(node.Right, output);
    }

    // ===================== Демонстрації =====================
    static int[] ReadIntArray(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine() ?? "";
        var parts = line.Split(new[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        var values = new List<int>();
        foreach (string p in parts)
        {
            if (int.TryParse(p, out int v))
            {
                values.Add(v);
            }
        }
        return values.ToArray();
    }

    static int? ReadInt(string prompt)
    {
        Console.Write(prompt);
        string line = Console.ReadLine() ?? "";
        if (int.TryParse(line.Trim(), out int v))
        {
            return v;
        }
        Console.WriteLine("Некоректне число.");
        return null;
    }

    static void DemoBinarySearchTree()
    {
        Console.Clear();
        Console.WriteLine("Бінарне дерево пошуку (BST)");
        var tree = new BinarySearchTree();
        int[] keys = ReadIntArray("Введіть ключі для вставки (через пробіл): ");
        if (keys.Length == 0)
        {
            keys = new[] { 50, 30, 70, 20, 40, 60, 80 };
            Console.WriteLine($"Використовую демо-набір: {string.Join(", ", keys)}");
        }
        foreach (int k in keys)
        {
            tree.Insert(k);
        }
        Console.WriteLine("\nСтруктура BST:");
        PrintBst(tree.Root);
        var inorder = new List<int>();
        InOrderBst(tree.Root, inorder);
        Console.WriteLine($"In-order: {string.Join(" ", inorder)}");

        int? toDelete = ReadInt("\nВведіть значення для видалення (Enter — пропустити): ");
        if (toDelete.HasValue)
        {
            tree.Delete(toDelete.Value);
            Console.WriteLine($"\nПісля видалення {toDelete.Value}:");
            PrintBst(tree.Root);
        }

        int? rotateValue = ReadInt("\nВведіть значення вузла для лівого повороту (Enter — пропустити): ");
        if (rotateValue.HasValue)
        {
            BstNode? target = tree.Search(rotateValue.Value);
            if (target == null)
            {
                Console.WriteLine("Вузол не знайдено.");
            }
            else
            {
                tree.LeftRotate(target);
                Console.WriteLine("\nПісля лівого повороту:");
                PrintBst(tree.Root);
            }
        }

        int? rRotateValue = ReadInt("\nВведіть значення вузла для правого повороту (Enter — пропустити): ");
        if (rRotateValue.HasValue)
        {
            BstNode? target = tree.Search(rRotateValue.Value);
            if (target == null)
            {
                Console.WriteLine("Вузол не знайдено.");
            }
            else
            {
                tree.RightRotate(target);
                Console.WriteLine("\nПісля правого повороту:");
                PrintBst(tree.Root);
            }
        }
    }

    static void DemoRedBlackTree()
    {
        Console.Clear();
        Console.WriteLine("Червоно-чорне дерево");
        var tree = new RedBlackTree();
        int[] keys = ReadIntArray("Введіть ключі для вставки (через пробіл): ");
        if (keys.Length == 0)
        {
            keys = new[] { 41, 38, 31, 12, 19, 8 };
            Console.WriteLine($"Використовую демо-набір (за методичкою): {string.Join(", ", keys)}");
        }
        foreach (int k in keys)
        {
            tree.Insert(k);
            Console.WriteLine($"\nПісля вставки {k} (Ч=червоний, Б=чорний):");
            PrintRb(tree, tree.Root);
        }

        int? toDelete = ReadInt("\nВведіть значення для видалення (Enter — пропустити): ");
        if (toDelete.HasValue)
        {
            bool removed = tree.Delete(toDelete.Value);
            if (removed)
            {
                Console.WriteLine($"\nПісля видалення {toDelete.Value}:");
                PrintRb(tree, tree.Root);
            }
            else
            {
                Console.WriteLine("Вузол не знайдено.");
            }
        }

        int? recolorParent = ReadInt(
            "\nВведіть значення батька, чиїх синів-братів треба перефарбувати (Enter — пропустити): ");
        if (recolorParent.HasValue)
        {
            bool ok = tree.RecolorSiblings(recolorParent.Value);
            if (ok)
            {
                Console.WriteLine("\nПісля перефарбування братів:");
                PrintRb(tree, tree.Root);
            }
            else
            {
                Console.WriteLine("Перефарбування неможливе (брати мають різні кольори або один з них Nil).");
            }
        }

        int? rotateValue = ReadInt("\nЛівий поворот навколо вузла зі значенням (Enter — пропустити): ");
        if (rotateValue.HasValue)
        {
            RbNode target = tree.Search(rotateValue.Value);
            if (target.IsNil)
            {
                Console.WriteLine("Вузол не знайдено.");
            }
            else
            {
                tree.LeftRotate(target);
                Console.WriteLine("\nПісля лівого повороту:");
                PrintRb(tree, tree.Root);
            }
        }

        int? rRotateValue = ReadInt("\nПравий поворот навколо вузла зі значенням (Enter — пропустити): ");
        if (rRotateValue.HasValue)
        {
            RbNode target = tree.Search(rRotateValue.Value);
            if (target.IsNil)
            {
                Console.WriteLine("Вузол не знайдено.");
            }
            else
            {
                tree.RightRotate(target);
                Console.WriteLine("\nПісля правого повороту:");
                PrintRb(tree, tree.Root);
            }
        }
    }

    static void DemoAvlTree()
    {
        Console.Clear();
        Console.WriteLine("АВЛ-дерево");
        var tree = new AvlTree();
        int[] keys = ReadIntArray("Введіть ключі для вставки (через пробіл): ");
        if (keys.Length == 0)
        {
            keys = new[] { 10, 20, 30, 40, 50, 25 };
            Console.WriteLine($"Використовую демо-набір: {string.Join(", ", keys)}");
        }
        foreach (int k in keys)
        {
            tree.Insert(k);
            Console.WriteLine($"\nПісля вставки {k}:");
            PrintAvl(tree.Root);
        }

        int? toDelete = ReadInt("\nВведіть значення для видалення (Enter — пропустити): ");
        if (toDelete.HasValue)
        {
            tree.Delete(toDelete.Value);
            Console.WriteLine($"\nПісля видалення {toDelete.Value}:");
            PrintAvl(tree.Root);
        }
    }

    static void DemoSplayTree()
    {
        Console.Clear();
        Console.WriteLine("Splay-дерево");
        var tree = new SplayTree();
        int[] keys = ReadIntArray("Введіть ключі для вставки (через пробіл): ");
        if (keys.Length == 0)
        {
            keys = new[] { 50, 30, 70, 20, 40, 60, 80 };
            Console.WriteLine($"Використовую демо-набір: {string.Join(", ", keys)}");
        }
        foreach (int k in keys)
        {
            tree.Insert(k);
        }
        Console.WriteLine("\nСтруктура Splay-дерева після вставок:");
        PrintSplay(tree.Root);

        int? searchValue = ReadInt("\nВведіть значення для пошуку (вузол піднімається у корінь): ");
        if (searchValue.HasValue)
        {
            var found = tree.Search(searchValue.Value);
            if (found != null)
            {
                Console.WriteLine($"\nЗнайдено {searchValue.Value}, корінь:");
            }
            else
            {
                Console.WriteLine($"\nВузол {searchValue.Value} не знайдено. Останній відвіданий стає коренем.");
            }
            PrintSplay(tree.Root);
        }

        int? toDelete = ReadInt("\nВведіть значення для видалення (Enter — пропустити): ");
        if (toDelete.HasValue)
        {
            tree.Delete(toDelete.Value);
            Console.WriteLine($"\nПісля видалення {toDelete.Value}:");
            PrintSplay(tree.Root);
        }
    }

    // Завдання 1 (теоретичне). Послідовна вставка 41,38,31,12,19,8 у RB-дерево.
    static void DemoRbInsertionSequence()
    {
        Console.Clear();
        Console.WriteLine("Послідовна вставка ключів 41, 38, 31, 12, 19, 8 у червоно-чорне дерево");
        var tree = new RedBlackTree();
        int[] keys = { 41, 38, 31, 12, 19, 8 };
        foreach (int k in keys)
        {
            tree.Insert(k);
            Console.WriteLine($"\nПісля вставки {k}:");
            PrintRb(tree, tree.Root);
        }
    }

    // ===================== Завдання 4. Дерево відрізків з max =====================
    class IntervalNode
    {
        public int Low;
        public int High;
        public int Max;
        public IntervalNode? Left;
        public IntervalNode? Right;
        public IntervalNode? Parent;

        public IntervalNode(int low, int high)
        {
            Low = low;
            High = high;
            Max = high;
        }
    }

    class IntervalTree
    {
        public IntervalNode? Root;

        // Оновлення max за O(1) при заданих змінах структури.
        private static void UpdateMax(IntervalNode node)
        {
            int max = node.High;
            if (node.Left != null && node.Left.Max > max)
            {
                max = node.Left.Max;
            }
            if (node.Right != null && node.Right.Max > max)
            {
                max = node.Right.Max;
            }
            node.Max = max;
        }

        // Лівий поворот, що оновлює атрибути max за O(1).
        public void LeftRotate(IntervalNode x)
        {
            IntervalNode? y = x.Right;
            if (y == null)
            {
                return;
            }
            x.Right = y.Left;
            if (y.Left != null)
            {
                y.Left.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == null)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }
            y.Left = x;
            x.Parent = y;
            UpdateMax(x);
            UpdateMax(y);
        }

        public void Insert(int low, int high)
        {
            IntervalNode newNode = new IntervalNode(low, high);
            if (Root == null)
            {
                Root = newNode;
                return;
            }
            IntervalNode current = Root;
            while (true)
            {
                if (newNode.Max > current.Max)
                {
                    current.Max = newNode.Max;
                }
                if (low < current.Low)
                {
                    if (current.Left == null)
                    {
                        current.Left = newNode;
                        newNode.Parent = current;
                        return;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = newNode;
                        newNode.Parent = current;
                        return;
                    }
                    current = current.Right;
                }
            }
        }

        // Пошук перетину з інтервалом [low, high] — стандартна операція.
        public IntervalNode? IntervalSearch(int low, int high)
        {
            IntervalNode? x = Root;
            while (x != null && !(x.Low <= high && low <= x.High))
            {
                if (x.Left != null && x.Left.Max >= low)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }
            return x;
        }
    }

    static void PrintIntervalTree(IntervalNode? node, string indent = "", bool last = true)
    {
        if (node == null)
        {
            return;
        }
        Console.Write(indent);
        if (last)
        {
            Console.Write("└── ");
            indent += "    ";
        }
        else
        {
            Console.Write("├── ");
            indent += "│   ";
        }
        Console.WriteLine($"[{node.Low}, {node.High}], max={node.Max}");
        PrintIntervalTree(node.Left, indent, node.Right == null);
        PrintIntervalTree(node.Right, indent, true);
    }

    static void DemoIntervalLeftRotate()
    {
        Console.Clear();
        Console.WriteLine("Завдання 4. LEFT-ROTATE для дерева відрізків з оновленням max за O(1)");
        var tree = new IntervalTree();
        (int Low, int High)[] intervals =
        {
            (16, 21),
            (8, 9),
            (25, 30),
            (5, 8),
            (15, 23),
            (17, 19),
            (26, 26),
            (0, 3),
            (6, 10),
            (19, 20),
        };
        foreach (var iv in intervals)
        {
            tree.Insert(iv.Low, iv.High);
        }
        Console.WriteLine("\nДерево відрізків до повороту:");
        PrintIntervalTree(tree.Root);
        if (tree.Root != null)
        {
            Console.WriteLine("\nВиконуємо лівий поворот навколо кореня...");
            tree.LeftRotate(tree.Root);
            Console.WriteLine("\nДерево відрізків після лівого повороту:");
            PrintIntervalTree(tree.Root);
        }
    }

    // ===================== Завдання 5. Перекриття прямокутників =====================
    struct Rectangle
    {
        public int X1;
        public int Y1;
        public int X2;
        public int Y2;

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            X1 = Math.Min(x1, x2);
            Y1 = Math.Min(y1, y2);
            X2 = Math.Max(x1, x2);
            Y2 = Math.Max(y1, y2);
        }
    }

    // Пошук пари перекриттю інтегральних схем — sweep line + інтервальне дерево по Y.
    static bool HasOverlap(List<Rectangle> rectangles, out Rectangle a, out Rectangle b)
    {
        a = default;
        b = default;
        var events = new List<(int X, int Type, Rectangle Rect)>();
        for (int i = 0; i < rectangles.Count; i++)
        {
            events.Add((rectangles[i].X1, 0, rectangles[i]));
            events.Add((rectangles[i].X2, 1, rectangles[i]));
        }
        events.Sort((p, q) => p.X != q.X ? p.X.CompareTo(q.X) : p.Type.CompareTo(q.Type));
        var active = new List<Rectangle>();
        foreach (var e in events)
        {
            if (e.Type == 0)
            {
                foreach (var r in active)
                {
                    if (r.Y1 <= e.Rect.Y2 && e.Rect.Y1 <= r.Y2)
                    {
                        a = r;
                        b = e.Rect;
                        return true;
                    }
                }
                active.Add(e.Rect);
            }
            else
            {
                active.RemoveAll(r =>
                    r.X1 == e.Rect.X1 && r.Y1 == e.Rect.Y1 && r.X2 == e.Rect.X2 && r.Y2 == e.Rect.Y2);
            }
        }
        return false;
    }

    static void DemoRectangleOverlap()
    {
        Console.Clear();
        Console.WriteLine("Завдання 5. Перевірка перекриття прямокутників на інтегральній схемі");
        Console.WriteLine("Введіть кількість прямокутників (Enter — використати демо-набір): ");
        string raw = Console.ReadLine() ?? "";
        List<Rectangle> rectangles = new List<Rectangle>();
        if (int.TryParse(raw.Trim(), out int n) && n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                Console.Write($"Прямокутник {i + 1} у форматі x1 y1 x2 y2: ");
                int[] parts = ReadIntArray("");
                if (parts.Length >= 4)
                {
                    rectangles.Add(new Rectangle(parts[0], parts[1], parts[2], parts[3]));
                }
                else
                {
                    Console.WriteLine("Некоректний ввід, прямокутник проігноровано.");
                    i--;
                }
            }
        }
        else
        {
            rectangles.Add(new Rectangle(0, 0, 5, 5));
            rectangles.Add(new Rectangle(6, 1, 9, 4));
            rectangles.Add(new Rectangle(2, 2, 4, 4));
            rectangles.Add(new Rectangle(10, 10, 12, 12));
            Console.WriteLine("\nДемо-набір прямокутників:");
            for (int i = 0; i < rectangles.Count; i++)
            {
                Console.WriteLine(
                    $"  {i + 1}. ({rectangles[i].X1}, {rectangles[i].Y1}) — ({rectangles[i].X2}, {rectangles[i].Y2})");
            }
        }
        bool overlap = HasOverlap(rectangles, out Rectangle a, out Rectangle b);
        if (overlap)
        {
            Console.WriteLine("\nЗнайдено пару прямокутників, що перекриваються:");
            Console.WriteLine($"  A: ({a.X1}, {a.Y1}) — ({a.X2}, {a.Y2})");
            Console.WriteLine($"  B: ({b.X1}, {b.Y1}) — ({b.X2}, {b.Y2})");
        }
        else
        {
            Console.WriteLine("\nЖодна пара прямокутників не перекривається.");
        }
    }

    // ===================== Завдання 5 (теор). Перестановка Йосипа за O(n) =====================
    // Якщо m фіксоване, то для кожного n можна обчислити (n,m)-перестановку за O(n)
    // ітеративним моделюванням з допомогою кільцевого списку (доступ за постійний час).
    static int[] JosephusPermutation(int n, int m)
    {
        var list = new LinkedList<int>();
        for (int i = 1; i <= n; i++)
        {
            list.AddLast(i);
        }
        var result = new int[n];
        var current = list.First!;
        int idx = 0;
        while (list.Count > 0)
        {
            for (int i = 1; i < m; i++)
            {
                current = current.Next ?? list.First!;
            }
            result[idx++] = current.Value;
            var next = current.Next ?? list.First;
            list.Remove(current);
            current = next ?? list.First!;
        }
        return result;
    }

    static void DemoJosephus()
    {
        Console.Clear();
        Console.WriteLine("Завдання 5 (теоретичне). Перестановка Йосипа за O(n)");
        int? n = ReadInt("Введіть n: ");
        int? m = ReadInt("Введіть m: ");
        if (!n.HasValue || !m.HasValue || n.Value <= 0 || m.Value <= 0)
        {
            Console.WriteLine("Очікую n > 0 та 0 < m < n. Використовую n=7, m=3.");
            n = 7;
            m = 3;
        }
        int[] result = JosephusPermutation(n.Value, m.Value);
        Console.WriteLine($"\n({n}, {m})-перестановка Йосипа:");
        Console.WriteLine(string.Join(" ", result));
    }

    // ===================== Меню =====================
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine("Лабораторна робота №6. Алгоритми роботи із деревами.");
            Console.WriteLine("Меню:");
            Console.WriteLine("1. BST: вставка, видалення, повороти");
            Console.WriteLine("2. Червоно-чорне дерево: вставка, видалення, перефарбування братів, повороти");
            Console.WriteLine("3. АВЛ-дерево: вставка, видалення");
            Console.WriteLine("4. Splay-дерево: вставка, пошук (з підняттям у корінь), видалення");
            Console.WriteLine("5. Завдання 1. Послідовна вставка 41,38,31,12,19,8 у RB-дерево");
            Console.WriteLine("6. Завдання 4. LEFT-ROTATE для дерева відрізків (оновлення max за O(1))");
            Console.WriteLine("7. Завдання 5. Перекриття прямокутників на інтегральній схемі");
            Console.WriteLine("8. Завдання 5 (теор). (n, m)-перестановка Йосипа за O(n)");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    DemoBinarySearchTree();
                    break;
                case "2":
                    DemoRedBlackTree();
                    break;
                case "3":
                    DemoAvlTree();
                    break;
                case "4":
                    DemoSplayTree();
                    break;
                case "5":
                    DemoRbInsertionSequence();
                    break;
                case "6":
                    DemoIntervalLeftRotate();
                    break;
                case "7":
                    DemoRectangleOverlap();
                    break;
                case "8":
                    DemoJosephus();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
            if (choice != "0")
            {
                Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
                if (Console.IsInputRedirected)
                {
                    Console.ReadLine();
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }
        while (choice != "0");
    }
}
