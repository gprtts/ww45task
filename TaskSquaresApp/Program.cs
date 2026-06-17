using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Запуск программы...");
        int[] numbers = { 1, 2, 3, 4, 5 };

        int totalSum = 0;
        object locker = new object();

        Task[] tasks = new Task[numbers.Length];

        for (int i = 0; i < numbers.Length; i++)
        {
            int num = numbers[i];

            tasks[i] = Task.Run(() =>
            {
                int square = num * num;

                Console.WriteLine($"Число: {num}, квадрат: {square}, поток: {Thread.CurrentThread.ManagedThreadId}");

                lock (locker)
                {
                    totalSum += square;
                }
            });
        }

        Task.WaitAll(tasks);

        Console.WriteLine($"Итоговая сумма квадратов: {totalSum}");
        Console.ReadLine();
    }
}