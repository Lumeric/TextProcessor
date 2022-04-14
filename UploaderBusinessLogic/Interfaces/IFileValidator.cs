using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploaderBusinessLogic
{
	public interface IFileValidator
	{
		List<string> ValidateWords(string[] inputWords);
	}
}
