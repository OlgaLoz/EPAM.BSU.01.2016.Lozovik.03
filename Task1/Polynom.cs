using System;
using System.Globalization;
using System.Threading;

namespace Task1
{
    public class Polynom : IEquatable<Polynom>
    {
        private readonly double[] coefficients;

        public Polynom(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (coefficients.Length == 0)
            {
                throw new ArgumentException("Coefficient array can't be empty!");
            }

            this.coefficients = new double[LastCoefficientIndex(coefficients) + 1];
            for (int i = 0; i < this.coefficients.Length; i++)
            {
                this.coefficients[i] = coefficients[i];
            }
        }

        #region Override System.Object methods
        public override string ToString()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            string result = "";
            bool isFirstElement = true;
            for (int i = 0; i < coefficients.Length; i++)
            {
                if (coefficients[i].Equals(0)) continue;

                if (isFirstElement)
                {
                    isFirstElement = false;

                    if (coefficients[i] < 0)
                    {
                        result += GetSign(coefficients[i]);
                    }

                    if (Math.Abs(coefficients[i]).Equals(1))
                    {
                        result += $"x^{i}";
                    }
                    else
                    {
                        result += $"{Math.Abs(coefficients[i])}*x^{i}";
                    }                       
                }
                else
                {
                    if (Math.Abs(coefficients[i]).Equals(1))
                    {
                        result += $" {GetSign(coefficients[i])} x^{i}";
                    }
                    else
                    {
                        result += $" {GetSign(coefficients[i])} {Math.Abs(coefficients[i])}*x^{i}";
                    }                      
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            int hash = 12345;
            for (int i = 0; i < coefficients.Length; i++)
            {
                hash += hash ^ coefficients[i].GetHashCode();
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Polynom polynom = obj as Polynom;
            return Equals(polynom);
        }
        #endregion

        #region  Implementation of the interface IEquatable<T>
        public bool Equals(Polynom polynom)
        {
            if ((object)polynom == null)
            {
                return false;
            }

            return CompareArrays(coefficients, polynom.coefficients);
        }
        #endregion

        #region Overloaded operators
        public static bool operator ==(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if ((object)lhs != null)
            {
                return lhs.Equals(rhs);
            }

            return false;
        }

        public static bool operator !=(Polynom lhs, Polynom rhs)
        {
            return !(lhs == rhs);
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            return Add(lhs, rhs);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            return Substract(lhs, rhs);
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            return Multiply(lhs, rhs);
        }
        #endregion

        #region Arithmetic functions
        public static Polynom Add(Polynom lhs, Polynom rhs)
        {
            int maxLength = Max(lhs.coefficients.Length, rhs.coefficients.Length);
            double[] newCoefficients = new double[maxLength];

            for (int i = 0; i < maxLength; i++)
            {
                if (i < lhs.coefficients.Length)
                {
                    newCoefficients[i] += lhs.coefficients[i];
                }
                if (i < rhs.coefficients.Length)
                {
                    newCoefficients[i] += rhs.coefficients[i];
                }
            }
            return new Polynom(newCoefficients);
        }

        public static Polynom Substract (Polynom lhs, Polynom rhs)
        {
            int maxLength = Max(lhs.coefficients.Length, rhs.coefficients.Length); 
            double[] newCoefficients = new double[maxLength];
           
            for (int i = 0; i < maxLength; i++)
            {
                if (i < lhs.coefficients.Length)
                {
                    newCoefficients[i] += lhs.coefficients[i];
                }
                if (i < rhs.coefficients.Length)
                {
                    newCoefficients[i] -= rhs.coefficients[i];
                }
            }

            return new Polynom(newCoefficients);
        }

        public static Polynom Multiply(Polynom lhs, Polynom rhs)
        {
            double[] tempCoefficients = new double[lhs.coefficients.Length * rhs.coefficients.Length];

            for (int i = 0; i < lhs.coefficients.Length; i++)
            {
                for (int j = 0; j < rhs.coefficients.Length; j++)
                {
                    tempCoefficients[i + j] += lhs.coefficients[i] * rhs.coefficients[j];
                }
            }

            double[] newCoefficients = new double[LastCoefficientIndex(tempCoefficients) + 1];
            for (int i = 0; i < newCoefficients.Length; i++)
            {
                newCoefficients[i] = tempCoefficients[i];
            }

            return new Polynom(newCoefficients);
        }
        #endregion

        #region Private methods
        private static int Max(int a, int b) => a < b ? b : a;

        private static int LastCoefficientIndex(double[] array)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (!array[i].Equals(0))
                {
                    return i;
                }
            }
            return 0;
        }

        private string GetSign(double value) => value >= 0 ? "+" : "-";

        private bool CompareArrays(double[] array1, double[] array2 )
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (!Equals(array1[i], array2[i]))
                {
                    return false;
                }
            }

            return true;
        }
#endregion

    }
}
