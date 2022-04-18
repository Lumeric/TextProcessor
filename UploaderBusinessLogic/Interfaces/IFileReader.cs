namespace UploaderBusinessLogic
{
	public interface IFileReader
	{
		string[] ReadAndSplit(string filePath);
	}
}
