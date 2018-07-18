using System;
using System.Diagnostics;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			Console.WriteLine(Problems.Problem179((int)Math.Pow(10, 5)));

			stopwatch.Stop();
			Console.WriteLine($"Time elapsed: {stopwatch.Elapsed}");
		}
	}
}