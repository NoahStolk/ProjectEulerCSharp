using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Utils
{
	public static class ProblemUtils
	{
		public static List<List<T>> GetAllCombos<T>(this List<T> list)
		{
			List<List<T>> result = new List<List<T>>
			{
				new List<T>()
			};
			result.Last().Add(list[0]);
			if (list.Count == 1)
				return result;

			List<List<T>> tailCombos = list.Skip(1).ToList().GetAllCombos();
			tailCombos.ForEach(combo =>
			{
				result.Add(new List<T>(combo));
				combo.Add(list[0]);
				result.Add(new List<T>(combo));
			});

			return result;
		}

		public static string GetWord(int n)
		{
			string word = "";

			if (n.ToString().Length == 4)
			{
				word += "onethousand";
				n = int.Parse(n.ToString().Substring(1));
			}

			if (n.ToString().Length == 3)
			{
				if (n >= 900)
					word += "nine";
				else if (n >= 800)
					word += "eight";
				else if (n >= 700)
					word += "seven";
				else if (n >= 600)
					word += "six";
				else if (n >= 500)
					word += "five";
				else if (n >= 400)
					word += "four";
				else if (n >= 300)
					word += "three";
				else if (n >= 200)
					word += "two";
				else
					word += "one";

				if (n % 100 == 0)
					word += "hundred";
				else
					word += "hundredand";

				n = int.Parse(n.ToString().Substring(1));
			}

			if (n.ToString().Length == 2)
			{
				if (n >= 20)
				{
					if (n >= 90)
						word += "ninety";
					else if (n >= 80)
						word += "eighty";
					else if (n >= 70)
						word += "seventy";
					else if (n >= 60)
						word += "sixty";
					else if (n >= 50)
						word += "fifty";
					else if (n >= 40)
						word += "forty";
					else if (n >= 30)
						word += "thirty";
					else if (n >= 20)
						word += "twenty";
					n = int.Parse(n.ToString().Substring(1));
				}
				else
				{
					if (n == 19)
						word += "nineteen";
					else if (n == 18)
						word += "eighteen";
					else if (n == 17)
						word += "seventeen";
					else if (n == 16)
						word += "sixteen";
					else if (n == 15)
						word += "fifteen";
					else if (n == 14)
						word += "fourteen";
					else if (n == 13)
						word += "thirteen";
					else if (n == 12)
						word += "twelve";
					else if (n == 11)
						word += "eleven";
					else if (n == 10)
						word += "ten";
					n = 0;
				}
			}

			if (n.ToString().Length == 1)
			{
				if (n == 9)
					word += "nine";
				if (n == 8)
					word += "eight";
				if (n == 7)
					word += "seven";
				if (n == 6)
					word += "six";
				if (n == 5)
					word += "five";
				if (n == 4)
					word += "four";
				if (n == 3)
					word += "three";
				if (n == 2)
					word += "two";
				if (n == 1)
					word += "one";
			}

			return word;
		}
	}
}