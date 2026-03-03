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

        // Ввід розміру
        Console.Write("Введіть розмір масиву: ");
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            Console.Write("Некоректне число. Спробуйте ще раз: ");

        // Генерація масиву
        int[] array = GenerateArray(size);

        Console.WriteLine("\nЗгенерований масив:");
        PrintArray(array);

        // Ввід числа для пошуку
        Console.Write("\nВведіть число для пошуку: ");
        while (!int.TryParse(Console.ReadLine(), out x))
            Console.Write("Некоректне число. Спробуйте ще раз: ");

        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
        Console.ReadLine();

        do
        {
            Console.Clear();

            Console.WriteLine("Поточний масив:");
            PrintArray(array);

            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Пошук перебором");
            Console.WriteLine("2. Пошук з бар'єром");
            Console.WriteLine("3. Бінарний пошук");
            Console.WriteLine("4. Бінарний пошук (золотий переріз)");
            Console.WriteLine("0. Вихід");

            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RunSearch(LinearSearch, array, x, false, "Linear");
                    break;

                case "2":
                    RunSearch(BarrierSearch, array, x, false, "Barrier");
                    break;

                case "3":
                    RunSearch(BinarySearch, array, x, true, "Binary");
                    break;

                case "4":
                    RunSearch(GoldenRatioBinarySearch, array, x, true, "Golden Ratio");
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

    static void RunSearch(Func<int[], int, int> searchMethod,
                          int[] originalArray,
                          int x,
                          bool needsSorting,
                          string name)
    {
        int[] arr = (int[])originalArray.Clone();

        if (needsSorting)
        {
            Array.Sort(arr);
            Console.WriteLine("\nВідсортований масив:");
        }
        else
        {
            Console.WriteLine("\nМасив:");
        }

        PrintArray(arr);

        Stopwatch sw = Stopwatch.StartNew();
        int result = searchMethod(arr, x);
        sw.Stop();

        Console.WriteLine($"\n{name} Search:");
        Console.WriteLine($"Шукане число: {x}");
        Console.WriteLine($"Індекс: {result}");
        Console.WriteLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F6} мс");
    }

    static int[] GenerateArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
            array[i] = rand.Next(0, 100);
        return array;
    }

    static void PrintArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Console.Write(array[i] + " ");
        Console.WriteLine();
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

    static int BinarySearch(int[] array, int x)
    {
        int left = 0, right = array.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (array[mid] == x)
            {
                return mid;
            }
            else if (array[mid] < x)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }

    static int GoldenRatioBinarySearch(int[] array, int x)
    {
        int left = 0, right = array.Length - 1;
        double phi = (1 + Math.Sqrt(5)) / 2; 

        while (left <= right)
        {
            int mid = left + (int)((right - left) / phi);

            if (array[mid] == x)
            {
                return mid;
            }
            else if (array[mid] < x)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }
}
