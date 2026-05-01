using System;

class Program
{
    class Node
    {
        public int Value;
        public Node Left;
        public Node Right;
        public Node Parent;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    static void Preorder(Node root)
    {
        if (root == null)
        {
            return;
        }
        else
        {
            Console.Write(root.Value + " "); 
            Preorder(root.Left);             
            Preorder(root.Right);            
        }
    }


    static void Postorder(Node root)
    {
        if (root == null)
        {
            return;
        }
        else
        {
            Postorder(root.Left);             
            Postorder(root.Right);            
            Console.Write(root.Value + " ");  
        }
    }

        // Нерекурсивний симетричний обхід (in-order) з використанням стека
        static void InOrderTraversal(Node root)
        {
            var stack = new System.Collections.Generic.Stack<Node>();
            Node current = root;
            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                Console.Write(current.Value + " ");
                current = current.Right;
            }
        }

    static void MorrisInOrderTraversal(Node root)
    {
        Node current = root;
        while (current != null)
        {
            if (current.Left == null)
            {
                Console.Write(current.Value + " ");
                current = current.Right;
            }
            else
            {
                Node predecessor = current.Left;
                while (predecessor.Right != null && predecessor.Right != current)
                {
                    predecessor = predecessor.Right;
                }

                if (predecessor.Right == null)
                {
                    predecessor.Right = current;
                    current = current.Left;
                }
                else
                {
                    predecessor.Right = null;
                    Console.Write(current.Value + " ");
                    current = current.Right;
                }
            }
        }
    }

    static Node TreeMinimum(Node root)
    {
        if (root == null)
        {
            return null;
        }
        else
        {
            if (root.Left == null)
            {
                return root;
            }
            else
            {
                return TreeMinimum(root.Left);
            }
        }
    }

    static Node TreeMaximum(Node root)
    {
        if (root == null)
        {
            return null;
        }
        else
        {
            if (root.Right == null)
            {
                return root;
            }
            else
            {
                return TreeMaximum(root.Right);
            }
        }
    }

    static Node TreePredecessor(Node x)
    {
        if (x == null)
        {
            return null;
        }
        else
        {
            if (x.Left != null)
            {
                return TreeMaximum(x.Left);
            }
            else
            {
                Node y = x.Parent;
                while (y != null && x == y.Left)
                {
                    x = y;
                    y = y.Parent;
                }
                return y;
            }
        }
    }

    static Node FindNode(Node root, int value)
    {
        if (root == null)
        {
            return null;
        }
        else
        {
            if (root.Value == value)
            {
                return root;
            }
            else
            {
                Node leftSearch = FindNode(root.Left, value);
                if (leftSearch != null)
                {
                    return leftSearch;
                }
                else
                {
                    return FindNode(root.Right, value);
                }
            }
        }
    }

    static Node RecursiveTreeInsert(Node root, Node newNode)
    {
        if (root == null)
        {
            return newNode;
        }

        if (newNode.Value < root.Value)
        {
            if (root.Left == null)
            {
                root.Left = newNode;
                newNode.Parent = root;
            }
            else
            {
                RecursiveTreeInsert(root.Left, newNode);
            }
        }
        else
        {
            if (root.Right == null)
            {
                root.Right = newNode;
                newNode.Parent = root;
            }
            else
            {
                RecursiveTreeInsert(root.Right, newNode);
            }
        }

        return root;
    }

    static void PrintTree()
    {
        Console.WriteLine("       1");
        Console.WriteLine("      / \\");
        Console.WriteLine("     2   3");
        Console.WriteLine("    / \\   \\");
        Console.WriteLine("   4   5   6");
        Console.WriteLine("      / \\");
        Console.WriteLine("     7   8");
    }



    //        1
    //       / \
    //      2   3
    //     / \   \
    //    4   5   6
    //       / \
    //      7   8
    static Node BuildDemoTree()
    {
        Node root = new Node(1);

        root.Left = new Node(2);
        root.Left.Parent = root;
        root.Right = new Node(3);
        root.Right.Parent = root;

        root.Left.Left = new Node(4);
        root.Left.Left.Parent = root.Left;
        root.Left.Right = new Node(5);
        root.Left.Right.Parent = root.Left;

        root.Right.Right = new Node(6);
        root.Right.Right.Parent = root.Right;

        root.Left.Right.Left = new Node(7);
        root.Left.Right.Left.Parent = root.Left.Right;
        root.Left.Right.Right = new Node(8);
        root.Left.Right.Right.Parent = root.Left.Right;

        return root;
    }

    static void RunPreorderDemo()
    {
        Console.WriteLine("\nПрямий обхід дерева");

        Node root = BuildDemoTree();

        Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
        PrintTree();

        Console.Write("\nПрямий обхід ");
        Preorder(root);

    }

    static void RunPostorderDemo()
    {
        Console.WriteLine("\nЗворотний обхід дерева");

        Node root = BuildDemoTree();

        Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
        PrintTree();

        Console.Write("\nЗворотний обхід : ");
        Postorder(root);

    }

        static void RunInOrderDemo()
        {
            Console.WriteLine("\nСиметричний (in-order) обхід дерева");

            Node root = BuildDemoTree();

            Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
            PrintTree();

            Console.Write("\nСиметричний обхід : ");
            InOrderTraversal(root);
        }

    static void RunMorrisInOrderDemo()
    {
        Console.WriteLine("\nСиметричний (in-order) обхід дерева (без стека, алгоритм Морріса)");
        
        Node root = BuildDemoTree();
        
        Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
        PrintTree();
        
        Console.Write("\nСиметричний обхід: ");
        MorrisInOrderTraversal(root);
    }

    static void RunTreeMinMaxDemo()
    {
        Console.WriteLine("\nПошук крайніх лівого та правого вузлів (Tree-Minimum та Tree-Maximum)");
        
        Node root = BuildDemoTree();
        
        Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
        PrintTree();
        
        Node min = TreeMinimum(root);
        Node max = TreeMaximum(root);
        
        Console.WriteLine($"\nКрайній лівий вузол (мінімум): {(min != null ? min.Value.ToString() : "дерево порожнє")}");
        Console.WriteLine($"Крайній правий вузол (максимум): {(max != null ? max.Value.ToString() : "дерево порожнє")}");
    }

    static void RunTreePredecessorDemo()
    {
        Console.WriteLine("\nПошук попередника вузла (TREE-PREDECESSOR)");
        
        Node root = BuildDemoTree();
        
        Console.WriteLine("\nСтруктура дерева (n = 8 вузлів):");
        PrintTree();
        
        Console.Write("\nВведіть значення вузла: ");
        if (int.TryParse(Console.ReadLine(), out int val))
        {
            Node target = FindNode(root, val);
            if (target == null)
            {
                Console.WriteLine("Вузол з таким значенням не знайдено в дереві.");
            }
            else
            {
                Node predecessor = TreePredecessor(target);
                if (predecessor == null)
                {
                    Console.WriteLine("Попередника не існує (вузол є першим при симетричному обході).");
                }
                else
                {
                    Console.WriteLine($"Попередник вузла {val} — це вузол зі значенням {predecessor.Value}");
                }
            }
        }
        else
        {
            Console.WriteLine("Некоректне значення.");
        }
    }

    static void RunRecursiveTreeInsertDemo()
    {
        Console.WriteLine("\nВставка вузла в дерево (рекурсивно)");
        
        Node root = BuildDemoTree();
        
        Console.WriteLine("\nСтруктура початкового дерева (якщо вузол не існує, він буде доданий за правилами бінарного дерева пошуку):");
        PrintTree();
        
        Console.Write("\nВведіть значення нового вузла для вставки: ");
        if (int.TryParse(Console.ReadLine(), out int val))
        {
            Node newNode = new Node(val);
            root = RecursiveTreeInsert(root, newNode);
            Console.WriteLine($"\nВузол {val} успішно вставлено!");
            
            Console.Write("Симетричний обхід оновленого дерева (має бути відсортованим): ");
            InOrderTraversal(root);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Некоректне значення.");
        }
    }

    static void Main()
    {
        Console.Clear();

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine(" Меню ");
            Console.WriteLine("1. Прямий обхід дерева");
            Console.WriteLine("2. Зворотний обхід дерева");
            Console.WriteLine("3. Симетричний (in-order) обхід дерева (зі стеком)");
            Console.WriteLine("4. Симетричний обхід дерева (без стека, алгоритм Морріса)");
            Console.WriteLine("5. Пошук вузлів (Tree-Minimum та Tree-Maximum) - рекурсивно");
            Console.WriteLine("6. Знайти попередника для заданого вузла (TREE-PREDECESSOR)");
            Console.WriteLine("7. Вставка вузла в дерево (рекурсивно)");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RunPreorderDemo();
                    break;
                case "2":
                    RunPostorderDemo();
                    break;
                case "3":
                    RunInOrderDemo();
                    break;
                case "4":
                    RunMorrisInOrderDemo();
                    break;
                case "5":
                    RunTreeMinMaxDemo();
                    break;
                case "6":
                    RunTreePredecessorDemo();
                    break;
                case "7":
                    RunRecursiveTreeInsertDemo();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
            if (choice != "0")
            {
                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                Console.ReadKey();
            }
            else
            {
                // Завершення програми
            }
        }
        while (choice != "0");
    }
}