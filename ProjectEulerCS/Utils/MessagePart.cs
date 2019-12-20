using System;

namespace ProjectEulerCS.Utils
{
	public class MessagePart
	{
		public string Message { get; set; }
		public ConsoleColor Color { get; set; }

		public MessagePart(string message, ConsoleColor color)
		{
			Message = message;
			Color = color;
		}
	}
}