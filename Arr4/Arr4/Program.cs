using System;

class Program
{
    static string FractionToDecimal(int numerator, int denominator)
    {
        if (numerator == 0)
            return "0";

        if (numerator % denominator == 0)
            return (numerator / denominator).ToString();

        var result = (numerator / denominator).ToString() + '.';
        numerator %= denominator;
        string decimalPart = "";
        var Remainders = new int[denominator];
        var Positions = new int[denominator];
        var position = 0;
        var isRepeat = false;

         while (numerator != 0)
         {
            for (int i = 0; i < position; i++)
            {
                if (Remainders[i] == numerator)
                {
                    decimalPart = decimalPart.Insert(Positions[i], "(") + ')';
                    isRepeat = true;
                    break;
                }
            }
            
            if (isRepeat)
                break;

            Remainders[position] = numerator;
            Positions[position] = decimalPart.Length;
            position++;

            numerator *= 10;
            decimalPart += (numerator / denominator).ToString();
            numerator %= denominator;

         }

         return result + decimalPart;
    }

    static void Main()
    {
        var a = 1;
        var b = 6;
        Console.WriteLine(FractionToDecimal(a, b));
    }
}