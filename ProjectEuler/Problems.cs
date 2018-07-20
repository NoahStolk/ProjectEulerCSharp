using System;
using System.Collections.Generic;
using XtendedNET.Extensions;

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
				if (number.IsPrime())
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

		public static int Problem18()
		{
			const int triangleSize = 15;

			int[][] matrix =
			{
				new int[] { 75 },
				new int[] { 95, 64 },
				new int[] { 17, 47, 82 },
				new int[] { 18, 35, 87, 10 },
				new int[] { 20, 4, 82, 47, 65 },
				new int[] { 19, 1, 23, 75, 3, 34 },
				new int[] { 88, 2, 77, 73, 7, 63, 67 },
				new int[] { 99, 65, 4, 28, 6, 16, 70, 92 },
				new int[] { 41, 41, 26, 56, 83, 40, 80, 70, 33 },
				new int[] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 },
				new int[] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 },
				new int[] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 },
				new int[] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 },
				new int[] { 63, 66, 4, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 },
				new int[] { 4, 62, 98, 27, 23, 9, 70, 98, 73, 93, 38, 53, 60, 4, 23 }
			};

			for (int y = triangleSize - 2; y > -1; y--)
			{
				for (int x = 0; x < y + 1; x++)
				{
					if (matrix[y + 1][x] < matrix[y + 1][x + 1])
						matrix[y][x] += matrix[y + 1][x + 1];
					else
						matrix[y][x] += matrix[y + 1][x];
				}
			}

			return matrix[0][0];
		}

		public static int Problem37()
		{
			int sum = 0;
			int primes = 0;

			int j = 11;
			while (primes < 11)
			{
				if (j.IsTruncatablePrime())
				{
					primes++;
					sum += j;
				}
				j += 2;
			}

			return sum;
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
				divisors[1] = (i + 1).GetPositiveDivisors();

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