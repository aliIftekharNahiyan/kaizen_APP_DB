using Kaizen.Debugging;

namespace Kaizen
{
    public class KaizenConsts
    {
        public const string LocalizationSourceName = "Kaizen";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8fa58b6c3f864b479e5dc324369fc201";
    }
}
