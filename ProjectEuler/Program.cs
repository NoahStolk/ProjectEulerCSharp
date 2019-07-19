using NetBase.Extensions;
using NetBase.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ProjectEuler
{
	public static class Program
	{
		public static void Main()
		{
			ConsoleUtils.WriteLineColor("Project Euler - C#", ConsoleColor.Green);
			ConsoleUtils.WriteLineColor("Enter 'help' for a list of commands.", ConsoleColor.Cyan);
			Console.WriteLine();

			for (; ; )
			{
				ConsoleUtils.WriteColor("Enter command: ", ConsoleColor.Yellow);

				string input = Console.ReadLine().ToLower();
				Console.WriteLine();

				switch (input)
				{
					case "help":
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "Command", "Description"), ConsoleColor.Magenta);
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "help", "Shows the list of commands."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "progress", "Shows all available problems."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "exit", "Exits the program."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "x", "Solves problem x."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "x,y,z", "Solves problem x, y, and z."), ConsoleColor.Cyan);
						Console.WriteLine();
						continue;
					case "progress":
						ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", "Problem", "Return type"), ConsoleColor.Magenta);
						foreach (MethodInfo method in typeof(Problems).GetMethods())
							if (method.Name.Contains("Problem"))
								ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", method.Name.Numeric(), method.ReturnType), ConsoleColor.Cyan);

						Console.WriteLine();
						continue;
					case "exit":
						Environment.Exit(0);
						break;
				}

				string[] problemsInput = input.Split(',').Distinct().ToArray();

				Dictionary<string, MethodInfo> problemMethods = new Dictionary<string, MethodInfo>();
				for (int i = 0; i < problemsInput.Length; i++)
					problemMethods.Add(problemsInput[i], typeof(Problems).GetMethod($"Problem{problemsInput[i]}"));

				ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}{2, -15}", "Problem", "Answer", "Time"), ConsoleColor.Magenta);

				foreach (KeyValuePair<string, MethodInfo> kvp in problemMethods)
				{
					ExecuteProblem(kvp);
				}

				Console.WriteLine();
			}
		}

		private static void ExecuteProblem(KeyValuePair<string, MethodInfo> kvp)
		{
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}{2, -15}", kvp.Value.Name.Numeric(), kvp.Value.Invoke(Problems.Instance, null), stopwatch.Elapsed), ConsoleColor.White);
			}
			catch (NullReferenceException)
			{
				ConsoleUtils.WriteLineColor(string.Format("{0, -15}{1, -15}", kvp.Key, "Problem not found."), ConsoleColor.Red);
			}
			catch (Exception ex)
			{
				ConsoleUtils.WriteLineColor("FATAL ERROR", ConsoleColor.Red);
				ConsoleUtils.WriteLineColor(ex.AllInnerExceptionMessages("\n===\n"), ConsoleColor.Red);
			}
		}
	}
}