using System;

class Program
{
    static void Main()
    {
        Console.Clear();

        int[] partitionArray = { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
        int[] counting = { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
        string[] radixSort = { "COW", "DOG", "SEA", "RUG", "ROW", "MOB", "BOX", "TAB", "BAR", "EAR", "TAR", "DIG", "BIG", "TEA", "NOW", "FOX" };
        double[] bucketSort = { 0.79, 0.13, 0.16, 0.64, 0.39, 0.20, 0.89, 0.53, 0.71, 0.42 };

        string choice;
        do
        {
            Console.Clear();

            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. PARTITION з масивом А = (13,19,9,5,12,8,7,4,21,2,6,11)");
            Console.WriteLine("2. MODIFIED PARTITION з масивом В = (1,1,1,1,1,1,1)");
            Console.WriteLine("3. QUICKSORT (незростаючий порядок)");
            Console.WriteLine("4. RANDOMIZED-QUICKSORT (незростаючий порядок)");
            Console.WriteLine("5. COUNTING-SORT");
            Console.WriteLine("6. Запити про кількість елементів на відрізку [a..b]");
            Console.WriteLine("7. RADIX-SORT");
            Console.WriteLine("8. BUCKET-SORT");
            Console.WriteLine("9. Задача про нафтову компанію");
            Console.WriteLine("0. Вихід");
            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    int[] copyA = (int[])partitionArray.Clone();
                    Console.WriteLine("1. Процедура PARTITION");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyA)})\n");
                    Partition(copyA, 0, copyA.Length - 1);
                    break;

                case "2":
                    Console.Clear();
                    int[] arrayB = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
                    int rightIndex = arrayB.Length - 1;
                    int qStandard = rightIndex;
                    Console.WriteLine($"({ArrayToString(arrayB)})");
                    Console.WriteLine("2. Процедура MODIFIED PARTITION");
                    int qModified = ModifiedPartition((int[])arrayB.Clone(), 0, rightIndex);
                    Console.WriteLine($"Стандартний PARTITION повертає q = r = {qStandard} (0), або {qStandard + 1} (1)");
                    Console.WriteLine($"Модифікований PARTITION повертає q = (p + r) / 2 = {qModified} (0), або {qModified + 1} (1)");
                    break;

                case "3":
                    Console.Clear();
                    int[] copyC = (int[])partitionArray.Clone();
                    Console.WriteLine("3. QUICKSORT (незростаючий порядок)");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyC)})");
                    quicksort(copyC, 0, copyC.Length - 1);
                    Console.WriteLine($"Відсортований масив: ({ArrayToString(copyC)})");
                    break;

                case "4":
                    Console.Clear();

                    break;

                case "5":
                    Console.Clear();

                    break;

                case "6":
                    Console.Clear();

                    break;

                case "7":
                    Console.Clear();

                    break;

                case "8":
                    Console.Clear();

                    break;

                case "9":
                    Console.Clear();

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

 static string ArrayToString(int[] arr, string separator = ", ")
    {
        string result = "";
        for (int i = 0; i < arr.Length; i++)
        {
            result += arr[i];
            if (i < arr.Length - 1)
            {
                result += separator;
            }
        }
        return result;
    }
    static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        Console.WriteLine($"Опорний елемент x = {pivot}\n");

        int i = left - 1;
        for (int j = left; j < right; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Console.WriteLine($"j = {j,-2}, A[j] = {arr[j],-2} --> A[j] <= pivot, зміна місцями A[{i}] та A[{j}]");
                
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
            else
            {
                Console.WriteLine($"j = {j,-2}, A[j] = {arr[j],-2} --> A[j] > pivot, без змін");
            }
            
            Console.WriteLine($"Поточний стан:  ({ArrayToString(arr)})");
            Console.WriteLine("---------------------------------------------");
        }

        Console.WriteLine($"\nОстання перестановка: A[{i + 1}] та A[{right}]");
        int finalTemp = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = finalTemp;

        Console.WriteLine($"Масив після PARTITION: ({ArrayToString(arr)})");
        Console.WriteLine($"Повернений індекс q: {i + 1}");
        
        return i + 1;
    }

    static int ModifiedPartition(int[] arr, int left, int right)
    {
        int pivot = arr[right];
        int i = left - 1;
        bool allEqual = true;

        for (int j = left; j < right; j++)
        {
            if (arr[j] != pivot)
            {
                allEqual = false;
            }
            else if (arr[j] <= pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        if (allEqual)
        {
            return (left + right) / 2;
        }

        int finalTemp = arr[i + 1];
        arr[i + 1] = arr[right];
        arr[right] = finalTemp;
        
        return i + 1;
    }

    static int partition (int[] array, int start, int end)
   {
       int temp;//swap helper
       int marker = start;
       for ( int i = start; i < end; i++ )
       {
           if ( array[i] > array[end] )
           {
               temp = array[marker]; // swap
               array[marker] = array[i];
               array[i] = temp;
               marker += 1;
           }
       }
       temp = array[marker];
       array[marker] = array[end];
       array[end] = temp;
       return marker;
   }

   static void quicksort (int[] array, int start, int end)
   {
       if ( start <= end )
       {
           int pivot = partition (array, start, end);
           quicksort (array, start, pivot-1);
           quicksort (array, pivot+1, end);
        }
   }

}