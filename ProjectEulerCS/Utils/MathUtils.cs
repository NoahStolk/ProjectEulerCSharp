using NetBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;

namespace ProjectEulerCS.Utils
{
	public static class MathUtils
	{
		private static readonly string decimalSeparator = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

		public static bool IsPrime(long number)
		{
			if (number < 2)
				return false;

			double sqrt = Math.Sqrt(number);
			for (long i = 2; i <= sqrt; i++)
				if (number % i == 0 || number % Math.Ceiling(number / (double)i) == 0)
					return false;

			if (sqrt == Math.Floor(sqrt))
				return false;

			return true;
		}

		public static bool IsTruncatablePrime(long number)
		{
			if (number < 10)
				return false;

			string numStr = number.ToString();
			int length = numStr.Length;
			for (int i = length - 1; i >= 0; i--)
				if (!IsPrime(int.Parse(numStr.Substring(0, length - i))) || !IsPrime(int.Parse(numStr.Substring(i, length - i))))
					return false;

			return true;
		}

		public static bool IsCircularPrime(long number)
		{
			if (!IsPrime(number))
				return false;

			string r = number.ToString();
			for (int i = 0; i < r.Length; i++)
			{
				r = r.Substring(1, r.Length - 1) + r[0];
				if (!IsPrime(long.Parse(r)))
					return false;
			}

			return true;
		}

		public static List<int> PowersOfTwo(int exponent)
		{
			List<int> list = new List<int> { 1 };

			for (int i = 1; i <= exponent; i++)
			{
				int count = list.Count;
				for (int j = count - 1; j >= 0; j--)
				{
					if (list[j] >= 5)
					{
						list[j] -= 10 - list[j];
						if (j >= count - 1)
							list.Add(0);
						list[j + 1]++;
					}
					else list[j] *= 2;
				}
			}

			list.Reverse();

			return list;
		}

		public static bool IsComposite(int number)
		{
			int factors = 0;
			int root = (int)Math.Sqrt(number);

			for (int i = 2; i <= root; i++)
			{
				while (number % i == 0)
				{
					factors++;
					if (factors > 2)
						return false;

					number /= i;
				}
			}

			if (number != 1)
				factors++;

			if (factors == 2)
				return true;

			return false;
		}

		public static int GetDivisorAmount(long number)
		{
			long sqrt = (long)Math.Sqrt(number);

			int divisors = 0;
			for (long i = 1; i <= sqrt; i++)
			{
				if (number % i == 0 && i * i != number)
					divisors += 2;
				else if (number % i == 0 && i * i == number)
					divisors++;
			}

			return divisors;
		}

		public static List<long> GetDivisors(long number)
		{
			List<long> divisors = new List<long> { 1, number };

			long sqrt = (long)Math.Sqrt(number);
			for (long i = 2; i <= sqrt; i++)
			{
				if (number % i == 0)
				{
					divisors.Add(i);
					if (i != number / i)
						divisors.Add(number / i);
				}
			}

			return divisors;
		}

		public static List<long> GetProperDivisors(long number)
		{
			List<long> divisors = new List<long> { 1 };

			long sqrt = (long)Math.Sqrt(number);
			for (long i = 2; i <= sqrt; i++)
			{
				if (number % i == 0)
				{
					divisors.Add(i);
					if (i != number / i)
						divisors.Add(number / i);
				}
			}

			return divisors;
		}

		public static long GetDivisorSum(long number)
		{
			long divisorSum = 1 + number;

			long sqrt = (long)Math.Sqrt(number);
			for (long i = 2; i <= sqrt; i++)
			{
				if (number % i == 0)
				{
					divisorSum += i;
					if (i != number / i)
						divisorSum += number / i;
				}
			}

			return divisorSum;
		}

		public static long GetProperDivisorSum(long number)
		{
			long divisorSum = 1;

			long sqrt = (long)Math.Sqrt(number);
			for (long i = 2; i <= sqrt; i++)
			{
				if (number % i == 0)
				{
					divisorSum += i;
					if (i != number / i)
						divisorSum += number / i;
				}
			}

			return divisorSum;
		}

		public static bool ContainsSameDigits(int x, int y)
		{
			string a = x.ToString();
			string b = y.ToString();

			if (a.Length != b.Length)
				return false;

			for (int i = 0; i < a.Length; i++)
				if (!a.Contains(b[i].ToString()))
					return false;

			return true;
		}

		public static bool IsInteger(decimal value)
		{
			return value % 1 == 0;
		}
		public static bool IsInteger(double value)
		{
			return value % 1 == 0;
		}

		public static string GetDecimals(decimal value)
		{
			if (!IsInteger(value))
				return string.Empty;

			string d = value.ToString();
			return d.Substring(d.IndexOf(decimalSeparator) + 1);
		}
		public static string GetDecimals(double value)
		{
			if (!IsInteger(value))
				return string.Empty;

			string d = value.ToString();
			return d.Substring(d.IndexOf(decimalSeparator) + 1);
		}

		public static int Collatz(decimal input)
		{
			int count = 0;

			while (input > 1)
			{
				input = input % 2 == 0 ? input / 2 : input * 3 + 1;
				count++;
			}

			return count;
		}

		public static long Fibonacci(long max)
		{
			long a = 0;
			long b = 1;
			long c = 0;

			for (long i = 0; i < max; i++)
			{
				c = a + b;
				a = b;
				b = c;
			}

			return c;
		}

		public static int SquareDigits(int x)
		{
			string s = x.ToString();
			int t = 0;
			foreach (char c in s)
				t += (int)Math.Pow(int.Parse(c.ToString()), 2);
			return t;
		}

		public static int GetNthTriangle(int n)
		{
			return (int)(n * 0.5 * (n + 1));
		}

		public static int GetPentagonal(int n)
		{
			return n * (3 * n - 1) / 2;
		}

		public static bool IsPandigital(long x, bool includeZero)
		{
			string s = x.ToString();
			if (!includeZero && s.Contains("0"))
				return false;

			int len = s.Length;
			for (int i = len - 1; i > 0; i--)
				if (!s.Contains(i.ToString()))
					return false;

			return true;
		}

		public static long GetHexagonal(long n)
		{
			return n * (2 * n - 1);
		}
		public static int GetHexagonal(int n)
		{
			return n * (2 * n - 1);
		}

		public static bool IsTriangle(long n)
		{
			return IsInteger(Math.Sqrt(8 * n + 1));
		}
		public static bool IsTriangle(int n)
		{
			return IsInteger(Math.Sqrt(8 * n + 1));
		}

		public static bool IsPentagonal(long x)
		{
			return IsInteger((1 + Math.Sqrt(24 * x + 1)) / 6);
		}
		public static bool IsPentagonal(int x)
		{
			return IsInteger((1 + Math.Sqrt(24 * x + 1)) / 6);
		}

		public static long Factorial(long i)
		{
			if (i <= 1)
				return 1;
			return i * Factorial(i - 1);
		}
		public static int Factorial(int i)
		{
			if (i <= 1)
				return 1;
			return i * Factorial(i - 1);
		}
		public static BigInteger Factorial(BigInteger a)
		{
			if (a <= 1)
				return 1;
			return a * Factorial(a - 1);
		}

		public static BigInteger Combinations(BigInteger n, BigInteger r)
		{
			return Factorial(n) / (Factorial(r) * Factorial(n - r));
		}

		public static int DigitAt(int value, int pos)
		{
			string d = value.ToString();
			if (pos < 0 || pos >= d.Length)
				throw new IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the string.\nParameter name: {nameof(pos)}");

			return int.Parse(d[pos].ToString());
		}
		public static int DigitAt(decimal value, int pos)
		{
			string d = value.ToString().Replace(decimalSeparator, string.Empty);
			if (pos < 0 || pos >= d.Length)
				throw new IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the string.\nParameter name: {nameof(pos)}");

			return int.Parse(d[pos].ToString());
		}
		public static int DigitAt(double value, int pos)
		{
			string d = value.ToString().Replace(decimalSeparator, string.Empty);
			if (pos < 0 || pos >= d.Length)
				throw new IndexOutOfRangeException($"Index was out of range. Must be non-negative and less than the size of the string.\nParameter name: {nameof(pos)}");

			return int.Parse(d[pos].ToString());
		}
		public static int DigitAt(BigInteger i, int pos)
		{
			return int.Parse(i.ToString()[pos].ToString());
		}

		public static bool IsLychrel(int num)
		{
			BigInteger b = num;
			for (int i = 0; i < 50; i++)
			{
				b += BigInteger.Parse(b.ToString().Reverse());
				if (b.ToString().IsPalindrome())
					return false;
			}
			return true;
		}

		public static string NumeralAddition(this string s1, string s2)
		{
			if (s1.Numeric() != s1 || s2.Numeric() != s2)
				return string.Empty;

			string aRev = s1.Reverse();
			string bRev = s2.Reverse();

			int length = Math.Max(aRev.Length, bRev.Length) + 1;
			char[] n = new char[length];

			int remainder = 0;
			for (int i = 0; i < length; i++)
			{
				int aDigit = aRev.Length > i ? int.Parse(aRev[i].ToString()) : 0;
				int bDigit = bRev.Length > i ? int.Parse(bRev[i].ToString()) : 0;
				int result = aDigit + bDigit + remainder;
				string resultStr = result.ToString();
				remainder = (resultStr.Length > 1) ? int.Parse(resultStr[0].ToString()) : 0;

				n[i] = int.Parse(resultStr[resultStr.Length - 1].ToString()).ToString()[0];
			}

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < n.Length; i++)
				sb.Append(n[i]);

			string endResult = sb.ToString();
			for (int i = endResult.Length - 1; i >= 0; i--)
				if (endResult[i] == '0')
					endResult = endResult.Substring(0, endResult.Length - 1);
				else
					break;

			return endResult.Reverse();
		}

		public static string NumeralSubtraction(this string s1, string s2)
		{
			if (s1.Numeric() != s1 || s2.Numeric() != s2)
				return string.Empty;

			string aRev = s1.Reverse();
			string bRev = s2.Reverse();

			int length = Math.Max(aRev.Length, bRev.Length);
			char[] n = new char[length];

			int remainder = 0;
			for (int i = 0; i < length; i++)
			{
				int aDigit = aRev.Length > i ? int.Parse(aRev[i].ToString()) : 0;
				int bDigit = bRev.Length > i ? int.Parse(bRev[i].ToString()) : 0;
				int result = aDigit - bDigit - remainder;

				remainder = 0;
				while (result < 0)
				{
					result += 10;
					remainder++;
				}

				string resultStr = result.ToString();

				n[i] = int.Parse(resultStr[resultStr.Length - 1].ToString()).ToString()[0];
			}

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < n.Length; i++)
				sb.Append(n[i]);

			string endResult = sb.ToString();
			for (int i = endResult.Length - 1; i >= 0; i--)
				if (endResult[i] == '0')
					endResult = endResult.Substring(0, endResult.Length - 1);
				else
					break;

			return endResult.Reverse();
		}
	}
}