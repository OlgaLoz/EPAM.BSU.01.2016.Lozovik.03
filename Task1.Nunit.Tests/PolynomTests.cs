using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Task1.Nunit.Tests
{
    [TestFixture]
    public class PolynomTests
    {
        [TestCase(new double[] { 0, 1, 10 }, Result = "x^1 + 10*x^2")]
        [TestCase(new double[] { 0, 0, 10 }, Result = "10*x^2")]
        [TestCase(new double[] { 5, 1, 10 }, Result = "5*x^0 + x^1 + 10*x^2")]
        [TestCase(new double[] { -8, 1, -10 }, Result = "-8*x^0 + x^1 - 10*x^2")]
        [TestCase(new double[] { 0 },  Result = "")]
        public string ToString_Test(double[] coefficients)
        {
            return new Polynom(coefficients).ToString();
        }

        [TestCase(new double[] {}, ExpectedException = typeof(ArgumentException))]
        [TestCase(null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase(new double[] { 0, 1, 10 }, Result = "x^1 + 10*x^2")]
        public string Constructor_Test(double[] coefficients)
        {
            return new Polynom(coefficients).ToString();
        }

        #region Arithmetic functions Tests

        [TestCase(new double[] {0, 1, 10, -5 }, new double[] {0, 0, 2, 4 }, Result = "x^1 + 12*x^2 - x^3")]
        [TestCase(new double[] { 0, 1, 10 }, new double[] { 4 }, Result = "4*x^0 + x^1 + 10*x^2")]
        [TestCase(new double[] { 4 }, new double[] { 0, 1, 10 }, Result = "4*x^0 + x^1 + 10*x^2")]
        [TestCase(new double[] { 0 }, new double[] { 0 }, Result = "")]
        [TestCase(new double[] { 0, 1, 10 }, new double[] { 0, 0, 2, 4 }, Result = "x^1 + 12*x^2 + 4*x^3")]
        [TestCase(new double[] { 0 }, new double[] { 5, 0, -1, -4 }, Result = "5*x^0 - x^2 - 4*x^3")]
        public string Add_Test(double[] lhs, double[] rhs)
        {
            return (new Polynom(lhs) + new Polynom(rhs)).ToString();
        }

        [TestCase(new double[] { 0, 1, 10, -5 }, new double[] { 0, 0, 2, 4 }, Result = "x^1 + 8*x^2 - 9*x^3")]
        [TestCase(new double[] { -5, -1.5, 10 }, new double[] { -4 }, Result = "-x^0 - 1.5*x^1 + 10*x^2")]
        [TestCase(new double[] { 0, 1, 10 }, new double[] { 4 }, Result = "-4*x^0 + x^1 + 10*x^2")]
        [TestCase(new double[] { 0, 1, 10 }, new double[] { 0, 0, 2, 4 }, Result = "x^1 + 8*x^2 - 4*x^3")]
        [TestCase(new double[] { 0 }, new double[] { 0, 0, 2, 4 }, Result = "-2*x^2 - 4*x^3")]
        public string Substruct_Test(double[] lhs, double[] rhs)
        {
            return (new Polynom(lhs) - new Polynom(rhs)).ToString();
        }

        [TestCase(new double[] { 0, 1 }, new double[] { 1, 1, 1, 1 }, Result = "x^1 + x^2 + x^3 + x^4")]
        [TestCase(new double[] { 1, 1, 1, 1 }, new double[] { 0, 1 }, Result = "x^1 + x^2 + x^3 + x^4")]
        [TestCase(new double[] { 1, 1 }, new double[] { 1, 1 }, Result = "x^0 + 2*x^1 + x^2")]
        [TestCase(new double[] { -1, 1 }, new double[] { 1, 1 }, Result = "-x^0 + x^2")]
        [TestCase(new double[] { -1, -1 }, new double[] { -1, -1 }, Result = "x^0 + 2*x^1 + x^2")]
        [TestCase(new double[] { 15, -1 }, new double[] { -10, -1 }, Result = "-150*x^0 - 5*x^1 + x^2")]
        [TestCase(new double[] { 0, 0, 1, 0, 1 }, new double[] { 0, 1, 0, 1 }, Result = "x^3 + 2*x^5 + x^7")]
        [TestCase(new double[] { 0 }, new double[] { 0, 0, 2, 4 }, Result = "")]
        public string Multiply_Test(double[] lhs, double[] rhs)
        {
            return (new Polynom(lhs) * new Polynom(rhs)).ToString();
        }
        #endregion

        #region GetHashCode Tests

        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1, 1, 1 }, Result = false)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1, 0, 0 }, Result = true)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, Result = true)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 0 }, Result = false)]
        public bool GetHashCode_DifferentObjects(double[] coefficients1, double[] coefficients2)
        {
            int hash1 = new Polynom(coefficients1).GetHashCode();
            int hash2 = new Polynom(coefficients2).GetHashCode();
            Debug.WriteLine(hash1);
            Debug.WriteLine(hash2);
            return  hash1 == hash2;
        }

        [TestCase(new double[] { 0, 1 }, Result = true)]
        public bool GetHashCode_TheSameObject(double[] coefficients)
        {
            Polynom polynom = new Polynom(coefficients);
            int hash1 = polynom.GetHashCode();
            int hash2 = polynom.GetHashCode();
            return hash1 == hash2;
        }
        #endregion

        #region Equals Tests
        [TestCase(new double[] { 0, 1 }, Result = true)]
        public bool Equals_TheSameObject(double[] coefficients)
        {
            Polynom polynom = new Polynom(coefficients);
            return polynom.Equals(polynom);
        }

        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1, 1, 1 }, Result = false)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1, 0, 0 }, Result = true)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, Result = true)]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 0 }, Result = false)]
        public bool Equals_DifferentObjects(double[] coefficients1, double[] coefficients2)
        {
            Polynom polynom = new Polynom(coefficients1);
            return polynom.Equals(new Polynom(coefficients2));
        }

        [TestCase(new double[] { 0, 1 }, Result = false)]
        public bool Equals_WithNull(double[] coefficients)
        {
             Polynom polynom = new Polynom(coefficients);
             return polynom.Equals(null);
        }

        [TestCase(new double[] { 0, 1 }, Result = false)]
        [TestCase(5, Result = false)]
        public bool Equals_WithObjectParametr(object parametr)
        {
            Polynom polynom = new Polynom(1, 2, 3, 4);
            return polynom.Equals(null);
        }
        #endregion

    }
}
