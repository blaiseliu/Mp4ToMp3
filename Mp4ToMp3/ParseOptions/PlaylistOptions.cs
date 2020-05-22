using CommandLine;

namespace Mp4ToMp3.ParseOptions
{
    [Verb("playlist",HelpText="Youtube Playlist.")]
    public class PlaylistOptions:ParseOptionBase
    {
        [Option('s',"source", Required = true)]
        public string SourceFolder { get; set; }
        [Option('t', "target", Required = false, Default = "export")]
        public string TargetFolder { get; set; }
    }
}
