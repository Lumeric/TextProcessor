using System.Text;

namespace UploaderBusinessLogic
{
	public class FileReader : IFileReader
	{
		private readonly char[] _wordSeparators = { ' ', '\r', '\n' };

		public string[] ReadAndSplit(string filePath)
		{
			try
			{
				var file = File.ReadAllText(filePath, Encoding.UTF8);

				return SelectWords(file);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		private string[] SelectWords(string file)
		{
			return file.Split(_wordSeparators);
		}
	}
}
