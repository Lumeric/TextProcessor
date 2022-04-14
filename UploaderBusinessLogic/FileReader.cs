using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploaderBusinessLogic
{
	public class FileReader : IFileReader
	{
		private char[] _wordSeparators = { ' ', '\r', '\n' };

		public string[] ReadAndSplit(string filePath)
		{
			try
			{
				var file = File.ReadAllText(filePath, Encoding.UTF8);
				var words = SelectWords(file);

				return words;
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine($@"File ""{ex.FileName}"" does not exist.");
				throw;
			}
			catch (FileLoadException ex)
			{
				Console.WriteLine($@"File ""{ex.FileName}"" fail to load.");
				throw;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		private string[] SelectWords(string file)
		{
			string[] unfilteredWords = file.Split(_wordSeparators);

			return unfilteredWords;
		}
	}
}
