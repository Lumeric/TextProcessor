using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploaderBusinessLogic
{
	public interface IFileUploader
	{
		Task UploadFile();
	}
}
