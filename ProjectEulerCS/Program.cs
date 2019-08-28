using NetBase.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NetBase.Console;
using System.Text.RegularExpressions;
using System.Text;
using ProjectEulerCS.Problems;

namespace ProjectEulerCS
{
	public static class Program
	{
		private static string FormatColumns(params string[] columnNames)
		{
			StringBuilder format = new StringBuilder();
			for (int i = 0; i < columnNames.Length; i++)
				format.Append($"{{{i}, -15}}");
			return string.Format(format.ToString(), columnNames);
		}

		private static ConsoleColor GetColor(ProblemState state)
		{
			switch (state)
			{
				case ProblemState.Solved: return ConsoleColor.Green;
				case ProblemState.TooSlow: return ConsoleColor.Magenta;
				case ProblemState.InProgress: return ConsoleColor.DarkYellow;
				default: return ConsoleColor.White;
			}
		}

		public static void Main()
		{
			Console.SetWindowSize(192, 64);

			ConsoleUtils.WriteBanner("Project Euler - C#", 4, 2, '-', ConsoleColor.DarkBlue, ConsoleColor.Green);
			Console.WriteLine();
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
						ConsoleUtils.WriteLineColor(FormatColumns("Command", "Description"), ConsoleColor.Magenta);
						ConsoleUtils.WriteLineColor(FormatColumns("help", "Shows the list of commands."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(FormatColumns("progress", "Shows all available problems."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(FormatColumns("exit", "Exits the program."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(FormatColumns("x", "Solves problem x."), ConsoleColor.Cyan);
						ConsoleUtils.WriteLineColor(FormatColumns("x,y,z", "Solves problem x, y, and z."), ConsoleColor.Cyan);
						Console.WriteLine();
						continue;
					case "progress":
						foreach (ProblemState state in (ProblemState[])Enum.GetValues(typeof(ProblemState)))
							ConsoleUtils.WriteLineColor($"\t\t{state}", GetColor(state));
						ConsoleUtils.WriteLineColor("\t\tUnsolved", ConsoleColor.White);
						Console.WriteLine();

						Dictionary<int, ProblemState> problems = new Dictionary<int, ProblemState>();
						foreach (MethodInfo method in typeof(ProblemsHandler).GetMethods())
							if (int.TryParse(method.Name.Numeric(), out int problem))
								problems.Add(problem, method.GetCustomAttribute<Problem>().State);

						for (int i = 0; i < Math.Ceiling((double)problems.Keys.Max() / 10); i++)
						{
							for (int j = 0; j < 10; j++)
							{
								int problem = i * 10 + j + 1;
								if (problems.Keys.Contains(problem))
								{
									ProblemState state = problems[problem];
									ConsoleUtils.WriteColor($"{problem.ToString("D3")} ", GetColor(state));
								}
								else
								{
									ConsoleUtils.WriteColor($"{problem.ToString("D3")} ", ConsoleColor.White);
								}
							}
							Console.WriteLine();
						}

						Console.WriteLine();
						continue;
					case "exit":
						Environment.Exit(0);
						break;
				}

				int[] problemsInput = Array.ConvertAll(
					Regex.Replace(input, "[^0-9,]", "") // Remove all characters except numbers and commas.
						.Split(',') // Split by comma.
						.Distinct() // Remove duplicates.
						.Where(s => s.Length != 0) // Remove empty strings.
						.ToArray(), s => int.Parse(s)); // Parse all.

				Dictionary<int, MethodInfo> problemMethods = new Dictionary<int, MethodInfo>();
				for (int i = 0; i < problemsInput.Length; i++)
					problemMethods.Add(problemsInput[i], typeof(ProblemsHandler).GetMethod($"Problem{problemsInput[i].ToString("D3")}"));

				ConsoleUtils.WriteLineColor(FormatColumns("Problem", "Answer", "Time"), ConsoleColor.Magenta);

				foreach (KeyValuePair<int, MethodInfo> kvp in problemMethods)
					ExecuteProblem(kvp);

				Console.WriteLine();
			}
		}

		private static void ExecuteProblem(KeyValuePair<int, MethodInfo> kvp)
		{
			try
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				ConsoleUtils.WriteLineColor(FormatColumns(kvp.Value.Name.Numeric(), kvp.Value.Invoke(ProblemsHandler.Instance, null).ToString(), stopwatch.Elapsed.ToString()), ConsoleColor.White);
			}
			catch (NullReferenceException)
			{
				ConsoleUtils.WriteLineColor(FormatColumns(kvp.Key.ToString("D3"), "Problem not found."), ConsoleColor.Red);
			}
			catch (Exception ex)
			{
				ConsoleUtils.WriteLineColor("FATAL ERROR", ConsoleColor.Red);
				ConsoleUtils.WriteLineColor(ex.AllInnerExceptionMessages("\n\n\t"), ConsoleColor.Red);
			}
		}
	}
}