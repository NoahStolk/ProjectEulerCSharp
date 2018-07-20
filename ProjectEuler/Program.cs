using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using XtendedNET.Extensions;

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
				WriteColor("Enter problems to solve (separated by commas): ", ConsoleColor.Yellow);
				string[] problems = Console.ReadLine().Split(',').Distinct().ToArray();

				Type type = typeof(Problems);
				Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
				for (int i = 0; i < problems.Length; i++)
					methods.Add(problems[i], type.GetMethod($"Problem{problems[i]}"));
				Problems p = new Problems();

				Console.WriteLine();
				WriteLineColor(string.Format("{0, -10}{1, -15}{2, -15}", "Problem", "Answer", "Time"), ConsoleColor.Magenta);
				Console.WriteLine();

				foreach (KeyValuePair<string, MethodInfo> kvp in methods)
				{
					try
					{
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						WriteLineColor(string.Format("{0, -10}{1, -15}{2, -15}", kvp.Value.Name.Numeric(), kvp.Value.Invoke(p, null), stopwatch.Elapsed), ConsoleColor.White);
					}
					catch (NullReferenceException)
					{
						WriteLineColor(string.Format("{0, -10}Problem not found.", kvp.Key), ConsoleColor.Red);
					}
					catch (Exception ex)
					{
						WriteLineColor("FATAL ERROR", ConsoleColor.Red);
						WriteLineColor(ex.GetAllInnerExceptionMessages("\n===\n"), ConsoleColor.Red);
					}
				}

				Console.WriteLine();
			}
		}

		public static void WriteLineColor(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
		}

		public static void WriteColor(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
		}
	}
}