using System.IO;
using System.Linq;

namespace ProjectEuler
{
	public static class Utils
	{
		public static int[][] GenerateMatrix(int problem)
		{
			string file = Path.Combine("Resources", $"{problem.ToString("D3")}.txt");
			int[][] matrix = new int[File.ReadLines(file).Count()][];
			int i = 0;
			foreach (string line in File.ReadAllLines(file))
			{
				string lineCopy = line;
				while (lineCopy.Contains("  "))
					lineCopy = lineCopy.Replace("  ", " ");
				if (lineCopy[0] == ' ')
					lineCopy = lineCopy.Substring(1);

				string[] numbers = lineCopy.Split(' ');
				matrix[i] = new int[numbers.Length];
				for (int j = 0; j < numbers.Length; j++)
					matrix[i][j] = int.Parse(numbers[j]);
				i++;
			}
			return matrix;
		}
	}
}