using TextProcessorDownloader;

var downloadManager = new TextProcessorDownloadManager();
await downloadManager.Start();

Console.WriteLine("Success!");
Console.ReadLine();