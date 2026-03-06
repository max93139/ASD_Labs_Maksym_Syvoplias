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

            Console.WriteLine("Поточний масив:");
            PrintArray(partitionArray);

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
                    PrintInitialPartitionState(copyA);
                    Partition(copyA, 0, copyA.Length - 1);
                    break;

                case "2":
                    Console.Clear();
                    int[] arrayB = { 1, 1, 1, 1, 1, 1 };
                    int rightIndex = arrayB.Length - 1;
                    int qStandard = rightIndex; // 
                    int qModified = ModifiedPartition((int[])arrayB.Clone(), 0, rightIndex);

                    PrintModifiedPartitionResult(arrayB, qStandard, qModified);
                    break;

                case "3":
                    Console.Clear();
                    int[] copyForQs = (int[])partitionArray.Clone();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("3. QUICKSORT (незростаючий порядок)");
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyForQs)})\n");
                    quicksort(copyForQs, 0, copyForQs.Length - 1);
                    Console.WriteLine($"\nВідсортований масив: ({ArrayToString(copyForQs)})");
                    break;

                case "4":
                    Console.Clear();
                    int[] copyForRQs = (int[])partitionArray.Clone();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("4. RANDOMIZED-QUICKSORT (незростаючий порядок)");
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyForRQs)})\n");
                    RandomizedQuicksort(copyForRQs, 0, copyForRQs.Length - 1);
                    Console.WriteLine($"\nВідсортований масив: ({ArrayToString(copyForRQs)})");
                    break;

                case "5":
                    Console.Clear();
                    int[] copyForCS = (int[])counting.Clone();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("5. ПРОЦЕДУРА COUNTING-SORT");
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyForCS)})\n");
                    CountingSortIllustration(copyForCS);
                    break;

                case "6":
                    Console.Clear();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("6. ЗАПИТИ ПРО КІЛЬКІСТЬ ЕЛЕМЕНТІВ НА ВІДРІЗКУ [a..b] ЗА O(1)");
                    Console.WriteLine("==================================================");
                    ExplainRangeQueryAlgorithm();
                    break;

                case "7":
                    Console.Clear();
                    string[] copyForRadix = (string[])radixSort.Clone();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("7. RADIX-SORT (сортування слів)");
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyForRadix)})\n");
                    RadixSortIllustration(copyForRadix);
                    break;

                case "8":
                    Console.Clear();
                    double[] copyForBucket = (double[])bucketSort.Clone();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("8. BUCKET-SORT (сортування дійсних чисел)");
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Початковий масив: ({ArrayToString(copyForBucket)})\n");
                    BucketSortIllustration(copyForBucket);
                    break;

                case "9":
                    Console.Clear();
                    Console.WriteLine("==================================================");
                    Console.WriteLine("9. ЗАДАЧА ПРО НАФТОВУ КОМПАНІЮ (ТРУБОПРОВІД)");
                    Console.WriteLine("==================================================");
                    ExplainPipelineMedianAlgorithm();
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

    static void PrintArray(int[] arr)
    {
        Console.WriteLine($"({ArrayToString(arr)})");
    }

    static void PrintInitialPartitionState(int[] arr)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("1. ПРОЦЕДУРА PARTITION");
        Console.WriteLine("==================================================");
        Console.WriteLine($"Початковий масив: ({ArrayToString(arr)})\n");
    }

    static void PrintModifiedPartitionResult(int[] arrayB, int qStandard, int qModified)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("2. MODIFIED PARTITION (масив з однаковими елементами)");
        Console.WriteLine("==================================================");
        Console.WriteLine($"Початковий масив: ({ArrayToString(arrayB)})\n");
        Console.WriteLine($"Стандартний PARTITION повертає q = r = {qStandard} (0-based), або {qStandard + 1} (1-based)");
        Console.WriteLine($"Модифікований PARTITION повертає q = (p + r) / 2 = {qModified} (0-based), або {qModified + 1} (1-based)");
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

    static int[] quicksort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] >= pivot) // Незростаючий порядок (>=)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int finalTemp = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = finalTemp;
            
            int q = i + 1;

            quicksort(arr, left, q - 1);
            quicksort(arr, q + 1, right);
        }
        return arr;
    }

    static Random _rand = new Random();

    static int[] RandomizedQuicksort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = _rand.Next(left, right + 1);
            // Випадковий вибір опорного елемента і обмін з останнім
            int tempPivot = arr[pivotIndex];
            arr[pivotIndex] = arr[right];
            arr[right] = tempPivot;

            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] >= pivot) // Незростаючий порядок (>=)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int finalTemp = arr[i + 1];
            arr[i + 1] = arr[right];
            arr[right] = finalTemp;
            
            int q = i + 1;

            RandomizedQuicksort(arr, left, q - 1);
            RandomizedQuicksort(arr, q + 1, right);
        }
        return arr;
    }

    static void CountingSortIllustration(int[] A)
    {
        int k = 0;
        for (int i = 0; i < A.Length; i++)
        {
            if (A[i] > k) k = A[i];
        }

        int[] C = new int[k + 1];
        int[] B = new int[A.Length];

        Console.WriteLine($"Інтервал значень: 0..{k}  (k = {k})\n");

        for (int j = 0; j < A.Length; j++)
        {
            C[A[j]]++;
        }
        Console.WriteLine($"Масив C після підрахунку входжень: ({ArrayToString(C)})");

        for (int i = 1; i <= k; i++)
        {
            C[i] = C[i] + C[i - 1];
        }
        Console.WriteLine($"Масив C після накопичення сум: ({ArrayToString(C)})\n");

        for (int j = A.Length - 1; j >= 0; j--)
        {
            B[C[A[j]] - 1] = A[j];
            Console.WriteLine($"j = {j,2}, A[j] = {A[j]}: розміщуємо в B[{C[A[j]] - 1,2}], C[{A[j]}] стає {C[A[j]] - 1,2}");
            C[A[j]]--;
        }

        Console.WriteLine($"\nВідсортований масив: ({ArrayToString(B)})");
    }

    static void ExplainRangeQueryAlgorithm()
    {
        Console.WriteLine("ОПИС АЛГОРИТМУ:");
        Console.WriteLine("Алгоритм базується на ідеї попередньої обробки, схожій на перший етап Counting-Sort.\n");
        Console.WriteLine("1. Створюємо масив C розміром k + 1, заповнений нулями.");
        Console.WriteLine("2. Для кожного елемента x вхідного масиву A збільшуємо C[x] на 1 (C[x]++).");
        Console.WriteLine("   (На цьому етапі C[x] містить кількість елементів, рівних x).");
        Console.WriteLine("3. Обчислюємо префіксні суми: для кожного i від 1 до k робимо C[i] = C[i] + C[i - 1].");
        Console.WriteLine("   (Після цього C[i] містить кількість елементів, що менші або дорівнюють i).");
        Console.WriteLine("\nСкладність попередньої обробки: Θ(n) для кроку 2 та Θ(k) для кроку 3. Разом: Θ(n + k).\n");
        
        Console.WriteLine("ВІДПОВІДЬ НА ЗАПИТ за час O(1):");
        Console.WriteLine("Щоб дізнатися скільки елементів належить відрізку [a..b]:");
        Console.WriteLine("- Якщо a == 0, відповідь: C[b]");
        Console.WriteLine("- Якщо a > 0,  відповідь: C[b] - C[a - 1]\n");

        Console.WriteLine("ПРИКЛАД ДЕМОНСТРАЦІЇ:");
        int[] A = { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
        int k = 6;
        Console.WriteLine($"Вхідний масив A: ({ArrayToString(A)}), k = {k}");
        
        int[] C = new int[k + 1];
        for (int i = 0; i < A.Length; i++) C[A[i]]++;
        for (int i = 1; i <= k; i++) C[i] += C[i - 1];
        
        Console.WriteLine($"Префіксний масив C після обробки: ({ArrayToString(C)})\n");

        int a1 = 1, b1 = 4;
        int ans1 = a1 == 0 ? C[b1] : C[b1] - C[a1 - 1];
        Console.WriteLine($"Запит [a..b] = [{a1}..{b1}]:");
        Console.WriteLine($"Результат: C[{b1}] - C[{a1 - 1}] = {C[b1]} - {C[a1 - 1]} = {ans1} елементів");

        int a2 = 0, b2 = 2;
        int ans2 = a2 == 0 ? C[b2] : C[b2] - C[a2 - 1];
        Console.WriteLine($"Запит [a..b] = [{a2}..{b2}]:");
        Console.WriteLine($"Результат: C[{b2}] = {C[b2]} елементів");
    }

    static string ArrayToString(string[] arr, string separator = ", ")
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

    static void RadixSortIllustration(string[] arr)
    {
        int wordLength = 3; // Всі слова мають довжину 3 символи
        
        for (int pos = wordLength - 1; pos >= 0; pos--)
        {
            CountingSortForStrings(arr, pos);
            Console.WriteLine($"Після сортування за розрядом {pos + 1} (індекс {pos}, літера з кінця):");
            Console.WriteLine($"({ArrayToString(arr)})\n");
        }
        
        Console.WriteLine($"Відсортований масив: ({ArrayToString(arr)})");
    }

    static void CountingSortForStrings(string[] arr, int pos)
    {
        int n = arr.Length;
        string[] output = new string[n];
        int[] count = new int[256];

        for (int i = 0; i < n; i++)
        {
            count[arr[i][pos]]++;
        }

        for (int i = 1; i < 256; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            output[count[arr[i][pos]] - 1] = arr[i];
            count[arr[i][pos]]--;
        }

        for (int i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }
    }

    static string ArrayToString(double[] arr, string separator = ", ")
    {
        string result = "";
        for (int i = 0; i < arr.Length; i++)
        {
            // Форматуємо з відкиданням нуля, щоб було ".79", як в умові, якщо потрібно, або "0.79"
            result += arr[i].ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            if (i < arr.Length - 1)
            {
                result += separator;
            }
        }
        return result;
    }

    static void BucketSortIllustration(double[] arr)
    {
        int n = arr.Length;
        System.Collections.Generic.List<double>[] buckets = new System.Collections.Generic.List<double>[n];

        for (int i = 0; i < n; i++)
        {
            buckets[i] = new System.Collections.Generic.List<double>();
        }

        Console.WriteLine("Розподіл по кошиках (buckets):");
        for (int i = 0; i < n; i++)
        {
            int bucketIndex = (int)(n * arr[i]);
            buckets[bucketIndex].Add(arr[i]);
            Console.WriteLine($"Елемент {arr[i].ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)} йде у кошик B[{bucketIndex}]");
        }

        Console.WriteLine("\nСортування кожного кошика та результат:");
        for (int i = 0; i < n; i++)
        {
            if (buckets[i].Count > 0)
            {
                buckets[i].Sort();
                string bucketContent = string.Join(", ", buckets[i].ConvertAll(d => d.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)));
                Console.WriteLine($"Кошик B[{i}] містить: ({bucketContent})");
            }
            else
            {
                Console.WriteLine($"Кошик B[{i}] порожній.");
            }
        }

        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < buckets[i].Count; j++)
            {
                arr[index++] = buckets[i][j];
            }
        }

        Console.WriteLine($"\nВідсортований масив: ({ArrayToString(arr)})");
    }

    static void ExplainPipelineMedianAlgorithm()
    {
        Console.WriteLine("ОПИС ПРОБЛЕМИ:");
        Console.WriteLine("Трубопровід пройде зі сходу на захід, тобто його Y-координата буде постійною.");
        Console.WriteLine("Свердловини мають координати (x_i, y_i), де x_i - ігноруються, а y_i - впливають на довжину рукавів.");
        Console.WriteLine("Загальна довжина рукавів: Сума |y_i - Y|, де Y — координата магістрального трубопроводу.\n");

        Console.WriteLine("МАТЕМАТИЧНЕ ОБҐРУНТУВАННЯ:");
        Console.WriteLine("Відомо з математики, що сума відстаней від заданих точок на осі до шуканої точки Y");
        Console.WriteLine("мінімізується тоді, коли Y є **МЕДІАНОЮ** масиву координат y_i.");
        Console.WriteLine("Якщо кількість точок непарна, Y = y_[median].");
        Console.WriteLine("Якщо кількість точок парна, Y може бути будь-якою точкою між двома середніми значеннями.\n");

        Console.WriteLine("АЛГОРИТМ ТА ЧАС ВИКОНАННЯ Θ(n):");
        Console.WriteLine("Пошук медіани масиву не потребує повного його сортування (яке займало б O(n log n)).");
        Console.WriteLine("Можна використати алгоритм **Quickselect** (або медіану медіан), який знаходить");
        Console.WriteLine("k-ту порядкову статистику (в нашому випадку медіану) за середній час Θ(n).\n");

        Console.WriteLine("ПРИКЛАД:");
        int[] y_coords = { 5, 20, 15, 1, 8, 4, 11 };
        Console.WriteLine($"Y-координати свердловин: ({ArrayToString(y_coords)})");
        
        int[] sorted_y = (int[])y_coords.Clone();
        Array.Sort(sorted_y); // використовуємо Sort лише для простої ілюстрації результату
        Console.WriteLine($"Відсортовано для наочності: ({ArrayToString(sorted_y)})");
        
        int medianIndex = sorted_y.Length / 2;
        int Y_opt = sorted_y[medianIndex];
        Console.WriteLine($"Оптимальна координата трубопроводу (медіана): Y = {Y_opt}");

        int sumDist = 0;
        foreach (int y in y_coords)
        {
            sumDist += Math.Abs(y - Y_opt);
        }
        Console.WriteLine($"Мінімальна загальна довжина рукавів: {sumDist}");
    }
}
