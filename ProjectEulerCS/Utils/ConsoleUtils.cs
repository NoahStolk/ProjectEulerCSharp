using NetBase.Extensions;
using System;
using Cmd = System.Console;

namespace ProjectEulerCS.Utils
{
	public static class ConsoleUtils
	{
		public static void WriteColor(string message, ConsoleColor color)
		{
			Cmd.ForegroundColor = color;
			Cmd.Write(message);
			Cmd.ResetColor();
		}

		public static void WriteLineColor(string message, ConsoleColor color)
		{
			WriteColor($"{message}\n", color);
		}

		public static void WriteColorMultiple(params MessagePart[] messageParts)
		{
			foreach (MessagePart mp in messageParts)
			{
				Cmd.ForegroundColor = mp.Color;
				Cmd.Write(mp.Message);
			}
			Cmd.ResetColor();
		}

		public static void WriteLineColorMultiple(params MessagePart[] messageParts)
		{
			WriteColorMultiple(messageParts);
			Cmd.WriteLine();
		}

		public static void ClearLine(int offset)
		{
			int currentLineCursor = Cmd.CursorTop + offset;
			Cmd.SetCursorPosition(0, Cmd.CursorTop);
			Cmd.Write(new string(' ', Cmd.BufferWidth));
			Cmd.SetCursorPosition(0, currentLineCursor - offset);
		}

		public static void WriteBanner(string name, int paddingHor, int paddingVer, char border, ConsoleColor background, ConsoleColor foreground)
		{
			Cmd.BackgroundColor = background;
			Cmd.ForegroundColor = foreground;
			for (int i = 0; i < paddingVer * 2 + 1; i++)
			{
				if (i == 0 || i == paddingVer * 2)
					Cmd.WriteLine(border.ToString().Repeat(name.Length + paddingHor * 2 + 2));
				else if (i == paddingVer)
					Cmd.WriteLine($"{border}{" ".Repeat(paddingHor)}{name}{" ".Repeat(paddingHor)}{border}");
				else
					Cmd.WriteLine($"{border}{" ".Repeat(paddingHor * 2 + name.Length)}{border}");
			}
			Cmd.ResetColor();
		}
	}
}