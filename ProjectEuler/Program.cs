using NetBase.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ProjectEuler
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Type problemsType = typeof(Problems);
			Problems problems = new Problems();

			WriteLineColor("Project Euler - C#", ConsoleColor.Green);
			WriteLineColor("Enter 'help' for a list of commands.", ConsoleColor.Cyan);
			Console.WriteLine();

			for (; ; )
			{
				WriteColor("Enter command: ", ConsoleColor.Yellow);

				string input = Console.ReadLine();
				Console.WriteLine();

				if (input.ToLower() == "help")
				{
					WriteLineColor(string.Format("{0, -15}{1, -15}", "Command", "Description"), ConsoleColor.Magenta);
					WriteLineColor(string.Format("{0, -15}{1, -15}", "help", "Shows the list of commands."), ConsoleColor.Cyan);
					WriteLineColor(string.Format("{0, -15}{1, -15}", "progress", "Shows all available problems."), ConsoleColor.Cyan);
					WriteLineColor(string.Format("{0, -15}{1, -15}", "exit", "Exits the program."), ConsoleColor.Cyan);
					WriteLineColor(string.Format("{0, -15}{1, -15}", "x", "Solves problem x."), ConsoleColor.Cyan);
					WriteLineColor(string.Format("{0, -15}{1, -15}", "x,y,z", "Solves problem x, y, and z."), ConsoleColor.Cyan);
					Console.WriteLine();
					continue;
				}
				else if (input.ToLower() == "progress")
				{
					WriteLineColor(string.Format("{0, -15}{1, -15}", "Problem", "Return type"), ConsoleColor.Magenta);
					foreach (MethodInfo method in problemsType.GetMethods())
						if (method.Name.Contains("Problem"))
							WriteLineColor(string.Format("{0, -15}{1, -15}", method.Name.Numeric(), method.ReturnType), ConsoleColor.Cyan);

					Console.WriteLine();
					continue;
				}
				else if (input.ToLower() == "exit")
				{
					Environment.Exit(0);
				}

				string[] problemsInput = input.Split(',').Distinct().ToArray();

				Dictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
				for (int i = 0; i < problemsInput.Length; i++)
					methods.Add(problemsInput[i], problemsType.GetMethod($"Problem{problemsInput[i]}"));

				WriteLineColor(string.Format("{0, -15}{1, -15}{2, -15}", "Problem", "Answer", "Time"), ConsoleColor.Magenta);

				foreach (KeyValuePair<string, MethodInfo> kvp in methods)
				{
					try
					{
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();

						WriteLineColor(string.Format("{0, -15}{1, -15}{2, -15}", kvp.Value.Name.Numeric(), kvp.Value.Invoke(problems, null), stopwatch.Elapsed), ConsoleColor.White);
					}
					catch (NullReferenceException)
					{
						WriteLineColor(string.Format("{0, -15}{1, -15}", kvp.Key, "Problem not found."), ConsoleColor.Red);
					}
					catch (Exception ex)
					{
						WriteLineColor("FATAL ERROR", ConsoleColor.Red);
						WriteLineColor(ex.AllInnerExceptionMessages("\n===\n"), ConsoleColor.Red);
					}
				}

				Console.WriteLine();
			}
		}

		private static void WriteLineColor(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(message);
		}

		private static void WriteColor(string message, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write(message);
		}
	}
}