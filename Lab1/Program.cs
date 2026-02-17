using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string choice;
        int size, x;
        ReadInput(out size, out x);
        int[] array = GenerateArray(size);
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
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        int result = LinearSearch(array, x);
                        sw.Stop();
                        Console.WriteLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} мс");
                        if (result != -1)
                            Console.WriteLine($"Знайдено на позиції {result}");
                        else
                            Console.WriteLine("Елемент не знайдено");
                        break;
                    }
                    case "2":
                    {
                        Stopwatch sw = Stopwatch.StartNew();
                        int result = BarrierSearch(array, x);
                        sw.Stop();
                        Console.WriteLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} мс");
                        if (result != -1)
                            Console.WriteLine($"Знайдено на позиції {result}");
                        else
                            Console.WriteLine("Елемент не знайдено");
                        break;
                    }
                    case "3":
                    {
                        int[] arr = (int[])array.Clone();
                        Stopwatch sw = Stopwatch.StartNew();
                        int result = BinarySearch(arr, x);
                        sw.Stop();
                        Console.WriteLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} мс");
                        if (result != -1)
                            Console.WriteLine($"Знайдено на позиції {result}");
                        else
                            Console.WriteLine("Елемент не знайдено");
                        break;
                    }
                    case "4":
                    {
                        int[] arr = (int[])array.Clone();
                        Stopwatch sw = Stopwatch.StartNew();
                        int result = GoldenRatioBinarySearch(arr, x);
                        sw.Stop();
                        Console.WriteLine($"Час виконання: {sw.Elapsed.TotalMilliseconds:F4} мс");
                        if (result != -1)
                            Console.WriteLine($"Знайдено на позиції {result}");
                        else
                            Console.WriteLine("Елемент не знайдено");
                        break;
                    }
                    case "0":
                        Console.WriteLine("Завершення програми...");
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                } 
                   if (choice != "0")
                {
                    Console.WriteLine("\nНатисніть Enter, щоб повернутися до меню...");
                    Console.ReadLine();
                }
        }while(choice != "0");
    }
    static void ReadInput(out int size, out int x)
    {
        Console.Write("Введіть розмір масиву: ");
        while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
        {
            Console.Write("Некоректне число. Спробуйте ще раз: ");
        }

        Console.Write("Введіть число для пошуку: ");
        while (!int.TryParse(Console.ReadLine(), out x))
        {
            Console.Write("Некоректне число. Спробуйте ще раз: ");
        }
    }
        static int[] GenerateArray(int size)
    {
        int[] array = new int[size];
        Random rand = new Random();

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
          // Сортуємо масив
    Array.Sort(arr);

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
       // Сортуємо масив
    Array.Sort(arr);

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
