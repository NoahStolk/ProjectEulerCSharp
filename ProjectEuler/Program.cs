using System;
using System.Diagnostics;
using System.Reflection;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WriteLineColor("Project Euler - C#", ConsoleColor.Green);
			Console.WriteLine();

			for (; ; )
			{
				WriteColor("Enter a problem to solve: ", ConsoleColor.Yellow);
				string problem = Console.ReadLine();

				Type type = typeof(Problems);
				MethodInfo method = type.GetMethod($"Problem{problem}");
				Problems problems = new Problems();

				Console.WriteLine();

				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					WriteLineColor(method.Invoke(problems, null), ConsoleColor.White);
					stopwatch.Stop();
					WriteLineColor($"Execution took: {stopwatch.Elapsed}", ConsoleColor.Cyan);
				}
				catch (Exception)
				{
					WriteLineColor("Problem not found.", ConsoleColor.Red);
				}

				Console.WriteLine();
			}
		}

		public static void WriteLineColor(object message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
		}

		public static void WriteColor(object message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
		}
	}
}