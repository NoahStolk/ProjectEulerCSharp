using System;
using System.Text;

namespace ProjectEulerCSharp.Extensions
{
	public static class ExceptionExtensions
	{
		public static string GetAllInnerExceptionMessages(this Exception exception, int depth = 1)
		{
			StringBuilder sb = new StringBuilder($"{exception.Message}\n");
			if (exception.InnerException != null)
				sb.AppendLine($"{new string('\t', depth)}{exception.InnerException.GetAllInnerExceptionMessages(++depth)}");
			return sb.ToString();
		}
	}
}