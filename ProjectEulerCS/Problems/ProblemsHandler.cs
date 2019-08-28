using NetBase.Extensions;
using ProjectEulerCS.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEulerCS.Problems
{
	public sealed class ProblemsHandler
	{
		private static readonly Lazy<ProblemsHandler> lazy = new Lazy<ProblemsHandler>(() => new ProblemsHandler());
		public static ProblemsHandler Instance => lazy.Value;

		private ProblemsHandler()
		{
		}

		[Problem(ProblemState.Solved)]
		public int Problem001()
		{
			int result = 0;
			for (int i = 3; i < 1000; i++)
				if (i % 3 == 0 || i % 5 == 0)
					result += i;
			return result;
		}

		[Problem(ProblemState.Solved)]
		public int Problem002()
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

		[Problem(ProblemState.Solved)]
		public int Problem003()
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

		[Problem(ProblemState.Solved)]
		public int Problem004()
		{
			int largest = 0;

			// The square root of 100000 (minimum value that contains 6 digits) floored is 316, so start from there.
			for (int i = 316; i < 1000; i++)
			{
				for (int j = 316; j < 1000; j++)
				{
					int x = i * j;
					if (x < 100000 || x > 999999) // Result needs to contain 6 digits.
						continue;

					string str = x.ToString();

					bool breakOuter = false;
					for (int k = 0; k < 3; k++)
					{
						if (str[k] != str[6 - k - 1])
						{
							breakOuter = true;
							break;
						}
					}
					if (breakOuter)
						continue;

					if (x > largest)
						largest = x;
				}
			}

			return largest;
		}

		[Problem(ProblemState.Solved)]
		public long Problem005()
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

		[Problem(ProblemState.Solved)]
		public int Problem006()
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

		[Problem(ProblemState.Solved)]
		public int Problem007()
		{
			int primes = 1;
			int number = 3;
			for (; ; )
			{
				if (MathUtils.IsPrime(number))
					primes++;

				if (primes == 10001)
					return number;

				number += 2;
			}
		}

		[Problem(ProblemState.Solved)]
		public long Problem008()
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

		[Problem(ProblemState.Solved)]
		public long Problem009()
		{
			int num = 1000;
			long ans = 0;

			for (int i = 1; i < num / 2; i++)
			{
				double a = (Math.Pow(num, 2) / 2 - num * i) / (num - i);
				if (a % 1 == 0)
					ans = (long)(a * i * (num - a - i));
			}

			return ans;
		}

		[Problem(ProblemState.Solved)]
		public long Problem010()
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

				if (MathUtils.IsPrime(i))
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

		[Problem(ProblemState.Solved)]
		public int Problem011()
		{
			int w = 20;
			int h = 20;
			int[][] matrix = ResourceUtils.GenerateMatrix(11);

			int highest = 0;

			int product;
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

		[Problem(ProblemState.Solved)]
		public int Problem012()
		{
			int n = 2;
			int triangle = 3;

			for (; ; )
			{
				if (MathUtils.GetDivisorAmount(triangle) > 500)
					break;

				n++;
				triangle += n;
			}

			return triangle;
		}

		[Problem(ProblemState.Solved)]
		public string Problem013()
		{
			string[] digits = File.ReadAllLines(Path.Combine("Resources", "013.txt"));
			string total = "0";
			for (int x = 0; x < digits.Length; x++)
				total = total.NumeralAddition(digits[x]);

			return total.Substring(0, 10);
		}

		[Problem(ProblemState.Solved)]
		public int Problem014()
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

		[Problem(ProblemState.Solved)]
		public BigInteger Problem015()
		{
			BigInteger grid = 20;

			return MathUtils.Factorial(grid * 2) / MathUtils.Factorial(grid) / MathUtils.Factorial(grid);
		}

		[Problem(ProblemState.Solved)]
		public int Problem016()
		{
			List<int> digits = MathUtils.PowersOfTwo(1000);

			int total = 0;
			for (int i = 0; i < digits.Count; i++)
				total += digits[i];

			return total;
		}

		[Problem(ProblemState.Solved)]
		public int Problem017()
		{
			int letters = 0;

			for (int x = 1; x <= Math.Pow(10, 3); x++)
				letters += GetWord(x).Length;

			return letters;

			StringBuilder GetWord(int n)
			{
				StringBuilder word = new StringBuilder();

				if (n.ToString().Length == 4)
				{
					word.Append("onethousand");
					n = int.Parse(n.ToString().Substring(1));
				}

				if (n.ToString().Length == 3)
				{
					if (n >= 900)
						word.Append("nine");
					else if (n >= 800)
						word.Append("eight");
					else if (n >= 700)
						word.Append("seven");
					else if (n >= 600)
						word.Append("six");
					else if (n >= 500)
						word.Append("five");
					else if (n >= 400)
						word.Append("four");
					else if (n >= 300)
						word.Append("three");
					else if (n >= 200)
						word.Append("two");
					else
						word.Append("one");

					if (n % 100 == 0)
						word.Append("hundred");
					else
						word.Append("hundredand");

					n = int.Parse(n.ToString().Substring(1));
				}

				if (n.ToString().Length == 2)
				{
					if (n >= 20)
					{
						if (n >= 90)
							word.Append("ninety");
						else if (n >= 80)
							word.Append("eighty");
						else if (n >= 70)
							word.Append("seventy");
						else if (n >= 60)
							word.Append("sixty");
						else if (n >= 50)
							word.Append("fifty");
						else if (n >= 40)
							word.Append("forty");
						else if (n >= 30)
							word.Append("thirty");
						else if (n >= 20)
							word.Append("twenty");
						n = int.Parse(n.ToString().Substring(1));
					}
					else
					{
						if (n == 19)
							word.Append("nineteen");
						else if (n == 18)
							word.Append("eighteen");
						else if (n == 17)
							word.Append("seventeen");
						else if (n == 16)
							word.Append("sixteen");
						else if (n == 15)
							word.Append("fifteen");
						else if (n == 14)
							word.Append("fourteen");
						else if (n == 13)
							word.Append("thirteen");
						else if (n == 12)
							word.Append("twelve");
						else if (n == 11)
							word.Append("eleven");
						else if (n == 10)
							word.Append("ten");
						n = 0;
					}
				}

				if (n.ToString().Length == 1)
				{
					if (n == 9)
						word.Append("nine");
					if (n == 8)
						word.Append("eight");
					if (n == 7)
						word.Append("seven");
					if (n == 6)
						word.Append("six");
					if (n == 5)
						word.Append("five");
					if (n == 4)
						word.Append("four");
					if (n == 3)
						word.Append("three");
					if (n == 2)
						word.Append("two");
					if (n == 1)
						word.Append("one");
				}

				return word;
			}
		}

		[Problem(ProblemState.Solved)]
		public int Problem018()
		{
			int[][] matrix = ResourceUtils.GenerateMatrix(18);

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

		[Problem(ProblemState.Solved)]
		public int Problem019()
		{
			int s = 0;
			for (int i = 1901; i < 2001; i++)
				for (int j = 1; j < 13; j++)
					if (new DateTime(i, j, 1).DayOfWeek == DayOfWeek.Sunday)
						s++;
			return s;
		}

		[Problem(ProblemState.Solved)]
		public BigInteger Problem020()
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

		[Problem(ProblemState.Solved)]
		public int Problem021()
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

		[Problem(ProblemState.Solved)]
		public int Problem022()
		{
			int totalScore = 0;
			string text = File.ReadAllText(Path.Combine("Resources", "022.txt"));
			string[] names = text.Replace("\"", "").Split(',');
			Array.Sort(names);

			for (int x = 0; x < names.Length; x++)
			{
				int letterScore = 0;

				for (int y = 0; y < names[x].Length; y++)
					letterScore += names[x][y] - 64;

				int score = letterScore * (x + 1);

				totalScore += score;
			}

			return totalScore;
		}

		[Problem(ProblemState.TooSlow)] // 3:33.20
		public int Problem023()
		{
			int max = 28124;

			List<int> abundants = new List<int>();
			for (int i = 1; i < max; i++)
				if (i < MathUtils.GetProperDivisorSum(i))
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

		[Problem(ProblemState.Solved)]
		public string Problem024()
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

		[Problem(ProblemState.Solved)]
		public int Problem025()
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

		[Problem(ProblemState.Solved)]
		public int Problem026()
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

		[Problem(ProblemState.Solved)]
		public int Problem028()
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

		[Problem(ProblemState.Solved)]
		public int Problem029()
		{
			List<double> sequence = new List<double>();

			for (int x = 2; x < 101; x++)
				for (int y = 2; y < 101; y++)
					sequence.Add(Math.Pow(x, y));

			sequence = sequence.Distinct().ToList();

			return sequence.Count;
		}

		[Problem(ProblemState.Solved)]
		public int Problem030()
		{
			int t = 0;

			for (int x = 10; x < 999999; x++)
			{
				int sum = 0;
				for (int y = 0; y < x.ToString().Length; y++)
					sum += (int)Math.Pow(MathUtils.DigitAt(x, y), 5);
				if (x == sum)
					t += x;
			}

			return t;
		}

		[Problem(ProblemState.Solved)]
		public int Problem031()
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

		[Problem(ProblemState.Solved)]
		public int Problem032()
		{
			List<int> answers = new List<int>();

			for (uint i = 123456789; i < 987654321 / 2; i += 9)
			{
				if (!MathUtils.IsPandigital(i, false))
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

		[Problem(ProblemState.InProgress)]
		public double Problem033()
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

					if (MathUtils.DigitAt(j, 0) == MathUtils.DigitAt(i, 0))
						fraction2 = MathUtils.DigitAt(j, 1) / MathUtils.DigitAt(i, 1);
					else if (MathUtils.DigitAt(j, 1) == MathUtils.DigitAt(i, 0))
						fraction2 = MathUtils.DigitAt(j, 0) / MathUtils.DigitAt(i, 1);
					else if (MathUtils.DigitAt(j, 0) == MathUtils.DigitAt(i, 1))
						fraction2 = MathUtils.DigitAt(j, 1) / MathUtils.DigitAt(i, 0);
					else if (MathUtils.DigitAt(j, 1) == MathUtils.DigitAt(i, 1))
						fraction2 = MathUtils.DigitAt(j, 0) / MathUtils.DigitAt(i, 0);

					if (fraction1 == fraction2)
						fractions.Add(fraction1);
				}
			}

			double final = 1;
			foreach (double d in fractions)
				final *= d;
			return Math.Round(1 / final);
		}

		[Problem(ProblemState.Solved)]
		public int Problem034()
		{
			int total = 0;

			for (int x = 10; x < 999999; x++)
			{
				int sum = 0;
				for (int y = 0; y < x.ToString().Length; y++)
					sum += MathUtils.Factorial(MathUtils.DigitAt(x, y));
				if (x == sum)
					total += x;
			}

			return total;
		}

		[Problem(ProblemState.Solved)]
		public int Problem035()
		{
			int total = 0;
			for (uint i = 0; i < 1000000; i++)
			{
				if (MathUtils.IsCircularPrime(i))
					total++;
			}
			return total;
		}

		[Problem(ProblemState.Solved)]
		public int Problem036()
		{
			int total = 0;
			for (int i = 0; i < 1000000; i++)
			{
				if (i.ToString().IsPalindrome() && Convert.ToString(i, 2).IsPalindrome())
					total += i;
			}
			return total;
		}

		[Problem(ProblemState.Solved)]
		public uint Problem037()
		{
			uint sum = 0;
			int primes = 0;

			uint j = 11;
			while (primes < 11)
			{
				if (MathUtils.IsTruncatablePrime(j))
				{
					primes++;
					sum += j;
				}
				j += 2;
			}

			return sum;
		}

		[Problem(ProblemState.Solved)]
		public int Problem040()
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

		[Problem(ProblemState.Solved)]
		public int Problem041()
		{
			int largest = 0;
			for (int x = 1; x < 7777777; x++)
				if (MathUtils.IsPandigital(x, false) && MathUtils.IsPrime(x))
					if (x > largest)
						largest = x;
			return largest;
		}

		[Problem(ProblemState.Solved)]
		public int Problem042()
		{
			string text = File.ReadAllText(Path.Combine("Resources", "042.txt"));
			string[] words = text.Replace("\"", "").Split(',');

			int triangles = 0;

			foreach (string w in words)
			{
				int value = 0;
				for (int i = 0; i < w.Length; i++)
					value += w[i] - 64;

				if (MathUtils.IsTriangle(value))
					triangles++;
			}

			return triangles;
		}

		[Problem(ProblemState.InProgress)]
		public BigInteger Problem043()
		{
			//List<ulong> primes = new List<ulong>() { 2, 3, 5, 7, 11, 13, 17 };

			//BigInteger result = 0;
			//foreach (string s in "0123456789".GetPermutations(""))
			//{
			//Continue:
			//	if (!i.IsPandigital(true))
			//		continue;

			//	string s = i.ToString();
			//	for (int j = 0; j < 6; j++)
			//		if (ulong.Parse(s.Substring(j + 2, 3)) % primes[j] != 0)
			//		{
			//			i += 9;
			//			goto Continue;
			//		}

			//	result += 1;
			//}
			//return result;
			return 0;
		}

		[Problem(ProblemState.Solved)]
		public int Problem044()
		{
			for (int i = 1; i < 10001; i++)
			{
				for (int j = 1; j < 10001; j++)
				{
					int a = MathUtils.GetPentagonal(i);
					int b = MathUtils.GetPentagonal(j);
					if (MathUtils.IsPentagonal(a + b) && MathUtils.IsPentagonal(a - b))
						return (int)Math.Floor((double)Math.Abs(a - b));
				}
			}
			return 0;
		}

		[Problem(ProblemState.Solved)]
		public long Problem045()
		{
			long i = 143;
			for (; ; )
			{
				i++;
				long hex = MathUtils.GetHexagonal(i);
				if (MathUtils.IsPentagonal(hex) && MathUtils.IsTriangle(hex))
					return hex;
			}
		}

		[Problem(ProblemState.Solved)]
		public string Problem048()
		{
			BigInteger answer = 1;
			for (int i = 2; i < 1000; i++)
			{
				answer += BigInteger.Pow(i, i);
			}
			string s = answer.ToString();
			return s.Substring(s.Length - 10);
		}

		[Problem(ProblemState.Solved)]
		public string Problem049()
		{
			for (int i = 1488; i < 9999; i++)
			{
				if (!MathUtils.IsPrime(i))
					continue;

				string x = i.ToString();
				List<string> perms = x.GetPermutations("");

				foreach (string y in perms)
				{
					int yy = int.Parse(y);

					if (i == yy || !MathUtils.IsPrime(yy))
						continue;

					foreach (string z in perms)
					{
						int zz = int.Parse(z);

						if (zz == yy || !MathUtils.IsPrime(zz))
							continue;

						if (zz - yy == yy - i && yy - i > 0)
							return x + y + z;
					}
				}
			}
			return "";
		}

		[Problem(ProblemState.InProgress)]
		public int Problem050()
		{
			int answer = 0;

			int i = 0;
			int prime = 2;
			while (i < 1000000)
			{
				if (MathUtils.IsPrime(prime))
					i += prime;
				prime++;

				if (i < 1000000 && MathUtils.IsPrime(i))
					answer = i;
			}
			return answer;
		}

		[Problem(ProblemState.Solved)]
		public int Problem052()
		{
			int i = 0;
			for (; ; )
			{
			Restart:
				i++;
				for (int j = 2; j < 6; j++)
					if (!MathUtils.ContainsSameDigits(i, i * j))
						goto Restart;
				return i;
			}
		}

		[Problem(ProblemState.Solved)]
		public int Problem053()
		{
			int total = 0;
			for (BigInteger i = 1; i < 101; i++)
				for (BigInteger j = 1; j < i; j++)
					if (MathUtils.Combinations(i, j) > 1000000)
						total++;
			return total;
		}

		[Problem(ProblemState.Solved)]
		public int Problem055()
		{
			int lychrels = 0;
			for (int i = 10; i < 10000; i++)
				if (MathUtils.IsLychrel(i))
					lychrels++;
			return lychrels;
		}

		[Problem(ProblemState.Solved)]
		public double Problem056()
		{
			double largest = 0;

			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < 100; j++)
				{
					BigInteger r = BigInteger.Pow(i, j);

					double sum = 0;
					for (int k = 0; k < r.ToString().Length; k++)
						sum += MathUtils.DigitAt(r, k);
					if (largest < sum)
						largest = sum;
				}
			}
			return largest;
		}

		[Problem(ProblemState.Solved)]
		public double Problem058()
		{
			double grid = 1;
			long num = 1;
			long multiplier = 2;
			double i = 0;
			double primes = 0;
			for (; ; )
			{
				double diagonals = grid * 2 - 1;
				while (i < diagonals)
				{
					if (MathUtils.IsPrime(num) && i % 4 != 0)
						primes++;
					i++;
					num += multiplier;
					if (i % 4 == 0)
						multiplier += 2;
				}

				double result = primes / diagonals;

				if (result < 0.1 && grid != 1)
					break;
				else
					grid += 2;
			}

			return grid;
		}

		[Problem(ProblemState.Solved)]
		public int Problem063()
		{
			int total = 0;
			for (int i = 1; i < 25; i++)
				for (int j = 1; j < 25; j++)
					if (BigInteger.Pow(i, j).ToString().Length == j)
						total++;
			return total;
		}

		[Problem(ProblemState.Solved)]
		public int Problem067()
		{
			int[][] matrix = ResourceUtils.GenerateMatrix(67);

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

		[Problem(ProblemState.Solved)]
		public int Problem089()
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

		[Problem(ProblemState.Solved)]
		public int Problem092()
		{
			int total = 0;

			for (int i = 1; i < 10000000; i++)
			{
				int j = i;
				while (j != 1)
				{
					j = MathUtils.SquareDigits(j);
					if (j == 89)
					{
						total++;
						break;
					}
				}
			}

			return total;
		}

		[Problem(ProblemState.Solved)]
		public BigInteger Problem097()
		{
			return (28433 * BigInteger.Pow(2, 7830457) + 1) % 10000000000;
		}

		[Problem(ProblemState.TooSlow)] // 4:36.93
		public int Problem179()
		{
			int limit = (int)Math.Pow(10, 7);
			int result = 0;

			int[] divisors = new int[2] { 0, 2 };
			for (int i = 2; i < limit; i++)
			{
				divisors[0] = divisors[1];
				divisors[1] = MathUtils.GetDivisorAmount(i + 1);

				if (divisors[0] == divisors[1])
					result++;
			}

			return result;
		}

		[Problem(ProblemState.InProgress)]
		public string Problem345()
		{
			List<int> originalList = ResourceUtils.GenerateList(345).ToList();
			List<List<int>> combos = GetAllCombos(originalList);

			int c = 0;
			for (; ; )
			{
			Restart:

				List<int> list = new List<int>(combos[c]);
				Dictionary<int, int> highest = new Dictionary<int, int>();
				while (highest.Count < 5)
				{
					highest.Add(list.IndexOf(list.Max()), list.Max());
					list[list.IndexOf(list.Max())] = 0;
				}

				List<int> takenX = new List<int>();
				List<int> takenY = new List<int>();
				foreach (KeyValuePair<int, int> kvp in highest)
				{
					for (int i = 0; i < takenX.Count; i++)
					{
						if (kvp.Key / 5 == takenX[i] || kvp.Key % 5 == takenY[i])
						{
							c++;
							goto Restart;
						}
					}

					takenX.Add(kvp.Key / 5);
					takenY.Add(kvp.Key % 5);
				}

				StringBuilder highestStr = new StringBuilder();
				int result = 0;
				foreach (int num in highest.Values)
				{
					result += num;
					highestStr.Append(num.ToString());
					highestStr.Append(" + ");
				}

				return highestStr.ToString().TrimEnd("+ ".ToCharArray()) + " = " + result;
			}

			List<List<T>> GetAllCombos<T>(List<T> list)
			{
				List<List<T>> result = new List<List<T>>
				{
					new List<T>()
				};
				result.Last().Add(list[0]);
				if (list.Count == 1)
					return result;

				List<List<T>> tailCombos = GetAllCombos(list.Skip(1).ToList());
				tailCombos.ForEach(combo =>
				{
					result.Add(new List<T>(combo));
					combo.Add(list[0]);
					result.Add(new List<T>(combo));
				});

				return result;
			}
		}
	}
}