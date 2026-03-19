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
            Console.WriteLine(" Меню ");
            Console.WriteLine("1 ");
            Console.WriteLine("2");
            Console.WriteLine("0. Вихід");
            choice = Console.ReadLine() ?? "";
            switch (choice)
            {
                case "1":
                    
                    break;
                case "2":
                    
                    break;
                case "0":
                    
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
        
}