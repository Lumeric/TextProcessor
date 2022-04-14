using TextProcessorUploader;

var manager = new TextProcessingUploadManager();
await manager.Start();

Console.WriteLine("Success!");
Console.ReadLine();