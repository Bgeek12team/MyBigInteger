using DividersProject;
namespace P
{
    class p
    {
        
        public static void Main()
        {
            MyBigInteger n = new MyBigInteger("24");
            MyBigInteger m = new MyBigInteger("1000");
            MyBigInteger[] primes = Dividers.AllPrimes(n, m);
            foreach (var p in primes)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine(n / 2);


        }
    }
}