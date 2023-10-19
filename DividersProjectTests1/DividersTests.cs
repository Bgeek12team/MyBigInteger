using Microsoft.VisualStudio.TestTools.UnitTesting;
using DividersProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace DividersProject.Tests
{
    [TestClass()]
    public class DividersTests
    {
        [TestMethod()]
        public void IsDividerSimpleTest()
        {
            int n = 10;
            int d = 2;

            bool res = Dividers.IsDivider(n, d);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void AllDividers_N_Plus()
        {
            int n = 24;

            BigInteger[] expectedDividers = { 1, 2, 3, 4, 6, 8, 12, 24 };
            BigInteger[] dividers = Dividers.AllDividers(n);
            Array.Sort(dividers);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod]
        public void AllDividers_Zero()
        {
            int n = 0;
            long[] expectedDividers = { };
            long[] expectedPowers = { };

            BigInteger[] dividers = Dividers.AllDividers(n);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod]
        public void AllDividers_N_Minus()
        {
            int n = -10;
            long[] expectedDividers = { };
            long[] expectedPowers = { };

            BigInteger[] dividers = Dividers.AllDividers(n);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod()]
        public void IsPrimeTest_True()
        {
            int prime = 17;

            bool result = Dividers.IsPrime(prime);
            Assert.IsTrue(result);

        }
        [TestMethod()]
        public void IsPrimeTest_False()
        {
            int prime = 24;

            bool result = Dividers.IsPrime(prime);
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void IsPrimeTest_Minus()
        {
            int prime = -17;
            try
            {
                bool result = Dividers.IsPrime(prime);
            } catch (Exception e)
            {
                Assert.IsTrue(true);
            }

        }
        [TestMethod()]
        public void IsPrimeTest_Zero()
        {
            int prime = 0;

            try
            {
                bool result = Dividers.IsPrime(prime);
            } catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod()]
        public void IsPrimeTest_One()
        {
            int prime = 1;

            bool result = Dividers.IsPrime(prime);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllPrimes_revers()
        {
            int start = 20;
            int end = 10;

            long[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] { }, result);
        }

        [TestMethod]
        public void AllPrimes()
        {
            int start = 10;
            int end = 30;
            long[] expectedPrimes = { 11, 13, 17, 19, 23, 29 };

            long[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(expectedPrimes, result);
        }

        [TestMethod]
        public void AllPrimes_Minus()
        {
            int start = -10;
            int end = -1;

            long[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] { }, result);
        }

        [TestMethod]
        public void AllPrimes_One()
        {
            int start = 7;
            int end = 7;

            long[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] {7}, result);
        }

        [TestMethod]
        public void Factoring_Plus()
        {
            int n = 24;
            long[] expectedFactors = { 2, 3 };
            long[] expectedPowers = { 3, 1 };

            (long[] factors, long[] powers) = Dividers.Factorize(n);

            CollectionAssert.AreEqual(expectedFactors, factors);
            CollectionAssert.AreEqual(expectedPowers, powers);
        }

        [TestMethod]
        public void Factoring_Zero()
        {
            int n = 0;
            try
            {
                (long[] factors, long[] powers) = Dividers.Factorize(n);
            } catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Factoring_Minus()
        {
            int n = -10;
            try
            {
                (long[] factors, long[] powers) = Dividers.Factorize(n);
            } catch(Exception e)
            {
                Assert.IsTrue(true);
            }

        }

        [TestMethod]
        public void Factoring_ForOne()
        {
            int n = 1;
            int[] expectedFactors = { };
            int[] expectedPowers = { };

            (long[] factors, long[] powers) = Dividers.Factorize(n);

            CollectionAssert.AreEqual(expectedFactors, factors);
            CollectionAssert.AreEqual(expectedPowers, powers);
        }

        [TestMethod()]
        public void FindNumsWithDividersTest()
        {
            long[] allNums = Dividers.FindNumsWithDividers(3, 1, 100);
            long[] expectedNums = { 16, 81 };
            CollectionAssert.AreEqual(allNums, expectedNums);
        }
    }

}