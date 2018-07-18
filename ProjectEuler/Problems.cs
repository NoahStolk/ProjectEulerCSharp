using XtendedNET.Utils;

namespace ProjectEuler
{
	public static class Problems
	{
		public static int Problem1()
		{
			int result = 0;
			for (int i = 1; i < 1000; i++)
				if (i % 3 == 0 || i % 5 == 0)
					result += i;
			return result;
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