using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random random = new Random();

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
                    int[] copyD = (int[])partitionArray.Clone();
                    Console.WriteLine("4. RANDOMIZED-QUICKSORT (незростаючий порядок)");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyD)})");
                    randomized_quicksort(copyD, 0, copyD.Length - 1);
                    Console.WriteLine($"Відсортований масив: ({ArrayToString(copyD)})");
                    break;

                case "5":
                    Console.Clear();
                    int[] copyCounting = (int[])counting.Clone();
                    Console.WriteLine("5. COUNTING-SORT");
                    Console.WriteLine($"Початковий масив A: ({ArrayToString(copyCounting)})\n");
                    int maxElement = 6;
                    int[] sortedCounting = CountingSort(copyCounting, maxElement);
                    Console.WriteLine($"\nВідсортований масив B: ({ArrayToString(sortedCounting)})");
                    break;

                case "6":
                    Console.Clear();
                    Console.WriteLine("6. Попередня обробка для відповіді на запити [a..b]");
                    int[] arrForQuery = { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
                    Console.WriteLine($"Вхідний масив: ({ArrayToString(arrForQuery)})");

                    int kQuery = 6;
                    int[] prefixSums = PreprocessForRangeQueries(arrForQuery, kQuery);

                    Console.WriteLine();
                    int[,] queries = { { 0, 0 }, { 1, 3 }, { 2, 6 }, { 4, 5 } };
                    for (int q = 0; q < queries.GetLength(0); q++)
                    {
                        int aQuery = queries[q, 0];
                        int bQuery = queries[q, 1];
                        int countElements = CountElementsInRange(prefixSums, aQuery, bQuery);
                        Console.WriteLine($"Кількість на відрізку [{aQuery}..{bQuery}]: {countElements}");
                    }
                    break;

                case "7":
                    Console.Clear();
                    Console.WriteLine("7. RADIX-SORT (Сортування слів)");

                    string[] copyRadix = (string[])radixSort.Clone();
                    Console.WriteLine($"Початковий масив: ({string.Join(", ", copyRadix)})\n");

                    RadixSort(copyRadix);

                    Console.WriteLine($"\nВідсортований масив: ({string.Join(", ", copyRadix)})");
                    break;

                case "8":
                    Console.Clear();
                    Console.WriteLine("8. Завдання BUCKET-SORT");

                    double[] copyBucket = { 0.79, 0.13, 0.16, 0.64, 0.39, 0.20, 0.89, 0.53, 0.71, 0.42 };
                    Console.WriteLine($"Початковий масив: [{string.Join(", ", copyBucket.Select(x => x.ToString("0.00")))}]\n");

                    BucketSortTrace(copyBucket);
                    break;

                case "9":
                    Console.Clear();
                    Console.WriteLine("9. Задача про нафтову компанію (Оптимальне розташування трубопроводу)");

                    int[] yCoordinates = { 45, 22, 8, 31, 7 };
                    Console.WriteLine($"\nКоординати y: {string.Join(", ", yCoordinates)}");

                    Task9(yCoordinates, 0, yCoordinates.Length - 1);
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

    static int partition (int[] array, int left, int right)
   {
       int temp;
       int marker = left;
       for ( int i = left; i < right; i++ )
       {
           if ( array[i] > array[right] )
           {
               temp = array[marker]; // swap
               array[marker] = array[i];
               array[i] = temp;
               marker += 1;
           }
       }
       temp = array[marker];
       array[marker] = array[right];
       array[right] = temp;
       return marker;
   }

   static void quicksort (int[] array, int left, int right)
   {
       if ( left <= right )
       {
           int pivot = partition (array, left, right);
           quicksort (array, left, pivot-1);
           quicksort (array, pivot+1, right);
        }
   }

   static int randomized_partition(int[] array, int left, int right)
   {
       int i = random.Next(left, right + 1);
       int temp = array[right];
       array[right] = array[i];
       array[i] = temp;
       return partition(array, left, right);
   }
   static void randomized_quicksort(int[] array, int left, int right)
   {
       if (left < right)
       {
           int pivot = randomized_partition(array, left, right);
           randomized_quicksort(array, left, pivot - 1);
           randomized_quicksort(array, pivot + 1, right);
       }
   }
   static int[] CountingSort(int[] A, int k)
    {
        int[] C = new int[k + 1];
        int[] B = new int[A.Length];

        for (int j = 0; j < A.Length; j++)
        {
            C[A[j]]++;
        }
        Console.WriteLine($"Крок 1 (підрахунок частот). Масив C: ({ArrayToString(C)})");

        for (int i = 1; i <= k; i++)
        {
            C[i] += C[i - 1];
        }
        Console.WriteLine($"Крок 2 (накопичення сум). Масив C: ({ArrayToString(C)})\n");

        Console.WriteLine("Крок 3 (розміщення елементів):");
        for (int j = A.Length - 1; j >= 0; j--)
        {
            B[C[A[j]] - 1] = A[j];
            C[A[j]]--;

            Console.WriteLine($"Обробка елемента A[{j}] = {A[j]}:");
            Console.WriteLine($"  Масив C: ({ArrayToString(C)})");
            Console.WriteLine($"  Масив B: ({ArrayToString(B)})\n");
        }

        return B;
    }

    static int[] PreprocessForRangeQueries(int[] A, int k)
    {
        int[] C = new int[k + 1];
        for (int i = 0; i < A.Length; i++)
        {
            C[A[i]]++;
        }
        Console.WriteLine($"Частоти елементів: ({ArrayToString(C)})");

        for (int i = 1; i <= k; i++)
        {
            C[i] += C[i - 1];
        }
        Console.WriteLine($"Префіксні суми: ({ArrayToString(C)})");

        return C;
    }

    static int CountElementsInRange(int[] prefixSums, int a, int b)
    {
        if (a > b)
        {
            return 0;
        }

        int maxK = prefixSums.Length - 1;
        if (b > maxK)
        {
            b = maxK;
        }

        if (a > maxK)
        {
            return 0;
        }

        if (a <= 0)
        {
            return prefixSums[b];
        }
        else
        {
            return prefixSums[b] - prefixSums[a - 1];
        }
    }

    static void RadixSort(string[] arr)
    {
        int numLetters = 3;

        for (int i = numLetters - 1; i >= 0; i--)
        {
            CountingSortForRadix(arr, i);
            Console.WriteLine($"Після сортування за позицією {i + 1} (з кінця до початку):");
            Console.WriteLine($"({string.Join(", ", arr)})\n");
        }
    }

    static void CountingSortForRadix(string[] arr, int charPosition)
    {
        int n = arr.Length;
        string[] output = new string[n];

        int[] count = new int[26];

        for (int i = 0; i < n; i++)
        {
            int charIndex = arr[i][charPosition] - 'A';
            count[charIndex]++;
        }

        for (int i = 1; i < 26; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            int charIndex = arr[i][charPosition] - 'A';
            output[count[charIndex] - 1] = arr[i];
            count[charIndex]--;
        }

        for (int i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }
    }


    static void BucketSortTrace(double[] array)
    {
        int bucketCount = array.Length;
        List<double>[] buckets = new List<double>[bucketCount];

        for (int i = 0; i < bucketCount; i++)
        {
            buckets[i] = new List<double>();
        }

        for (int i = 0; i < array.Length; i++)
        {
            int index = (int)(bucketCount * array[i]);
            if (index >= bucketCount)
            {
                index = bucketCount - 1;
            }
            buckets[index].Add(array[i]);
        }

        Console.WriteLine("Кошики після розподілу:");
        for (int i = 0; i < bucketCount; i++)
        {
            string items = buckets[i].Count > 0 ? string.Join(", ", buckets[i].Select(x => x.ToString("0.00"))) : "";
            Console.WriteLine($"Кошик {i}: [{items}]");
        }

        Console.WriteLine("\nКошики після сортування:");
        for (int i = 0; i < bucketCount; i++)
        {
            InsertionSortList(buckets[i]);
            string items = buckets[i].Count > 0 ? string.Join(", ", buckets[i].Select(x => x.ToString("0.00"))) : "";
            Console.WriteLine($"Кошик {i}: [{items}]");
        }

        List<double> result = new List<double>();
        for (int i = 0; i < bucketCount; i++)
        {
            result.AddRange(buckets[i]);
        }
    }


    static void InsertionSortList(List<double> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            double key = list[i];
            int j = i - 1;

            while (j >= 0 && list[j] > key)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = key;
        }
    }

    static void Task9(int[] arr, int left, int right)
    {
        left = 0;
        right = arr.Length - 1;
        int k = arr.Length / 2;
        Random rand = new Random();


        while (left <= right)
        {
            int pivotIndex = rand.Next(left, right + 1);


            int temp = arr[pivotIndex];
            arr[pivotIndex] = arr[right];
            arr[right] = temp;

            int pivot = arr[right];
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (arr[j] <= pivot)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                }
            }

            temp = arr[i];
            arr[i] = arr[right];
            arr[right] = temp;

            if (i == k)
                break;
            else if (k < i)
                right = i - 1;
            else
                left = i + 1;
        }

        int optimalY = arr[k];

        int totalLength = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            totalLength += Math.Abs(arr[i] - optimalY);
        }

        Console.Write($"\nОптимальний рівень трубопроводу Y = {optimalY}");
        Console.Write($"\nМінімальна загальна довжина рукавів = {totalLength}\n");
    }
}