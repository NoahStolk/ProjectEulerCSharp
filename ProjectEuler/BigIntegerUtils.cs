using NetBase.Extensions;
using System.Numerics;

namespace ProjectEuler
{
	public static class BigIntegerUtils
	{
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
	}
}