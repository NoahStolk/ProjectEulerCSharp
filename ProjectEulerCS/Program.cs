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
	/// <summary>
	/// Handles all GUI-related tasks, as well as executing commands.
	/// </summary>
	public static class Program
	{
		private static readonly string ProblemNumberFormat = "D3";

		private static ConsoleColor UnsolvedColor => ConsoleColor.DarkGray;

		private static ConsoleColor GetColor(ProblemState state)
		{
			switch (state)
			{
				case ProblemState.Solved: return ConsoleColor.Green;
				case ProblemState.TooSlow: return ConsoleColor.DarkGreen;
				case ProblemState.InProgress: return ConsoleColor.Magenta;
				default: return ConsoleColor.White;
			}
		}

		private static string FormatColumns(params string[] columnNames)
		{
			StringBuilder format = new StringBuilder();
			for (int i = 0; i < columnNames.Length; i++)
				format.Append($"{{{i}, -15}}");
			return string.Format(format.ToString(), columnNames);
		}

		private static void TrySetWindowSize(int width, int height)
		{
			try
			{
				Console.SetWindowSize(width, height);
			}
			catch
			{
				ConsoleUtils.WriteLineColor($"Failed to set window size to {width}, {height}", ConsoleColor.Red);
			}
		}

		public static void Main()
		{
			TrySetWindowSize(192, 64);

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
						Dictionary<int, ProblemState> problems = new Dictionary<int, ProblemState>();
						foreach (MethodInfo method in typeof(ProblemsHandler).GetMethods())
							if (int.TryParse(method.Name.Numeric(), out int problem))
								problems.Add(problem, method.GetCustomAttribute<Problem>().State);

						foreach (ProblemState state in (ProblemState[])Enum.GetValues(typeof(ProblemState)))
							ConsoleUtils.WriteLineColor($"{problems.Where(p => p.Value == state).FirstOrDefault().Key.ToString(ProblemNumberFormat)} {state}", GetColor(state));

						for (int i = 1; i < problems.Keys.Max(); i++)
						{
							if (!problems.ContainsKey(i))
							{
								ConsoleUtils.WriteLineColor($"{i.ToString(ProblemNumberFormat)} Unsolved", UnsolvedColor);
								break;
							}
						}

						Console.WriteLine();

						for (int i = 0; i < Math.Ceiling((double)problems.Keys.Max() / 10); i++)
						{
							for (int j = 0; j < 10; j++)
							{
								int problem = i * 10 + j + 1;
								if (problems.Keys.Contains(problem))
								{
									ProblemState state = problems[problem];
									ConsoleUtils.WriteColor($"{problem.ToString(ProblemNumberFormat)} ", GetColor(state));
								}
								else
								{
									ConsoleUtils.WriteColor($"{problem.ToString(ProblemNumberFormat)} ", UnsolvedColor);
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
					problemMethods.Add(problemsInput[i], typeof(ProblemsHandler).GetMethod($"Problem{problemsInput[i].ToString(ProblemNumberFormat)}"));

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
				ConsoleUtils.WriteLineColor(FormatColumns(kvp.Key.ToString(ProblemNumberFormat), "Problem not found."), ConsoleColor.Red);
			}
			catch (Exception ex)
			{
				ConsoleUtils.WriteLineColor("FATAL ERROR", ConsoleColor.Red);
				ConsoleUtils.WriteLineColor(ex.AllInnerExceptionMessages(), ConsoleColor.Red);
			}
		}
	}
}