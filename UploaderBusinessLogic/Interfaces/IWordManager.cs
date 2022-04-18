namespace UploaderBusinessLogic
{
	public interface IWordManager
	{
		Dictionary<string, int> GetWords(List<string> words);
	}
}
