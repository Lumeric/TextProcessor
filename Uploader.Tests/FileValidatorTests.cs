using NUnit.Framework;
using UploaderBusinessLogic;

namespace Uploader.Tests
{
	[TestFixture]
	public class FileValidatorTests
	{
		[Test]
		public void ValidateWords_ValidateIncorrectInput_ReturnsEmpty()
		{
			//Arrange
			var validator = new FileValidator();

			//Action-
			var result = validator.ValidateWords(CreateIncorrectInput());

			//Assert
			Assert.IsEmpty(result);
		}

		[Test]
		public void ValidateWords_ValidateCorrectInput_ReturnsListCount()
		{
			//Arrange
			var validator = new FileValidator();

			//Action-
			var input = CreateCorrectInput();
			var result = validator.ValidateWords(input);

			//Assert
			Assert.AreEqual(result.Count, input.Length);
		}

		[Test]
		public void ValidateWords_ValidateIncorrectInput_ReturnsPartially()
		{
			//Arrange
			var validator = new FileValidator();

			//Action-
			var input = CreateMixedInput();
			var expectedCount = CreateCorrectInput().Length;
			var result = validator.ValidateWords(input);

			//Assert
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