using System.Text.RegularExpressions;

namespace UploaderBusinessLogic
{
	public class FileValidator : IFileValidator
	{
		private const string REGEX_PATTERN = @"^([a-zа-яё]{3,20})$";

		public List<string> ValidateWords(string[] inputWords)
		{
			List<string> validatedWords = new List<string>();

			for (int i = 0; i < inputWords.Length; i++)
			{
				if (IsMatchPattern(inputWords[i]))
					validatedWords.Add(inputWords[i]);
			}

			return validatedWords;
		}

		private bool IsMatchPattern(string word)
		{
			return Regex.IsMatch(word, REGEX_PATTERN, RegexOptions.IgnoreCase);
		}
	}
}