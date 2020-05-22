using Serilog.Core;

namespace Mp4ToMp3.Extensions
{
    public static class SerilogExtensions
    {
        public static void Divide(this Logger logger)
        {
            const string divider = "----------";
            logger.Information(divider);
        }
        public static void NewLine(this Logger logger)
        {
            const string divider = "";
            logger.Information(divider);
        }
    }
}
