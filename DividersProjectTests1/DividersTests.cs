using Microsoft.VisualStudio.TestTools.UnitTesting;
using DividersProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.VisualBasic;

namespace DividersProject.Tests
{
    [TestClass()]
    public class DividersTests
    {
        [TestMethod()]
        public void IsDividerSimpleTest()
        {
            MyBigInteger n = new MyBigInteger(10);
            MyBigInteger d = new MyBigInteger(2);

            bool res = Dividers.IsDivider(n, d);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void AllDividers_N_Plus()
        {
            MyBigInteger n =  new MyBigInteger(24);
           
            BigInteger[] expectedDividers = { 1, 2, 3, 4, 6, 8, 12, 24 };
            MyBigInteger[] dividers = Dividers.AllDividers(n);
            Array.Sort(dividers);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod]
        public void AllDividers_Zero()
        {
            MyBigInteger n  = new MyBigInteger(0);
            long[] expectedDividers = { };
            long[] expectedPowers = { };
            
            MyBigInteger[] dividers = Dividers.AllDividers(n);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod]
        public void AllDividers_N_Minus()
        {
            MyBigInteger n = new MyBigInteger(-10);
            long[] expectedDividers = { };
            long[] expectedPowers = { };

            MyBigInteger[] dividers = Dividers.AllDividers(n);

            CollectionAssert.AreEqual(expectedDividers, dividers);
        }

        [TestMethod()]
        public void IsPrimeTest_True()
        {
            MyBigInteger prime =new MyBigInteger(17);

            bool result = Dividers.IsPrime(prime);
            Assert.IsTrue(result);

        }
        [TestMethod()]
        public void IsPrimeTest_False()
        {
            MyBigInteger prime = new MyBigInteger(24);

            bool result = Dividers.IsPrime(prime);
            Assert.IsFalse(result);
        }
        [TestMethod()]
        public void IsPrimeTest_Minus()
        {
            MyBigInteger prime = new MyBigInteger(-17);
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
            MyBigInteger prime = new MyBigInteger(0);

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
            MyBigInteger prime = new MyBigInteger(1);

            bool result = Dividers.IsPrime(prime);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllPrimes_revers()
        {
            MyBigInteger start = new MyBigInteger(20);
            MyBigInteger end = new MyBigInteger(10);

            MyBigInteger[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] { }, result);
        }

        [TestMethod]
        public void AllPrimes()
        {
            MyBigInteger start = new MyBigInteger(10);
            MyBigInteger end = new MyBigInteger(30);
            long[] expectedPrimes = { 11, 13, 17, 19, 23, 29 };

            MyBigInteger[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(expectedPrimes, result);
        }

        [TestMethod]
        public void AllPrimes_Minus()
        {
            MyBigInteger start = new MyBigInteger(-10);
            MyBigInteger end = new MyBigInteger(-1);

            MyBigInteger[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] { }, result);
        }

        [TestMethod]
        public void AllPrimes_One()
        {
            MyBigInteger start = new MyBigInteger(7);
            MyBigInteger end = new MyBigInteger(7);

            MyBigInteger[] result = Dividers.AllPrimes(start, end);

            CollectionAssert.AreEqual(new long[] {7}, result);
        }

        [TestMethod]
        public void Factoring_Plus()
        {
            MyBigInteger n = new MyBigInteger(24);
            long[] expectedFactors = { 2, 3 };
            long[] expectedPowers = { 3, 1 };

            (MyBigInteger[] factors, MyBigInteger[] powers) = Dividers.Factorize(n);

            CollectionAssert.AreEqual(expectedFactors, factors);
            CollectionAssert.AreEqual(expectedPowers, powers);
        }

        [TestMethod]
        public void Factoring_Zero()
        {
            MyBigInteger n = new MyBigInteger(0);
            try
            {
                (MyBigInteger[] factors, MyBigInteger[] powers) = Dividers.Factorize(n);
            } catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Factoring_Minus()
        {
            MyBigInteger n = new MyBigInteger(-10);
            try
            {
                (MyBigInteger[] factors, MyBigInteger[] powers) = Dividers.Factorize(n);
            } catch(Exception e)
            {
                Assert.IsTrue(true);
            }

        }

        [TestMethod]
        public void Factoring_ForOne()
        {
            MyBigInteger n = new MyBigInteger(1);
            int[] expectedFactors = { };
            int[] expectedPowers = { };

            (MyBigInteger[] factors, MyBigInteger[] powers) = Dividers.Factorize(n);

            CollectionAssert.AreEqual(expectedFactors, factors);
            CollectionAssert.AreEqual(expectedPowers, powers);
        }

        [TestMethod()]
        public void FindNumsWithDividersTest()
        {   
            MyBigInteger first = new MyBigInteger(3);
            MyBigInteger second = new MyBigInteger(1);
            MyBigInteger third = new MyBigInteger(100);

            MyBigInteger[] allNums = Dividers.FindNumsWithDividers(first, second, third);
            long[] expectedNums = { 16, 81 };
            CollectionAssert.AreEqual(allNums, expectedNums);
        }
    }

}