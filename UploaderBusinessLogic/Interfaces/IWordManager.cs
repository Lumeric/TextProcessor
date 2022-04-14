using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploaderBusinessLogic
{
	public interface IWordManager
	{
		Dictionary<string, int> GetWords(List<string> words);
	}
}
