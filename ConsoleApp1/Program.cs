using System;
using System.Drawing;
using System.Globalization;

class Program
{
   public class Ratio
    {
        public Ratio(int num, int den)
        {
            Numerator = num;
            if (den <= 0) throw new ArgumentException();
            Denominator = den;
            Value = num / den;
        }

        public readonly int Numerator; //числитель
        public readonly int Denominator; //знаменатель
        public readonly double Value; //значение дроби Numerator / Denominator
    }
}