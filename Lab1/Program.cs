using System;
using System.Diagnostics;

class Program
{
    static Random rand = new Random();   

    static void Main()
    {
        string choice;
        int size, x;
        
        Console.Clear();
        ReadInput(out size, out x);
        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
        Console.ReadLine();

        do
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Пошук перебором:");
            Console.WriteLine("2. Пошук з бар'єром:");
            Console.WriteLine("3. Бінарний пошук:");
            Console.WriteLine("4. Бінарний пошук за правилом золотого перерізу:");
            Console.WriteLine("0. Вихід");

            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RunTest(LinearSearch, size, x ,false, "Linear");
                    break;

                case "2":
                    RunTest(BarrierSearch, size, x, false, "Barrier");
                    break;

                case "3":
                    RunTest(BinarySearch, size, x, true, "Binary");
                    break;

                case "4":
                    RunTest(GoldenRatioBinarySearch, size, x, true, "Golden");
                    break;

                case "0":
                    Console.WriteLine("Завершення програми...");
                     Console.Clear();
                    break;

                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            if (choice != "0")
            {
                Console.WriteLine("\nНатисніть Enter, щоб повернутися до меню...");
                Console.ReadLine();
            }

        } while (choice != "0");
    }


    static void RunTest(Func<int[], int, int> searchMethod,
                        int size,
                        int x,
                        bool needsSorting,
                        string name)
    {
        int runs = 10;
        double totalTime = 0;
        int[] results = new int[runs];

        for (int i = 0; i < runs; i++)
        {
            int[] newArray = GenerateArray(size);
            int randomX = x;

            int[] arr = newArray;

            if (needsSorting)
            {
                arr = (int[])newArray.Clone();
                Array.Sort(arr);
            }

            Stopwatch sw = Stopwatch.StartNew();
            results[i] = searchMethod(arr, randomX);
            sw.Stop();

            totalTime += sw.Elapsed.TotalMilliseconds;
        }

        Console.WriteLine($"\n{name} Search:");
        Console.WriteLine($"Середній час: {totalTime / runs:F6} мс");

        Console.WriteLine("Індекси знайдених елементів:");
        for (int i = 0; i < runs; i++)
            Console.Write(results[i] + " ");

        Console.WriteLine();
    }

    static void ReadInput(out int size, out int x)
    {
        Console.Write("Введіть розмір масиву: ");
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            Console.Write("Некоректне число. Спробуйте ще раз: ");

        Console.Write("Введіть число для пошуку : ");
        while (!int.TryParse(Console.ReadLine(), out x))
            Console.Write("Некоректне число. Спробуйте ще раз: ");
    }

    static int[] GenerateArray(int size)
    {
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
            array[i] = rand.Next(0, 10000);

        return array;
    }

    static int LinearSearch(int[] array, int x)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == x)
                return i;

        return -1;
    }

    static int BarrierSearch(int[] array, int x)
    {
        int n = array.Length;
        int last = array[n - 1];
        array[n - 1] = x;

        int i = 0;
        while (array[i] != x)
            i++;

        array[n - 1] = last;

        if (i < n - 1 || last == x)
            return i;

        return -1;
    }

    static int BinarySearch(int[] arr, int x)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (arr[mid] == x)
                return mid;

            if (arr[mid] < x)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    static int GoldenRatioBinarySearch(int[] arr, int x)
    {
        int left = 0;
        int right = arr.Length - 1;
        const double phi = 0.618;

        while (left <= right)
        {
            int mid = left + (int)(phi * (right - left));

            if (arr[mid] == x)
                return mid;

            if (arr[mid] < x)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }
} 