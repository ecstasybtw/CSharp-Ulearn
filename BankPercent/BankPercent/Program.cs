using System;

namespace BankPercent
{
    class Program
    {
        public static double Calculate(string userInput)
        {
            // Разделение строки на части
            string[] nums = userInput.Split(" ");

            // Преобразование строк в числа
            double ammountOfMoney = double.Parse(nums[0]);
            double annualRate = double.Parse(nums[1]);
            int termInMonths = int.Parse(nums[2]);

            // Приведение годовой процентной ставки к месячной
            double monthlyRate = annualRate / 12 / 100;

            // Вычисление итоговой суммы с учетом ежемесячной капитализации
            double accumulatedAmount = ammountOfMoney * Math.Pow(1 + monthlyRate, termInMonths);

            // Округление результата до целого числа
            return accumulatedAmount;
        }
    
        static void Main()
        {
            // Чтение пользовательского ввода
            string? userInput = Console.ReadLine();
            if (userInput != null)
            {
                // Вывод результата
                Console.WriteLine(Calculate(userInput));
            }
        }
    }
}