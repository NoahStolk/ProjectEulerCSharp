using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjectEulerCSharp.Extensions
{
	public static class StringExtensions
	{
		public static string Repeat(this string s, int amount)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < amount; i++)
				sb.Append(s);
			return sb.ToString();
		}

		public static string MakeNumeric(this string s, bool includeSign = false)
			=> Regex.Replace(s, includeSign ? "[^0-9.+-]" : "[^0-9]", "");

		public static string Reverse(this string s)
		{
			char[] charArray = s.ToCharArray();
			Array.Reverse(charArray);

			return new string(charArray);
		}

		public static bool IsPalindrome(this string s)
			=> s == Reverse(s);

		// TODO: Remove this method to improve performance.
		public static string SubstringSafe(this string s, int startIndex, int length)
			=> s.Substring(Math.Min(startIndex, s.Length), Math.Min(Math.Max(0, length), Math.Max(0, s.Length - startIndex)));

		public static int DigitAt(this string s, int pos)
			=> int.Parse(s[pos].ToString());

		public static List<string> GetPermutations(this string s, string prefix)
		{
			List<string> perms = new List<string>();

			int n = s.Length;
			if (n == 0)
			{
				perms.Add(prefix);
			}
			else
			{
				for (int i = 0; i < n; i++)
					perms.AddRange(GetPermutations(s.Substring(0, i) + s[(i + 1)..n], prefix + s[i]));
			}

			return perms;
		}
	}
}