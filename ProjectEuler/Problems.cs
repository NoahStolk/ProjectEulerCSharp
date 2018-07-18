using System;
using System.Collections.Generic;
using XtendedNET.Extensions;
using XtendedNET.Utils;

namespace ProjectEuler
{
	public class Problems
	{
		public static int Problem1()
		{
			int result = 0;
			for (int i = 1; i < 1000; i++)
				if (i % 3 == 0 || i % 5 == 0)
					result += i;
			return result;
		}

		public static int Problem2()
		{
			int a, b, c, d;
			a = 0;
			b = 1;
			c = 0;
			d = 0;

			while (c < 4000000)
			{
				if (c % 2 == 0)
					d += c;
				c = a + b;
				a = b;
				b = c;
			}

			return d;
		}

		public static double Problem3()
		{
			double a = 600851475143;
			double b = 2;

			while (a > 1)
			{
				if (a % b == 0)
					a /= b;
				else
					b++;
			}

			return b;
		}

		public static int Problem4()
		{
			int largest = 0;

			int x = 0;
			for (int i = 100; i < 1000; i++)
			{
				for (int j = 100; j < 1000; j++)
				{
					x = i * j;
					
					if (x.ToString() == x.ToString().Reverse())
						if (x > largest)
							largest = x;
				}
			}

			return largest;
		}

		public static long Problem5()
		{
			long intCount = 20;
			long highest = 0;
			long x = 20;
			long z = 0;
			bool found = false;

			while (!found)
			{
				for (int y = 1; y < 21; y++)
				{
					if (x % y == 0)
						z++;
				}

				if (z > highest)
					highest = z;

				if (z >= intCount)
				{
					found = true;
				}
				else
				{
					z = 0;
					x += 20;
				}
			}

			return x;
		}

		public static double Problem6()
		{
			int x = 100;
			double a = 0;
			double b = 0;

			for (int i = 1; i <= x; i++)
			{
				a += Math.Pow(i, 2);
				b += i;
			}

			b = Math.Pow(b, 2);

			return b - a;
		}

		public static int Problem7()
		{
			bool found = false;
			int number = 2;
			int primes = 0;

			while (!found)
			{
				if (MathUtils.IsPrime(number))
					primes++;

				if (primes == 10001)
					found = true;

				if (!found)
				{
					if (number % 2 == 0)
						number++;
					else
						number += 2;
				}
			}

			return number;
		}

		public static double Problem9()
		{
			double a = 0;
			double num = 1000;
			double ans = 0;

			for (int i = 1; i < num / 2; i++)
			{
				a = (Math.Pow(num, 2) / 2 - num * i) / (num - i);
				if (a % 1 == 0)
					ans = a * i * (num - a - i);
			}

			return ans;
		}

		public static double Problem10()
		{
			List<double> primes = new List<double>();
			double total = 2;

			for (double i = 3; i < 2000001; i += 2)
			{
				bool c = false;
				foreach (double p in primes)
				{
					if (i % p == 0)
						c = true;
				}

				if (c)
					continue;

				double z = 0;
				for (double j = 1; j < Math.Ceiling(Math.Sqrt(i)); j++)
				{
					if (i % j == 0)
						z++;
					if (i % Math.Ceiling(i / j) == 0)
						z++;
				}

				if (Math.Sqrt(i) == Math.Floor(Math.Sqrt(i)))
					z++;

				if (z == 2)
				{
					total += i;
					if (i < 1000)
						primes.Add(i);
				}
			}

			return total;
		}

		public static int Problem179(int limit)
		{
			int result = 0;
			int[] divisors = new int[2] { 0, 2 };
			for (int i = 2; i < limit; i++)
			{
				//if (Math.Sqrt(i) == (int)Math.Floor((double)i))
				//	continue;

				divisors[0] = divisors[1];
				divisors[1] = MathUtils.GetPositiveDivisors(i + 1);

				if (divisors[0] == divisors[1])
				{
					result++;
					//Console.WriteLine(divisors[0]);
				}
			}
			return result;
		}
	}
}