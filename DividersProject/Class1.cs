using System.Collections;
using System.Collections.Generic;
using System.Numerics;
namespace DividersProject
{
    /// <summary>
    /// Модуль, позволяющий работать с натуральными числами и их делителями
    /// </summary>
    public static class Dividers
    {
        /// <summary>
        /// Определяет, является ли натуральное число d
        /// делителем натурального числа n
        /// </summary>
        /// <param name="n">Проверяемое число в диапозоне от 1</param>
        /// <param name="d">Проверяемый делитель в диапозоне от 1</param>
        /// <returns>
        /// True: n - делитель d,
        /// False: n - не делитель d
        /// </returns>
        public static bool IsDivider(MyBigInteger n, MyBigInteger d)
        {
            return n % d == 0;
        }

        /// <summary>
        /// Находит разложение натурального числа на простые делители и их степени
        /// </summary>
        /// <param name="n">Проверяемое натуральное число от 2</param>
        /// <returns>
        /// Кортеж, состоящий из двух массивов:
        /// массив целочисленных положительных делителей
        /// и массив целочисленных положительных степеней соответсвующих делителей
        /// </returns>
        public static (MyBigInteger[], MyBigInteger[]) Factorize(MyBigInteger n)
        {

            List<MyBigInteger> dividers = new List<MyBigInteger>();
            List<MyBigInteger> powers = new List<MyBigInteger>();
            MyBigInteger[] primes = AllPrimes(new MyBigInteger("2"), n);
            long i = 0;
            int k = 0;
            while (n > 1)
            {
                if (n % primes[i] == 0)
                {
                    dividers.Add(primes[i]);
                    powers.Add(new MyBigInteger(0));
                    while (n % primes[i] == 0)
                    {
                        powers[k]++;
                        n /= primes[i];
                    }
                    k++;
                }
                i++;
            }
            return (dividers.ToArray(), powers.ToArray());
        }

        /// <summary>
        /// Проверяет, является ли данное натуральное число простым
        /// </summary>
        /// <param name="n">Проверяемое натуральное число от 1</param>
        /// <returns>
        /// True: число простое
        /// False: число составное
        /// </returns>
        public static bool IsPrime(MyBigInteger p)
        {
            return AllDividers(p).Length == 0;
        }

        /// <summary>
        /// Получает список всех натуральных делителей
        /// данного натурального числа
        /// </summary>
        /// <param name="n">
        /// Натуральное число, список делителей которого необходимо получить,
        /// в диапозоне от 1
        /// </param>
        /// <returns>Массив всех делителей числа</returns>
        public static MyBigInteger[] AllDividers(MyBigInteger n)
        {
            List<MyBigInteger> result = new List<MyBigInteger>();

            for (int i = 1; i <= MyBigInteger.Sqrt(n); i++)
            {
                if (Dividers.IsDivider(n, new MyBigInteger(i)))
                {
                    result.Add(new MyBigInteger(i));
                    result.Add(n / i);
                }
            }

            return result.Distinct().ToArray();
        }
        /// <summary>
        /// Находит все простые числа на отрезке 
        /// от данного натурального d 
        /// до данного натурального n
        /// с помощью метода сегментированного решета Аткина
        /// </summary>
        /// <param name="d">
        /// Начало проверяемого отрезка,
        /// натуральное число от 2
        /// </param>
        /// <param name="n">
        /// Конец проверяемого отрезка,
        /// натуральное число от n
        /// </param>
        /// <returns>Массив простых чисел на отрезке [d; n]</returns>
        public static MyBigInteger[] AllPrimes(MyBigInteger start, MyBigInteger end)
        {
            const int buffer = 100; //размер сегмента в алгоритме
            List<MyBigInteger> primesList = new List<MyBigInteger>();
            if (start <= 2)
            {
                primesList.Add(new MyBigInteger(2));
                primesList.Add(new MyBigInteger(3));
            }
            else if (start == 3)
            {
                primesList.Add(new MyBigInteger(3));
            }
            MyBigInteger startSegment = start;
            MyBigInteger endSegment = start + buffer;
            while (endSegment < end + buffer)
            {
                SieveSegment(startSegment, MyBigInteger.Min(endSegment, end), primesList);
                startSegment = endSegment + 1;
                endSegment += buffer;
            }

            return primesList.ToArray();
        }
        
        /// <summary>
        /// Обработка сегмента заданного размера в алгоритме решета Аткина
        /// </summary>
        /// <param name="start">Начало сегмента</param>
        /// <param name="end">Конец сегмента</param>
        /// <param name="primesList">Список простых чисел, найденных в предидущих сегментах</param>
        public static void SieveSegment(MyBigInteger start, MyBigInteger end, List<MyBigInteger> primesList)
        {
            MyBigInteger size = end - start + 1;
            bool[] isPrime = new bool[(int)size];
            MyBigInteger sqrt = MyBigInteger.Sqrt(end);

            for (MyBigInteger x = new MyBigInteger(1); x <= sqrt; x++)
            {
                for (MyBigInteger y = new MyBigInteger(1); y <= sqrt; y++)
                {
                    MyBigInteger n = (4 * x * x) + (y * y);
                    if (n >= start && n <= end && (n % 12 == 1 || n % 12 == 5))
                    {
                        isPrime[(int)(n - start)] = !isPrime[(int)(n - start)];
                    }

                    n = (3 * x * x) + (y * y);
                    if (n >= start && n <= end && n % 12 == 7)
                    {
                        isPrime[(int)(n - start)] = !isPrime[(int)(n - start)];
                    }

                    n = (3 * x * x) - (y * y);
                    if (x > y && n >= start && n <= end && n % 12 == 11)
                    {
                        isPrime[(int)(n - start)] = !isPrime[(int)(n - start)];
                    }
                }
            }
            for (MyBigInteger n = start + 2; n <= sqrt; n++)
            {
                if (isPrime[(int)(n - start)])
                {
                    MyBigInteger x = n * n;
                    for (MyBigInteger i = x; i <= end; i += x)
                    {
                        isPrime[(int)(i - start)] = false;
                    }
                }
            }

            for (MyBigInteger i = start; i <= end; i++)
            {
                if (isPrime[(int)(i - start)])
                {
                    primesList.Add(i);
                }
            }
        }
        /// <summary>
        /// Находит все простые числа на отрезке 
        /// от данного натурального d 
        /// до данного натурального n
        /// методом перебора
        /// </summary>
        /// <param name="d">
        /// Начало проверяемого отрезка,
        /// натуральное число от 1 до Int.MaxValue
        /// </param>
        /// <param name="n">
        /// Конец проверяемого отрезка,
        /// натуральное число от n до Int.MaxValue
        /// </param>
        /// <returns>Массив простых чисел на отрезке</returns>
        public static MyBigInteger[] AllPrimesByCheckingAll(MyBigInteger d, MyBigInteger n)
        {
            List<MyBigInteger> numbers = new List<MyBigInteger>();
            
            for (MyBigInteger i = d; i<=n; i++)
            {
                if (IsPrime(n))
                    numbers.Add(n);
            }

            return numbers.ToArray();
        }
        /// <summary>
        /// Находит на заданном отрезке количество чисел 
        /// со строго определенным количеством небазовых делителей
        /// </summary>
        /// <param name="amountOfDividers">
        /// Заданное целое количество делителей,
        /// </param>
        /// <param name="start">
        /// Начало проверяемого отрезка,
        /// натуральное число от 1 до Int.MaxValue</param>
        /// <param name="end">
        /// Конец проверяемого отрезка,
        /// натуральное число от start до Int.MaxValue
        /// </param>
        /// <returns>
        /// Массив чисел, находящихся на данном отрезке,
        /// с определенным количеством делителей
        /// </returns>
        public static MyBigInteger[] FindNumsWithDividers (MyBigInteger amountOfDividers, MyBigInteger start, MyBigInteger end)
        {
            if (amountOfDividers == 3)
                return Dividers.FindNumsWithThreeDividers(start, end);

            List<MyBigInteger> list = new List<MyBigInteger>();
            for (MyBigInteger k = start; k < end; k++)
            {
                MyBigInteger[] divider = AllDividers(k);
                if (divider.Length == amountOfDividers + 2)
                    list.Add(k);
            }
            return list.ToArray();
        }
        /// <summary>
        /// Находит на заданном отрезке количество чисел 
        /// с ровно тремя небазовыми делителями
        /// на основе того факта, что ровно 3 делителя имеют только
        /// полные 4-ые степени простых чисел
        /// </summary>
        /// <param name="start">
        /// Начало проверяемого отрезка,
        /// натуральное число от 1</param>
        /// <param name="end">
        /// Конец проверяемого отрезка,
        /// натуральное число от start
        /// </param>
        /// <returns>
        /// Массив чисел, находящихся на данном отрезке,
        /// с ровно тремя небазовыми делителями
        /// </returns>
        public static MyBigInteger[] FindNumsWithThreeDividers (MyBigInteger start, MyBigInteger end)
        {
            List<MyBigInteger> list = new();
            foreach (MyBigInteger i in AllPrimes(start, (MyBigInteger.Sqrt(MyBigInteger.Sqrt(end))) + 1 ) )
            {
                MyBigInteger sqare = i * i;
                MyBigInteger fourth = sqare * sqare;
                list.Add(fourth);
            }
            return list.ToArray();
        }
    }
}