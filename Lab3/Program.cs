 using System;
 class Program
 {
     static void Main()
     {
         Console.Clear();

         string choice ;
        do
        {
         Console.Clear();
         Console.WriteLine("Виберіть алгоритм поуку:");
         Console.WriteLine("1. Direct Search");
         Console.WriteLine("2. KnuthMorrisPrattSearch");
         Console.WriteLine("0. Вихід");
         choice = Console.ReadLine() ?? "";
        switch (choice)
         {
             case "1":
                Console.Clear();
                Console.Write("Введіть основний текст: ");
                string text1 = Input();
                Console.Write("Введіть рядок для пошуку: ");
                string pattern1 = Input();
                int result1 = DirectSearch(text1, pattern1);
                if (result1 >= 0)
                    Console.WriteLine($"Знайдено на позиції: {result1}");
                else
                    Console.WriteLine("Рядок не знайдено.");
                break;
            case "2":
                Console.Clear();
                Console.Write("Введіть основний текст: ");
                string text2 = Input();
                Console.Write("Введіть рядок для пошуку: ");
                string pattern2 = Input();
                int result2 = KnuthMorrisPrattSearch(text2, pattern2);
                if (result2 >= 0)
                    Console.WriteLine($"Знайдено на позиції: {result2}");
                else
                    Console.WriteLine("Рядок не знайдено.");
                break;
             case "0":
                 Console.WriteLine("Вихід з програми.");
                 break;
             default:
                 Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                 break;
         }
         Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
         Console.ReadKey();
         Console.Clear();
        }
        while (choice != "0");

     }

    static string Input()
    {
        string str ;
        do
        {
            str =  Console.ReadLine();
            if (str == null || str.Length == 0 || str =="")
            {
                Console.WriteLine("Помилка! Спробуйте ще раз ");
            }
        }
        while(str == null || str.Length == 0 || str =="");
        return str;
    }

    private static int DirectSearch(string text, string pattern)
    {
        int n = text.Length;
        int m = pattern.Length;
        int i = -1;
        int j;

        do
        {
            i++;
            j = 0;
            while (j < m && text[i + j] == pattern[j])
                j++;
        }
        while (j != m && i < n - m);

        if (j == m)
            return i;
        return -1;
    }

    private static int[] prefixFun(string pattern)
    {
        int[] pi = new int[pattern.Length];
        pi[0] = 0;
        int i = 1;
        int j = 0;

        while (i < pattern.Length)
        {
            if (pattern[i] == pattern[j])
            {
                pi[i] = j + 1;
                i++;
                j++;
            }
            else if (j == 0)
            {
                pi[i] = 0;
                i++;
            }
            else
            {
                j = pi[j - 1];
            }
        }

        return pi;
    }
    private static int KnuthMorrisPrattSearch(string text, string pattern, int[]? prefix = null)
    {
        prefix ??= prefixFun(pattern);
        int n = text.Length;
        int m = pattern.Length;
        int k = 0;
        int l = 0;

        while (k != n)
        {
            if (text[k] == pattern[l])
            {
                k++;
                l++;
                if (l == m)
                    return k - m;
            }
            else if (l == 0)
            {
                k++;
                if (k == n)
                    return -1;
            }
            else
            {
                l = prefix[l - 1];
            }
        }

        return -1;
    }



 }