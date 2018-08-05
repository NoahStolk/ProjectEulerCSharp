using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using XtendedNET.Extensions;
using XtendedNET.Utils;

namespace ProjectEuler
{
	public class Problems
	{
		public static int Problem1()
		{
			int result = 0;
			for (int i = 3; i < 1000; i++)
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

		public static int Problem3()
		{
			long a = 600851475143;
			int b = 2;

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
			int intCount = 20;

			long x = intCount;
			for (; ; )
			{
				Restart:
				for (int y = 1; y <= intCount; y++)
				{
					if (x % y != 0)
					{
						x += intCount;
						goto Restart;
					}
				}

				return x;
			}
		}

		public static int Problem6()
		{
			int x = 100;
			int a = 0;
			int b = 0;

			for (int i = 1; i <= x; i++)
			{
				a += (int)Math.Pow(i, 2);
				b += i;
			}

			b = (int)Math.Pow(b, 2);

			return b - a;
		}

		public static int Problem7()
		{
			int primes = 1;
			int number = 3;
			for (; ; )
			{
				if (number.IsPrime())
					primes++;

				if (primes == 10001)
					return number;

				number += 2;
			}
		}

		public static long Problem8()
		{
			string digits = File.ReadAllText(Path.Combine("Resources", "008.txt"));

			int adjacents = 13;
			int length = digits.Length;

			long highest = 0;
			for (int i = 0; i < length - adjacents; i++)
			{
				long product = 1;
				for (int j = 0; j < adjacents; j++)
					product *= digits.DigitAt(i + j);
				if (highest < product)
					highest = product;
			}

			return highest;
		}

		public static long Problem9()
		{
			double a = 0;
			int num = 1000;
			long ans = 0;

			for (int i = 1; i < num / 2; i++)
			{
				a = (Math.Pow(num, 2) / 2 - num * i) / (num - i);
				if (a % 1 == 0)
					ans = (long)(a * i * (num - a - i));
			}

			return ans;
		}

		public static long Problem10()
		{
			int[] primes = new int[256];
			int primeIndex = 0;
			long total = -1;

			for (int i = 3; i < 2000000; i += 2)
			{
				RestartOuter:
				for (int j = 0; j < primeIndex; j++)
				{
					if (i % primes[j] == 0)
					{
						i += 2;
						goto RestartOuter;
					}
				}

				if (i.IsPrime())
				{
					total += i;
					if (primeIndex < primes.Length)
					{
						primes[primeIndex] = i;
						primeIndex++;
					}
				}
			}

			return total;
		}

		public static int Problem11()
		{
			int w = 20;
			int h = 20;
			int[][] matrix = Utils.GenerateMatrix(11);

			int highest = 0;

			int product = 0;
			for (int x = 0; x < h; x++)
				for (int y = 0; y < w - 3; y++)
				{
					product = matrix[x][y] * matrix[x][y + 1] * matrix[x][y + 2] * matrix[x][y + 3];
					if (product > highest)
						highest = product;
				}

			for (int x = 0; x < h; x++)
				for (int y = 0; y < w - 3; y++)
				{
					product = matrix[y][x] * matrix[y + 1][x] * matrix[y + 2][x] * matrix[y + 3][x];
					if (product > highest)
						highest = product;
				}

			for (int x = 0; x < h - 3; x++)
				for (int y = 0; y < w - 3; y++)
				{
					product = matrix[x][y] * matrix[x + 1][y + 1] * matrix[x + 2][y + 2] * matrix[x + 3][y + 3];
					if (product > highest)
						highest = product;
				}

			for (int x = 0; x < h - 3; x++)
				for (int y = 0; y < w - 3; y++)
				{
					product = matrix[x + 3][y] * matrix[x + 2][y + 1] * matrix[x + 1][y + 2] * matrix[x][y + 3];
					if (product > highest)
						highest = product;
				}

			return highest;
		}

		public static int Problem12()
		{
			int n = 2;
			int triangle = 3;

			for (; ; )
			{
				if (triangle.GetDivisorAmount() > 500)
					break;

				n++;
				triangle += n;
			}

			return triangle;
		}

		public static string Problem13()
		{
			string[] digits = File.ReadAllLines(Path.Combine("Resources", "013.txt"));
			string total = "0";
			for (int x = 0; x < digits.Length; x++)
				total = total.NumeralAddition(digits[x]);

			return total.Substring(0, 10);
		}

		public static int Problem14()
		{
			int highestX = 0;
			int highestZ = 0;

			for (int x = 1; x < 1000001; x++)
			{
				double y = x;
				int z = 0;

				while (y > 1)
				{
					if (y % 2 == 0)
					{
						y *= 0.5;
					}
					else
					{
						y *= 3;
						y++;
					}
					z++;
				}

				if (z > highestZ)
				{
					highestZ = z;
					highestX = x;
				}
			}

			return highestX;
		}

		public static BigInteger Problem15()
		{
			BigInteger grid = 20;

			return ((grid * 2).Factorial() / grid.Factorial()) / grid.Factorial();
		}

		public static int Problem16()
		{
			List<int> digits = 1000.PowersOfTwo();

			int total = 0;
			for (int i = 0; i < digits.Count; i++)
				total += digits[i];

			return total;
		}

		public static int Problem17()
		{
			int letters = 0;

			for (int x = 1; x <= Math.Pow(10, 3); x++)
				letters += x.GetWord().Length;

			return letters;
		}

		public static int Problem18()
		{
			int[][] matrix = Utils.GenerateMatrix(18);

			for (int y = matrix.GetLength(0) - 2; y > -1; y--)
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

		public static int Problem19()
		{
			int s = 0;
			for (int i = 1901; i < 2001; i++)
				for (int j = 1; j < 13; j++)
					if (new DateTime(i, j, 1).DayOfWeek == DayOfWeek.Sunday)
						s++;
			return s;
		}

		public static BigInteger Problem20()
		{
			int number = 100;
			BigInteger total = 1;

			for (int x = 1; x < number + 1; x++)
				total *= x;

			List<int> digits = new List<int>();
			foreach (char c in total.ToString())
			{
				string s = c.ToString();
				digits.Add(int.Parse(s));
			}

			total = 0;
			foreach (int i in digits)
				total += i;

			return total;
		}

		public static int Problem21()
		{
			int t = 0;

			for (int x = 1; x < 10001; x++)
			{
				int d = 0;
				int e = 0;

				for (int y = 1; y < Math.Floor((double)x / 2) + 1; y++)
					if (x % y == 0)
						d += y;

				for (int z = 1; z < Math.Floor((double)d / 2) + 1; z++)
					if (d % z == 0)
						e += z;

				if (e == x && d != x)
					t += e;
			}

			return t;
		}

		public static int Problem22()
		{
			int totalScore = 0;
			string text = File.ReadAllText(Path.Combine("Resources", "022.txt"));
			string[] names = text.Replace("\"", "").Split(',');
			Array.Sort(names);

			for (int x = 0; x < names.Length; x++)
			{
				int score = 0;
				int letterScore = 0;

				for (int y = 0; y < names[x].Length; y++)
					letterScore += names[x][y] - 64;

				score = letterScore * (x + 1);

				totalScore += score;
			}

			return totalScore;
		}
		
		public static int Problem23()
		{
			int max = 28124;

			List<int> abundants = new List<int>();
			for (int i = 1; i < max; i++)
				if (i < i.GetProperDivisorSum())
					abundants.Add(i);

			int result = 0;
			for (int i = 1; i < max; i++)
			{
				bool writtenAsTwo = false;
				for (int j = 0; j < abundants.Count; j++)
				{
					if (abundants.Contains(i - abundants[j]))
					{
						writtenAsTwo = true;
						break;
					}
				}

				if (!writtenAsTwo)
					result += i;
			}
			return result;
		}

		public static string Problem24()
		{
			string digits = "0123456789";

			int i = 0;
			foreach (string permutation in digits.GetPermutations(""))
			{
				i++;
				if (i == Math.Pow(10, 6))
					return permutation;
			}

			return "";
		}

		public static int Problem25()
		{
			string a = "0";
			string b = "1";
			string c = "0";
			int d = 1;

			while (c.Length < 1000)
			{
				d++;
				c = a.NumeralAddition(b);
				a = b;
				b = c;
			}

			return d;
		}

		public static int Problem26()
		{
			int longest = 0;
			int hasLongest = 0;
			for (int i = 2; i < 1000; i++)
			{
				Restart:
				BigInteger d = BigInteger.Pow(10, 2000) / i;
				string s = d.ToString();

				for (int j = 0; j < 10; j++)
				{
					if (s.Contains($"{j}{j}{j}{j}"))
					{
						i++;
						goto Restart;
					}
				}
				
				StringBuilder cycle = new StringBuilder();
				for (int j = 0; j < s.Length; j++)
				{
					cycle.Append(s[j]);
					string c = cycle.ToString();
					if (s.SubstringSafe(j + 1, c.Length) == c)
					{
						if (longest < cycle.Length)
						{
							longest = cycle.Length;
							hasLongest = i;
						}
						break;
					}
				}
			}
			return hasLongest;
		}

		public static int Problem28()
		{
			int grid = 1001;
			int diagonals = grid * 2 - 1;

			int sum = 1;
			int multiplier = 2;
			int i = 0;
			int total = 0;

			while (i < diagonals)
			{
				total += sum;
				sum += multiplier;
				i++;
				if (i % 4 == 0)
					multiplier += 2;
			}

			return total;
		}

		public static int Problem29()
		{
			List<double> sequence = new List<double>();

			for (int x = 2; x < 101; x++)
				for (int y = 2; y < 101; y++)
					sequence.Add(Math.Pow(x, y));

			sequence = sequence.Distinct().ToList();

			return sequence.Count;
		}

		public static int Problem30()
		{
			int t = 0;

			for (int x = 10; x < 999999; x++)
			{
				int sum = 0;
				for (int y = 0; y < x.ToString().Length; y++)
					sum += (int)Math.Pow(x.DigitAt(y), 5);
				if (x == sum)
					t += x;
			}

			return t;
		}

		public static int Problem31()
		{
			int a = 200;
			int b = 100;
			int c = 50;
			int d = 20;
			int e = 10;
			int f = 5;
			int g = 2;
			int h = 1;
			int count = 0;

			for (int i = 1; i > -1; i--)
				for (int j = 2 - i * 2; j > -1; j--)
					for (int k = 4 - i * 4 - j * 2; k > -1; k--)
						for (double l = 10 - i * 10 - j * 5 - Math.Floor(k * 2.5); l > -1; l--)
							for (double m = 20 - i * 20 - j * 10 - k * 5 - l * 2; m > -1; m--)
								for (double n = 40 - i * 40 - j * 20 - k * 10 - l * 4 - m * 2; n > -1; n--)
									for (double o = 100 - i * 100 - j * 50 - k * 25 - l * 10 - m * 5 - Math.Floor(n * 2.5); o > -1; o--)
										for (double p = 200 - i * 200 - j * 100 - k * 50 - l * 20 - m * 10 - n * 5 - o * 2; p > -1; p--)
											if (a * i + b * j + c * k + d * l + e * m + f * n + g * o + h * p == 200)
												count++;

			return count;
		}

		public static int Problem32()
		{
			List<int> answers = new List<int>();

			for (int i = 123456789; i < 987654321 / 2; i += 9)
			{
				if (!i.IsPandigital(false))
					continue;

				string x = i.ToString();

				int mul = int.Parse(x.Substring(5));
				if (int.Parse(x.Substring(0, 2)) * int.Parse(x.Substring(2, 3)) == mul
				 || int.Parse(x.Substring(0, 3)) * int.Parse(x.Substring(3, 2)) == mul
				 || int.Parse(x.Substring(0, 1)) * int.Parse(x.Substring(1, 4)) == mul
				 || int.Parse(x.Substring(0, 4)) * int.Parse(x.Substring(4, 1)) == mul)
					answers.Add(mul);
			}

			answers = answers.Distinct().ToList();

			int total = 0;
			foreach (int i in answers)
				total += i;
			return total;
		}

		public static double Problem33()
		{
			List<double> fractions = new List<double>();

			for (double i = 10; i < 100; i++)
			{
				if (i % 10 == 0)
					continue;

				for (double j = 10; j < i; j++)
				{
					double fraction1 = j / i;
					double fraction2 = 0;

					if (j.DigitAt(0) == i.DigitAt(0))
						fraction2 = j.DigitAt(1) / i.DigitAt(1);
					else if (j.DigitAt(1) == i.DigitAt(0))
						fraction2 = j.DigitAt(0) / i.DigitAt(1);
					else if (j.DigitAt(0) == i.DigitAt(1))
						fraction2 = j.DigitAt(1) / i.DigitAt(0);
					else if (j.DigitAt(1) == i.DigitAt(1))
						fraction2 = j.DigitAt(0) / i.DigitAt(0);
		
					if (fraction1 == fraction2)
						fractions.Add(fraction1);
				}
			}

			double final = 1;
			foreach (double d in fractions)
				final *= d;
			return Math.Round(1 / final);
		}

		public static int Problem34()
		{
			int total = 0;

			for (int x = 10; x < 999999; x++)
			{
				int sum = 0;
				for (int y = 0; y < x.ToString().Length; y++)
					sum += x.DigitAt(y).Factorial();
				if (x == sum)
					total += x;
			}

			return total;
		}

		public static int Problem35()
		{
			int total = 0;
			for (int i = 0; i < 1000000; i++)
			{
				if (i.IsCircularPrime())
					total++;
			}
			return total;
		}

		public static int Problem36()
		{
			int total = 0;
			for (int i = 0; i < 1000000; i++)
			{
				if (i.ToString().IsPalindrome() && Convert.ToString(i, 2).IsPalindrome())
					total += i;
			}
			return total;
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

		public static int Problem40()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 1000000; i++)
				sb.Append(i.ToString());
			string s = sb.ToString();

			int result = 1;
			for (int j = 1; j < 1000000; j *= 10)
				result *= s.DigitAt(j);
			return result;
		}

		public static int Problem41()
		{
			int largest = 0;
			for (int x = 1; x < 7777777; x++)
				if (x.IsPandigital(false) && x.IsPrime())
					if (x > largest)
						largest = x;
			return largest;
		}

		public static int Problem42()
		{
			string text = File.ReadAllText(Path.Combine("Resources", "042.txt"));
			string[] words = text.Replace("\"", "").Split(',');

			int triangles = 0;

			foreach (string w in words)
			{
				int value = 0;
				for (int i = 0; i < w.Length; i++)
					value += w[i] - 64;

				if (value.IsTriangle())
					triangles++;
			}

			return triangles;
		}

		/// <summary>
		/// unsolved
		/// </summary>
		/// <returns></returns>
		public static BigInteger Problem43()
		{
			List<ulong> primes = new List<ulong>() { 2, 3, 5, 7, 11, 13, 17 };

			BigInteger result = 0;
			foreach (string s in "0123456789".GetPermutations(""))
			{
				//Continue:
				//if (!i.IsPandigital(true))
				//	continue;

				//string s = i.ToString();
				//for (int j = 0; j < 6; j++)
				//	if (ulong.Parse(s.Substring(j + 2, 3)) % primes[j] != 0)
				//	{
				//		i += 9;
				//		goto Continue;
				//	}

				result += 1;
			}
			return result;
		}

		public static int Problem44()
		{
			for (int i = 1; i < 10001; i++)
			{
				for (int j = 1; j < 10001; j++)
				{
					int a = MathUtils.GetPentagonal(i);
					int b = MathUtils.GetPentagonal(j);
					if ((a + b).IsPentagonal() && Math.Abs(a - b).IsPentagonal())
						return (int)Math.Floor((double)Math.Abs(a - b));
				}
			}
			return 0;
		}

		public static ulong Problem45()
		{
			ulong i = 143;
			for (; ; )
			{
				i++;
				ulong hex = MathUtils.GetHexagonal(i);
				if (hex.IsPentagonal() && hex.IsTriangle())
					return hex;
			}
		}

		public static string Problem48()
		{
			BigInteger answer = 1;
			for (int i = 2; i < 1000; i++)
			{
				answer += BigInteger.Pow(i, i);
			}
			string s = answer.ToString();
			return s.Substring(s.Length - 10);
		}

		public static string Problem49()
		{
			for (int i = 1488; i < 9999; i++)
			{
				if (!i.IsPrime())
					continue;

				string x = i.ToString();
				List<string> perms = x.GetPermutations("");

				foreach (string y in perms)
				{
					int yy = int.Parse(y);

					if (i == yy || !yy.IsPrime())
						continue;

					foreach (string z in perms)
					{
						int zz = int.Parse(z);

						if (zz == yy || !zz.IsPrime())
							continue;

						if (zz - yy == yy - i && yy - i > 0)
							return x + y + z;
					}
				}
			}
			return "";
		}

		public static int Problem52()
		{
			int i = 0;
			for (; ; )
			{
				i++;
				if (i.SameDigits(i * 2) && i.SameDigits(i * 3) && i.SameDigits(i * 4) && i.SameDigits(i * 5) && i.SameDigits(i * 6))
					return i;
			}
		}

		public static int Problem53()
		{
			int total = 0;
			for (BigInteger i = 1; i < 101; i++)
				for (BigInteger j = 1; j < i; j++)
					if (i.Combinations(j) > 1000000)
						total++;
			return total;
		}

		public static int Problem55()
		{
			int lychrels = 0;
			for (int i = 10; i < 10000; i++)
				if (i.IsLychrel())
					lychrels++;
			return lychrels;
		}

		public static double Problem56()
		{
			double largest = 0;

			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					BigInteger r = BigInteger.Pow(i, j);

					double sum = 0;
					for (int k = 0; k < r.ToString().Length; k++)
						sum += r.DigitAt(k);
					if (largest < sum)
						largest = sum;
				}
			}
			return largest;
		}

		public static double Problem58()
		{
			double result = 1;
			double grid = 1;
			double num = 1;
			double multiplier = 2;
			double i = 0;
			double primes = 0;
			for (; ; )
			{
				double diagonals = grid * 2 - 1;
				while (i < diagonals)
				{
					if (num.IsPrime() && i % 4 != 0)
						primes++;
					i++;
					num += multiplier;
					if (i % 4 == 0)
						multiplier += 2;
				}

				result = primes / diagonals;

				if (result < 0.1 && grid != 1)
					break;
				else
					grid += 2;
			}

			return grid;
		}

		public static int Problem63()
		{
			int total = 0;
			for (int i = 1; i < 25; i++)
				for (int j = 1; j < 25; j++)
					if (BigInteger.Pow(i, j).ToString().Length == j)
						total++;
			return total;
		}

		public static int Problem67()
		{
			int[][] matrix = Utils.GenerateMatrix(67);

			for (int y = matrix.GetLength(0) - 2; y > -1; y--)
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
		
		public static int Problem89()
		{
			int oldChars = 0;
			int newChars = 0;
			foreach (string line in File.ReadAllLines(Path.Combine("Resources", "089.txt")))
			{
				oldChars += line.Length;
				newChars += RomanUtils.IntegerToRoman(RomanUtils.RomanToInteger(line)).Length;
			}
			return oldChars - newChars;
		}

		public static int Problem92()
		{
			int total = 0;

			for (int i = 1; i < 10000000; i++)
			{
				int j = i;
				while (j != 1)
				{
					j = j.SquareDigits();
					if (j == 89)
					{
						total++;
						break;
					}
				}
			}

			return total;
		}

		public static BigInteger Problem97()
		{
			return (28433 * BigInteger.Pow(2, 7830457) + 1) % 10000000000;
		}

		public static int Problem179()
		{
			int limit = (int)Math.Pow(10, 7);
			int result = 0;

			int[] divisors = new int[2] { 0, 2 };
			for (int i = 2; i < limit; i++)
			{
				divisors[0] = divisors[1];
				divisors[1] = (i + 1).GetDivisorAmount();

				if (divisors[0] == divisors[1])
					result++;
			}

			return result;
		}

		/// <summary>
		/// unsolved
		/// </summary>
		/// <returns></returns>
		public static int Problem345()
		{
			int[][] matrix = Utils.GenerateMatrix(345);

			int largest = 0;
			List<Tuple<int, int>> taken = new List<Tuple<int, int>>();
			Dictionary<int, Tuple<int, int>> nums = new Dictionary<int, Tuple<int, int>>();
			int len = matrix.GetLength(0);

			List<int> order = new List<int> { 0, 1, 2, 3, 4, /*5, 6, 7, 8, 9, 10, 11, 12, 13, 14*/ };
			foreach (List<int> combo in order.GetAllCombos())
			{
				int result = 0;
				taken.Clear();
				foreach (int i in combo)
				{
					nums.Clear();
					for (int j = 0; j < len; j++)
					{
						if (!taken.Contains(Tuple.Create(i, j)))// && !nums.ContainsKey(matrix[i][j]))
							//foreach ()
							nums.Add(matrix[i][j], Tuple.Create(i, j));
						if (!taken.Contains(Tuple.Create(j, i)) && !nums.ContainsKey(matrix[j][i]))
							nums.Add(matrix[j][i], Tuple.Create(j, i));
					}
					KeyValuePair<int, Tuple<int, int>> max = nums.FirstOrDefault(x => x.Key == nums.Keys.Max());

					taken.Add(Tuple.Create(max.Value.Item1, max.Value.Item2));

					result += max.Key;
				}
				if (result > largest)
					largest = result;
			}

			return largest;
		}
	}
}