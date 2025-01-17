﻿namespace Lab1.Models
{
    public class CubicFunction : Function
    {

        public double Cubic { get; init; }
        public double Quadratic { get; init; }
        public double Linear { get; init; }
        public double Constant { get; init; }

        public CubicFunction()
        {
            Quadratic = 1;
            Quadratic = 0;
            Linear = 0;
            Constant = 0;
        }
        public CubicFunction(double cubic, double quadratic, double linear, double constant)
        {
            Cubic = cubic;
            Quadratic = quadratic;
            Linear = linear;
            Constant = constant;
        }

        public override double Calculate(double x)
            => Cubic * x * x + Quadratic * x * x + Linear * x + Constant;

        public override Function GetDerivative()
            => new QuadraticFunction(Cubic, Quadratic, Linear);

        public override Function GetAntiderivative()
            => new CubicFunction(Cubic, Quadratic, Linear, Constant);

        public override bool Equals(Function? obj)
        {
            if (obj is not CubicFunction f)
                return false;

            return Math.Round(Constant, 3) == Math.Round(f.Constant, 3) &&
                Math.Round(Linear, 3) == Math.Round(f.Linear, 3) &&
                Math.Round(Quadratic, 3) == Math.Round(f.Quadratic, 3) &&
                Math.Round(Cubic, 3) == Math.Round(f.Cubic, 3);
        }

        public override string ToString()
        {
            string result = "";

            if (Cubic != 0)
                result += $"{Cubic}x^3 ";

            if (Quadratic != 0)
                result += Quadratic > 0
                    ? $"+ {Quadratic}x "
                    : $"- {Math.Abs(Quadratic)}x ";

            if (Linear != 0)
                result += Linear > 0
                    ? $"+ {Linear}x "
                    : $"- {Math.Abs(Linear)}x ";

            if (Constant != 0)
                result += Constant > 0
                    ? $"+ {Constant}x "
                    : $"- {Math.Abs(Constant)}x ";
            return result;
        }

        public override int GetHashCode()
            => HashCode.Combine(Quadratic, Linear, Constant);
    }
}