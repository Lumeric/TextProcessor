using NUnit.Framework;
using UploaderBusinessLogic;

namespace Uploader.Tests
{
	[TestFixture]
	public class WordsValidatorTests
	{
		[Test]
		public void ValidateWords_ValidateIncorrectInput_ReturnsEmpty()
		{
			var validator = new WordsValidator();

			var result = validator.ValidateWords(CreateIncorrectInput());

			Assert.IsEmpty(result);
		}

		[Test]
		public void ValidateWords_ValidateCorrectInput_ReturnsListCount()
		{
			var validator = new WordsValidator();

			var input = CreateCorrectInput();
			var result = validator.ValidateWords(input);

			Assert.AreEqual(result.Count, input.Length);
		}

		[Test]
		public void ValidateWords_ValidateIncorrectInput_ReturnsPartially()
		{
			var validator = new WordsValidator();

			var input = CreateMixedInput();
			var expectedCount = CreateCorrectInput().Length;
			var result = validator.ValidateWords(input);

			Assert.AreEqual(result.Count, expectedCount);
		}

		private string[] CreateIncorrectInput()
		{
			return new string[] { "eR", "WX", " ", "VeryLargeSuperStringWow", "Этотожеоченьдлиннаястрока", "ы" };
		}

		private string[] CreateCorrectInput()
		{
			return new string[] { "eRa", "WXT", "мороз", "sunny", "строка", "yet" };
		}

		private string[] CreateMixedInput()
		{
			return CreateCorrectInput().Concat(CreateIncorrectInput()).ToArray();
		}
	}
}