using System;
using System.Collections.Generic;
using CommandLine;
using Mp4ToMp3.Converters;
using Mp4ToMp3.ParseOptions;

namespace Mp4ToMp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<PlaylistOptions>(args)
                .WithParsed<PlaylistOptions>(x => new PlaylistConverter().Process(x))
                .WithNotParsed(HandleParseError);

            Console.WriteLine("Press any key to finish.");
            Console.ReadKey(true);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
