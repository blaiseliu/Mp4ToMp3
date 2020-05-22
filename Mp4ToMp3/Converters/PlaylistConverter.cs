using System.Diagnostics;
using System.IO;
using Mp4ToMp3.Extensions;
using Mp4ToMp3.ParseOptions;
using Serilog;
using Serilog.Core;

namespace Mp4ToMp3.Converters
{
    internal class PlaylistConverter: ConverterBase<PlaylistOptions>
    {
        public override void Process(PlaylistOptions options)
        {
            var sourceFolderName = options.SourceFolder;
            var targetFolderName = Path.Combine(sourceFolderName, options.TargetFolder);
            Logger.Information($"Source Folder: {sourceFolderName}");
            Logger.Information($"Target Folder: {targetFolderName}");
            Logger.Divide();
            if (!Directory.Exists(targetFolderName))
            {
                Directory.CreateDirectory(targetFolderName);
                Logger.Information("Target Folder Created.");
                Logger.NewLine();
            }

            var sourceFolder=new DirectoryInfo(sourceFolderName);
            var sourceFiles = sourceFolder.GetFiles("*.mp4");
            foreach (var f in sourceFiles)
            {
                var nameWithoutExtension = Path.GetFileNameWithoutExtension(f.Name);
                var sourceFileName = f.FullName;
                Logger.Information($"Convert: {f.Name}");
                Logger.Information($"into: {nameWithoutExtension}.mp3");

                var targetFileName = Path.Combine(targetFolderName, $"{nameWithoutExtension}.mp3");
                Convert(sourceFileName, targetFileName);
                Logger.NewLine();
            }
        }

        private void Convert(string sourceFileName, string targetFileName)
        {
            var arguments = $"-i \"{sourceFileName}\" \"{targetFileName}\"";
            
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "ffmpeg.exe",
                    Arguments = arguments,
                    UseShellExecute = false, 
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            if (!process.Start())
            {
                Logger.Error("Error starting");
            }

            //while (!process.StandardOutput.EndOfStream)
            //{
            //    var line = process.StandardOutput.ReadLine();
            //    Logger.Information(line);
            //}

            //var output = process.StandardOutput.ReadToEnd();
            //Logger.Information(output);

            process.WaitForExit();
        }
    }

    internal abstract class ConverterBase<T>
        where T : ParseOptionBase
    {
        public abstract void Process(T options);
        protected Logger Logger =  new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }
}