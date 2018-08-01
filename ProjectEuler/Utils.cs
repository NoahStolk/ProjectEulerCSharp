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
				string[] numbers = line.Split(' ');
				matrix[i] = new int[numbers.Length];
				for (int j = 0; j < numbers.Length; j++)
					matrix[i][j] = int.Parse(numbers[j]);
				i++;
			}
			return matrix;
		}
	}
}