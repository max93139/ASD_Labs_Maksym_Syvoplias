 using System;
using System.Collections.Generic;

class Program
{
    class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }

    static Node head = null;
    static Node tail = null;

    static void Enqueue(int x)
    {
        Node newNode = new Node(x);
        if (tail == null)
        {
            head = tail = newNode;
            return;
        }
        tail.Next = newNode;
        tail = newNode;
    }

    static int Dequeue()
    {
        if (head == null)
        {
            throw new Exception("Queue Underflow");
        }

        int x = head.Data;
        head = head.Next;

        if (head == null)
        {
            tail = null;
        }

        return x;
    }

    static bool IsEmpty()
    {
        return head == null;
    }

    static void RunLinkedListQueueDemo()
    {
        Console.WriteLine("\n--- Демонстрація черги на лінійному зв'язаному списку ---");

        // Очищаємо чергу перед кожним демо, щоб вона була порожньою
        head = null;
        tail = null;

        Console.WriteLine("Додаємо елементи 10, 20, 30...");
        Enqueue(10);
        Enqueue(20);
        Enqueue(30);

        Console.WriteLine("Вилучаємо елементи:");
        try
        {
            while (!IsEmpty())
            {
                Console.WriteLine($"Вилучено: {Dequeue()}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static int[] deque;
    static int dqHead = 0;
    static int dqTail = 0;
    static int dqSize;

    static bool IsDequeFull()
    {
        return (dqTail + 1) % dqSize == dqHead;
    }

    static bool IsDequeEmpty()
    {
        return dqHead == dqTail;
    }

    static void InsertFront(int x)
    {
        if (IsDequeFull())
        {
            Console.WriteLine("Помилка: Переповнення (Overflow) у деку");
            return;
        }
        dqHead = (dqHead - 1 + dqSize) % dqSize;
        deque[dqHead] = x;
    }

    static void InsertRear(int x)
    {
        if (IsDequeFull())
        {
            Console.WriteLine("Помилка: Переповнення (Overflow) у деку");
            return;
        }
        deque[dqTail] = x;
        dqTail = (dqTail + 1) % dqSize;
    }

    static int DeleteFront()
    {
        if (IsDequeEmpty())
        {
            throw new Exception("Помилка: Спустошення (Underflow) у деку");
        }
        int x = deque[dqHead];
        dqHead = (dqHead + 1) % dqSize;
        return x;
    }

    static int DeleteRear()
    {
        if (IsDequeEmpty())
        {
            throw new Exception("Помилка: Спустошення (Underflow) у деку");
        }
        dqTail = (dqTail - 1 + dqSize) % dqSize;
        return deque[dqTail];
    }

    static void RunDequeDemo()
    {
        Console.WriteLine("\n--- Демонстрація дека на базі циклічного масиву ---");
        
        // Зверніть увагу: при такій логіці завжди 1 комірка залишається вільною
        // щоб відрізнити "порожній" стан від "повного". Тому для 5 елементів беремо розмір 6.
        dqSize = 6; 
        deque = new int[dqSize];
        dqHead = 0;
        dqTail = 0;

        Console.WriteLine("Вставка в кінець (InsertRear): 10, 20...");
        InsertRear(10);
        InsertRear(20);

        Console.WriteLine("Вставка на початок (InsertFront): 5, 1...");
        InsertFront(5);
        InsertFront(1);

        Console.WriteLine("Видаляємо з початку (DeleteFront):");
        try { Console.WriteLine($"Вилучено: {DeleteFront()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
        
        Console.WriteLine("Видаляємо з кінця (DeleteRear):");
        try { Console.WriteLine($"Вилучено: {DeleteRear()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    static Stack<int> stackPush = new Stack<int>(); // Перший стек (використовується для додавання)
    static Stack<int> stackPop = new Stack<int>();  // Другий стек (використовується для вилучення)

    // Операція вставки
    static void EnqueueTwoStacks(int x)
    {
        stackPush.Push(x);
    }

    // Операція вилучення
    static int DequeueTwoStacks()
    {
        // Якщо стек для вилучення вже порожній, маємо перекинути всі елементи з першого стеку
        if (stackPop.Count == 0)
        {
            while (stackPush.Count != 0) // Поки перший стек не спорожніє
            {
                int buffer = stackPush.Pop();
                stackPop.Push(buffer); // Пересипаємо елементи, розвертаючи їхній порядок
            }
        }

        // Перевіряємо ще раз. Якщо й після пересипання він порожній — значить черга взагалі пуста.
        if (stackPop.Count == 0)
        {
            Console.WriteLine("Помилка: Черга порожня! Вилучення неможливе.");
            return -1;
        }

        // Повертаємо останній (найстаріший) елемент
        return stackPop.Pop();
    }

    static void RunTwoStacksQueueDemo()
    {
        Console.WriteLine("\n--- Демонстрація черги на двох стеках ---");
        
        // Очищаємо стеки
        stackPush.Clear();
        stackPop.Clear();

        Console.WriteLine("Додаємо елементи (Enqueue): 100, 200, 300...");
        EnqueueTwoStacks(100);
        EnqueueTwoStacks(200);
        EnqueueTwoStacks(300);

        Console.WriteLine("Вилучаємо елементи (Dequeue):");
        try { Console.WriteLine($"Вилучено: {DequeueTwoStacks()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
        try { Console.WriteLine($"Вилучено: {DequeueTwoStacks()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
        
        Console.WriteLine("Додаємо новий елемент (Enqueue): 400...");
        EnqueueTwoStacks(400);
        
        Console.WriteLine("Довилучаємо інші елементи (Dequeue):");
        try { Console.WriteLine($"Вилучено: {DequeueTwoStacks()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
        try { Console.WriteLine($"Вилучено: {DequeueTwoStacks()}"); } catch (Exception ex) { Console.WriteLine(ex.Message); }
    }

    static Queue<int> mainQueue = new Queue<int>();   // Основна черга, де завжди нові елементи спереду
    static Queue<int> helperQueue = new Queue<int>(); // Допоміжна черга для перекидання

    // Додавання елемента
    static void PushToTwoQueuesStack(int x)
    {
        // Додаємо новий елемент до порожньої допоміжної черги
        helperQueue.Enqueue(x);

        // Перекидаємо всі існуючі елементи з основної черги за щойно доданим елементом
        while (mainQueue.Count != 0) 
        {
            int oldItem = mainQueue.Dequeue();
            helperQueue.Enqueue(oldItem);
        }

        // Міняємо черги місцями
        Queue<int> tempReference = mainQueue;
        mainQueue = helperQueue;
        helperQueue = tempReference;
    }

    // Вилучення елемента
    static int PopFromTwoQueuesStack()
    {
        // Перевіряємо на випадок порожнього стеку
        if (mainQueue.Count == 0)
        {
            Console.WriteLine("Помилка: Стек порожній! Вилучення неможливе.");
            return -1;
        }

        // Тут достатньо просто взяти перший елемент
        return mainQueue.Dequeue();
    }

    static void RunTwoQueuesStackDemo()
    {
        Console.WriteLine("\n--- Демонстрація стеку на двох чергах ---");
        
        mainQueue.Clear();
        helperQueue.Clear();

        Console.WriteLine("Додаємо елементи (Push): 10, 20, 30...");
        PushToTwoQueuesStack(10);
        PushToTwoQueuesStack(20);
        PushToTwoQueuesStack(30);

        Console.WriteLine("Вилучаємо елементи (Pop):");
        Console.WriteLine($"Вилучено: {PopFromTwoQueuesStack()}");
        Console.WriteLine($"Вилучено: {PopFromTwoQueuesStack()}");

        Console.WriteLine("Додаємо новий елемент (Push): 40...");
        PushToTwoQueuesStack(40);

        Console.WriteLine("Вилучаємо все до кінця:");
        Console.WriteLine($"Вилучено: {PopFromTwoQueuesStack()}");
        Console.WriteLine($"Вилучено: {PopFromTwoQueuesStack()}");
        Console.WriteLine($"Вилучено: {PopFromTwoQueuesStack()}"); // тут має спрацювати помилка
    }

    // Реалізація Стеку за допомогою лінійного зв'язаного списку
    static Node stackTopNode = null;

    static void PushToLinkedStack(int x)
    {
        Node element = new Node(x);
        
        // Вказуємо на поточну верхівку стеку
        element.Next = stackTopNode;
        
        // Верхівка стеку - доданий елемент
        stackTopNode = element;
    }

    static int PopFromLinkedStack()
    {
        // Перевірка спустошення
        if (stackTopNode == null)
        {
            Console.WriteLine("Помилка: Стек порожній. Немає що вилучати.");
            return -1;
        }

        // Беремо дані з верхівки
        int result = stackTopNode.Data;
        
        // Верхівкою тепер стає наступний елемент 
        stackTopNode = stackTopNode.Next;
        
        return result;
    }

    static void RunLinkedStackDemo()
    {
        Console.WriteLine("\n--- Демонстрація стеку на зв'язаному списку (O(1)) ---");
        
        // Очищаємо стек
        stackTopNode = null;

        Console.WriteLine("Додаємо (Push): 55, 66, 77...");
        PushToLinkedStack(55);
        PushToLinkedStack(66);
        PushToLinkedStack(77);

        Console.WriteLine("Вилучаємо (Pop):");
        Console.WriteLine($"Вилучено: {PopFromLinkedStack()}");
        Console.WriteLine($"Вилучено: {PopFromLinkedStack()}");

        Console.WriteLine("Додаємо (Push): 88...");
        PushToLinkedStack(88);

        Console.WriteLine("Вилучаємо все решта:");
        Console.WriteLine($"Вилучено: {PopFromLinkedStack()}");
        Console.WriteLine($"Вилучено: {PopFromLinkedStack()}");
        
        Console.Write("Спроба вилучити зайве: "); 
        PopFromLinkedStack(); // має спрацювати перевірка на null
    }

    // Реалізація Черги за допомогою лінійного зв'язаного списку 
    static Node queueFrontNode = null; // Звідки вилучаємо елементи
    static Node queueBackNode = null;  // Куди додаємо елементи

    static void EnqueueToLinkedList(int x)
    {
        Node newItem = new Node(x);
        
        // Якщо черга наразі порожня, новий елемент стає і початком, і кінцем
        if (queueBackNode == null)
        {
            queueFrontNode = newItem;
            queueBackNode = newItem;
            return;
        }
        
        // Якщо елементи вже стоять у черзі, ставимо новий елемент позаду
        queueBackNode.Next = newItem;
        queueBackNode = newItem;
    }

    static int DequeueFromLinkedList()
    {
        if (queueFrontNode == null)
        {
            Console.WriteLine("Увага: Черга порожня! Вилучення виконати неможливо.");
            return -1;
        }

        // Беремо дані з переднього вузла
        int extractedValue = queueFrontNode.Data;
        
        // Вилучаємо елемент з черги: наступний елемент стає першим
        queueFrontNode = queueFrontNode.Next;

        // Якщо черга повністю спорожніла - треба затерти і кінець також
        if (queueFrontNode == null)
        {
            queueBackNode = null;
        }

        return extractedValue;
    }

    static void RunLinkedQueueTheoryDemo()
    {
        Console.WriteLine("\n--- Демонстрація черги на зв'язаному списку (Теорія O(1)) ---");
        
        // Очищаємо чергу
        queueFrontNode = null;
        queueBackNode = null;

        Console.WriteLine("Додаємо (ENQUEUE): 11, 22, 33...");
        EnqueueToLinkedList(11);
        EnqueueToLinkedList(22);
        EnqueueToLinkedList(33);

        Console.WriteLine("Вилучаємо (DEQUEUE):");
        Console.WriteLine($"Вилучено: {DequeueFromLinkedList()}");
        Console.WriteLine($"Вилучено: {DequeueFromLinkedList()}");

        Console.WriteLine("Додаємо (ENQUEUE): 44...");
        EnqueueToLinkedList(44);

        Console.WriteLine("Вилучаємо все до кінця:");
        Console.WriteLine($"Вилучено: {DequeueFromLinkedList()}");
        Console.WriteLine($"Вилучено: {DequeueFromLinkedList()}");
        
        Console.Write("Спроба вилучити зайве: "); 
        DequeueFromLinkedList(); // перевірка на null
    }

    // Практичне Завдання 1 
    static void RunBalancedParenthesesDemo()
    {
        Console.WriteLine("\nПрактичне Завдання 1: Правильність дужкової послідовності");
        Console.WriteLine("Введіть рядок, що складається лише з круглих дужок '(' та ')':");
        string input = Console.ReadLine() ?? "";


        Stack<char> bracketsStack = new Stack<char>();
        bool isValid = true;

        foreach (char c in input)
        {
            if (c == '(')
            {
                bracketsStack.Push(c); // Відкрита дужка - кладемо в стек
            }
            else if (c == ')')
            {
                // Якщо стек порожній, значить закрита дужка не має пари - це помилка
                if (bracketsStack.Count == 0)
                {
                    isValid = false;
                    break;
                }
                
                bracketsStack.Pop(); // Закрита дужка знайшла пару - виймаємо відповідну відкриту зі стеку
            }
        }

        // В кінці стек має бути повністю порожнім
        if (bracketsStack.Count != 0)
        {
            isValid = false;
        }

        if (input.Length == 0)
        {
            Console.WriteLine("Рядок порожній.");
        }
        else if (isValid)
        {
            Console.WriteLine("Результат: Валідна");
        }
        else
        {
            Console.WriteLine("Результат: Невалідна");
        }
    }

    // Практичне Завдання 2 
    static Queue<int> MergeSortedQueues(Queue<int> q1, Queue<int> q2)
    {
        Queue<int> resultQueue = new Queue<int>();

        // Проходимо по обох чергах, поки в обох є елементи
        while (q1.Count > 0 && q2.Count > 0)
        {
            // Беремо елементи з обох черг і порівнюємо їх
            if (q1.Peek() <= q2.Peek())
            {
                // Якщо елемент з першої черги менший або рівний, вилучаємо його і додаємо до результату
                resultQueue.Enqueue(q1.Dequeue());
            }
            else
            {
                // Інакше беремо з другої черги
                resultQueue.Enqueue(q2.Dequeue());
            }
        }

        // Якщо в першій черзі ще залишилися елементи , додаємо їх усі
        while (q1.Count > 0)
        {
            resultQueue.Enqueue(q1.Dequeue());
        }

        // Аналогічно для другої черги
        while (q2.Count > 0)
        {
            resultQueue.Enqueue(q2.Dequeue());
        }

        return resultQueue;
    }

    static void RunMergeSortedQueuesDemo()
    {
        Console.WriteLine("\n Практичне Завдання 2: Об'єднання відсортованих черг");
        
        Queue<int> queue1 = new Queue<int>();
        queue1.Enqueue(1);
        queue1.Enqueue(5);
        queue1.Enqueue(9);
        queue1.Enqueue(15);
        
        Queue<int> queue2 = new Queue<int>();
        queue2.Enqueue(2);
        queue2.Enqueue(3);
        queue2.Enqueue(8);
        queue2.Enqueue(20);
        queue2.Enqueue(25);

        Console.Write("Черга 1: ");
        Console.WriteLine(string.Join(", ", queue1));
        
        Console.Write("Черга 2: ");
        Console.WriteLine(string.Join(", ", queue2));

        Queue<int> mergedQueue = MergeSortedQueues(queue1, queue2);

        Console.Write("Результат (Об'єднана черга): ");
        Console.WriteLine(string.Join(", ", mergedQueue));
    }

    // --- Практичне Завдання 3 ---
    static void RunBuildingVisibilityDemo()
    {
        Console.WriteLine("\n Практичне Завдання 3: Визначення видимості споруд");
        
        Console.WriteLine("Введіть розмір масиву n:");
        string inputN = Console.ReadLine() ?? "";
        if (!int.TryParse(inputN, out int n) || n <= 0)
        {
            Console.WriteLine("Помилка: не вдалося розпізнати число n, або воно менше рівне нулю.");
            return;
        }

        Console.WriteLine($"Введіть {n} висот споруд через пробіл (на одному рядку):");
        Console.WriteLine("Приклад: 8 2 3 11 11 10");
        string inputArr = Console.ReadLine() ?? "";

        string[] parts = inputArr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < n)
        {
            Console.WriteLine($"Увага: вказано n={n}, але введено менше елементів. Будуть оброблені лише наявні.");
            n = parts.Length; 
        }

        Stack<int> visibleStack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            if (int.TryParse(parts[i], out int currentHeight))
            {
                
                if (visibleStack.Count == 0 || currentHeight > visibleStack.Peek())
                {
                    visibleStack.Push(currentHeight);
                }
            }
        }

        int[] visibleArr = visibleStack.ToArray();
        Array.Reverse(visibleArr);

        Console.WriteLine($"\nКількість споруд, які можна побачити: {visibleStack.Count}");
        Console.WriteLine($"Висоти цих споруд: {string.Join(" ", visibleArr)}");
    }

    class SetOfStacks
    {
        private List<Stack<int>> stacks = new List<Stack<int>>();
        private int capacity;

        public SetOfStacks(int capacity)
        {
            if (capacity <= 0) throw new ArgumentException("Увага: Місткість стопки має бути більше 0.");
            this.capacity = capacity;
        }

        public void Push(int val)
        {
            // Якщо стеків ще немає, або поточний стек вже повністю заповнений - створюємо нову стопку
            if (stacks.Count == 0 || stacks[stacks.Count - 1].Count >= capacity)
            {
                Stack<int> newStack = new Stack<int>();
                newStack.Push(val);
                stacks.Add(newStack);
            }
            else
            {
                stacks[stacks.Count - 1].Push(val);
            }
        }

        public int Pop()
        {
            if (stacks.Count == 0)
            {
                Console.WriteLine("Помилка: Усі стопки порожні. Немає що вилучати.");
                return -1;
            }

            // Звертаємося до останньої активної стопки
            Stack<int> lastStack = stacks[stacks.Count - 1];
            int val = lastStack.Pop();

            // Якщо після вилучення ця остання стопка стала порожньою - прибираємо її цілком
            if (lastStack.Count == 0)
            {
                stacks.RemoveAt(stacks.Count - 1);
            }

            return val;
        }

        public int GetStacksCount()
        {
            return stacks.Count;
        }
    }

    static void RunSetOfStacksDemo()
    {
        Console.WriteLine("\n Практичне Завдання 4: Стопка тарілок (SetOfStacks)");
        
        Console.Write("Введіть максимальну місткість однієї стопки (наприклад, 3): ");
        if (!int.TryParse(Console.ReadLine(), out int cap) || cap <= 0)
        {
            Console.WriteLine("Некоректний ввід. Створено стопку з лімітом за замовчуванням: 3");
            cap = 3;
        }

        SetOfStacks setOfStacks = new SetOfStacks(cap);
        int plateCounter = 1; // Автоматичний лічильник тарілок

        string choice;
        do
        {
            Console.WriteLine($"\n SetOfStacks (Місткість={cap}, Всього стопок={setOfStacks.GetStacksCount()})");
            Console.WriteLine("1. Push (Покласти нову тарілку)");
            Console.WriteLine("2. Pop (Зняти верхню тарілку)");
            Console.WriteLine("0. Повернутися до Головного меню");
            Console.Write("Ваш вибір: ");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    setOfStacks.Push(plateCounter);
                    Console.WriteLine($"[OK] Тарілка №{plateCounter} покладена в стопку. Поточна кількість стопок: {setOfStacks.GetStacksCount()}");
                    plateCounter++;
                    break;
                case "2":
                    int popped = setOfStacks.Pop();
                    if (popped != -1)
                    {
                        Console.WriteLine($"[OK] Знято тарілку №{popped}. Залишилось стопок: {setOfStacks.GetStacksCount()}");
                    }
                    break;
                case "0":
                    Console.WriteLine("Вихід з демо SetOfStacks...");
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        } while (choice != "0");
    }

    static void Main()
    {
        Console.Clear();

        string choice;
        do
        {
            Console.Clear();
            Console.WriteLine(" Меню ");
            Console.WriteLine("1. Операції ENQUEUE і DEQUEUE (Зв'язаний список)");
            Console.WriteLine("2. Операції двосторонньої черги/дека (Масив)");
            Console.WriteLine("3. Операції черги на двох стеках (відповідь на теорію)");
            Console.WriteLine("4. Операції стеку на двох чергах (відповідь на теорію)");
            Console.WriteLine("5. Операції стеку на зв'язаному списку (відповідь на теорію)");
            Console.WriteLine("6. Операції черги на зв'язаному списку (відповідь на теорію)");
            Console.WriteLine("7. Практичне Завдання 1 (Баланс дужок)");
            Console.WriteLine("8. Практичне Завдання 2 (Об'єднання відсортованих черг)");
            Console.WriteLine("9. Практичне Завдання 3 (Видимість споруд)");
            Console.WriteLine("10. Практичне Завдання 4 (SetOfStacks)");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RunLinkedListQueueDemo();
                    break;
                case "2":
                    RunDequeDemo();
                    break;
                case "3":
                    RunTwoStacksQueueDemo();
                    break;
                case "4":
                    RunTwoQueuesStackDemo();
                    break;
                case "5":
                    RunLinkedStackDemo();
                    break;
                case "6":
                    RunLinkedQueueTheoryDemo();
                    break;
                case "7":
                    RunBalancedParenthesesDemo();
                    break;
                case "8":
                    RunMergeSortedQueuesDemo();
                    break;
                case "9":
                    RunBuildingVisibilityDemo();
                    break;
                case "10":
                    RunSetOfStacksDemo();
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
        }
        while (choice != "0");
    }
}