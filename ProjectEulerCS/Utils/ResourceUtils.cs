using System.IO;
using System.Linq;

namespace ProjectEulerCS.Utils
{
	public static class ResourceUtils
	{
		public static int[][] GenerateMatrix(int problem)
		{
			string filePath = Path.Combine("Resources", $"{problem.ToString("D3")}.txt");
			int[][] matrix = new int[File.ReadLines(filePath).Count()][];
			int i = 0;
			foreach (string line in File.ReadAllLines(filePath))
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

		public static int[] GenerateList(int problem)
		{
			string filePath = Path.Combine("Resources", $"{problem.ToString("D3")}.txt");
			string fileContents = File.ReadAllText(filePath);
			while (fileContents.Contains("  "))
				fileContents = fileContents.Replace("  ", " ");
			fileContents = fileContents.Replace("\r\n", " ");
			if (fileContents[0] == ' ')
				fileContents = fileContents.Substring(1);

			string[] numbers = fileContents.Split(' ');
			int[] matrix = new int[numbers.Length];
			for (int i = 0; i < matrix.Length; i++)
				matrix[i] = int.Parse(numbers[i]);
			return matrix;
		}
	}
}